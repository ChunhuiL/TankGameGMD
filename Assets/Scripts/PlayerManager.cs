using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public int lifeValue = 3;
    [HideInInspector]
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat;
    private static PlayerManager instance;
    public GameObject born;
    public Text playerScoreText;
    public Text playerLifeValueText;
    public GameObject isDefeatUI;
    public GameObject stopUI;
    //public int timeScale = 1;
    public static PlayerManager Instance { get => instance; set => instance = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToMenu", 3);
            return;
        }
        if (isDead)
        {
            Recover();
        }
        playerScoreText.text = playerScore.ToString();
        playerLifeValueText.text = lifeValue.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
           // timeScale = 0;
            Time.timeScale = 0;
            stopUI.SetActive(true);

        }
    }
    public void Recover()
    {
        if (lifeValue <=1)
        {
            lifeValue--;
            isDefeat = true;
            Invoke("ReturnToMenu", 3);
        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }
    private void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
