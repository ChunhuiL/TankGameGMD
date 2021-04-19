using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3;
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//up right down left    
    private Vector3 bulletEulerAngles;
    private float timeVal;
    private float timeValChangeDirection=4;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    private float h;
    private float v=-1;

    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }

    void Update()
    {
      
        if (timeVal >= 3)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (timeValChangeDirection >= 4)
        {
            int num = Random.Range(0,8);
            if(num>=5)
            {
                v = -1;
                h = 0;
            }
            else if (num ==0)
            {
                v = 1;
                h = 0;
            }
            else if (num >=0&&num<=2)
            {
                v = 0;
                h = -1;
            }
            else if (num >= 3 && num <= 4)
            {
                v = 0;
                h = 1;
            }
            timeValChangeDirection = 0;
        }
        else
            {
                timeValChangeDirection += Time.fixedDeltaTime;
            }

        transform.Translate(Vector3.up * v * moveSpeed * Time.deltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        if (v != 0)
        {
            return;
        }

        transform.Translate(Vector3.right * h * moveSpeed * Time.deltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
    }
    private void Attack()
    {

            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;

    }
    private void Die()
    {
        PlayerManager.Instance.playerScore++;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            timeValChangeDirection = 4;
        }
    }
}
