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
}
