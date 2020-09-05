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
    private int turnCount = 0;
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
        DontDestroyOnLoad(this.gameObject);
    }

    //Turn count control method
    public void AddTurnCount()
    {
        turnCount++;
        turnText.text = "Turn " + turnCount;
    }
    public void GameOver()
    {
        gameOverUI = GameObject.FindGameObjectsWithTag("GameoverUI");

        foreach (GameObject g in gameOverUI)    //Game Over UI Set
        {
            g.SetActive(true);
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("Intro Scene");
        }

        //TODO: Get game result and statistics(player both's stat, attack routine, etc..) and parse to JSON
    }

    private void EndGame()  //Only called by Message passing
    {
        Application.Quit();
    }
}
