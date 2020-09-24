using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IntroSceneManager : MonoBehaviour
{
    public void Load_BattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void Load_IntroScene()
    {
        StatManager statManager = GameObject.Find("StatManager").GetComponent<StatManager>();
        statManager.isStat = true;
        SceneManager.LoadScene("IntroScene");
    }
}
