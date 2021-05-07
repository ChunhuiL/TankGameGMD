using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public float moveSpeed = 3;
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//up right down left    
    private Vector3 bulletEulerAngles;
    private float timeVal;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;
    [HideInInspector]
    private float defendTimeVal = 3;
    public bool isDefended = true;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;
    delegate void Delegate();
    Delegate move;
    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        move = Move;
        move();
    }
    
    void Update()
    {
        if (isDefended)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }
      
    }
    //Avoid shaking in FixedUpdate
    void FixedUpdate()
    {
        if (PlayerManager.Instance.isDefeat)
        {
            return;
        }
        Move(); 
        if (timeVal>=0.4f)
        {
            //Attack();
            StartCoroutine(Attack());
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
    }

    private void Move()
    {
        //v (-1,1)
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
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
        if (Mathf.Abs(v) > 0.05f)
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        //if v h same time ,v first
        if (v != 0)
        {
            return;
        }
        //h (-1,1)
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
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
        if (Mathf.Abs(h) > 0.05f)
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[0];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
    }
    IEnumerator Attack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position,Quaternion.Euler(transform.eulerAngles+bulletEulerAngles));
            timeVal = 0;
        }
        yield return null;
    }
    public void Die()
    {
        if (isDefended)
        {
            return;
        }
        PlayerManager.Instance.isDead = true;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
