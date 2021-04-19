using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StopGame : MonoBehaviour
{
    private int choice = 0;
    public Transform posOne;
    public Transform posTwo;
    public PlayerManager pm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            choice = 1;
            transform.position = posOne.position;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            choice = 2;
            transform.position = posTwo.position;
        }
        if (choice == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            pm.stopUI.SetActive(false);
            Time.timeScale = 1;
            //pm.timeScale=1;
        }
        if (choice == 2 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }
}
