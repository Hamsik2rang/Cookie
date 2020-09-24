using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;                             //Singleton Pattern
    public bool isGameOver = false;
    public Text turnText;
    private int turnCount = 1;
    private GameObject[] gameOverUI;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (isGameOver)
        {
            GameOver();
        }
    }

    //Turn count control method
    public void AddTurnCount()
    {
        turnCount++;
        turnText.text = "Turn " + turnCount;
    }

    public void GameOver()
    {
        //gameOverUI = GameObject.FindGameObjectsWithTag("GameoverUI");
        GameObject.Find("Canvas").transform.FindChild("GameOverUI").gameObject.SetActive(true);

        //Time.timeScale = 0;


        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("Intro Scene");
        }
    }

    private void EndGame()  //Only called by Message passing
    {
        Application.Quit();
    }

}
