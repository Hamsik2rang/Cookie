using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoLoader : MonoBehaviour
{
    private string[] statIconName;

    public static InfoLoader instance
    {
        get;
        private set;
    } = null;
    private GameObject info;
    private Text objectName;
    private Text hpText;
    private Image firstPrimeStatImage;
    private Image secondPrimeStatImage;

    private int objectHP;
    private int objectMaxHP;
    private int[] primeStats = new int[2];


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
        statIconName = new string[] { "Strength", "Agility", "Defence", "Vitality" };

        info = GameObject.Find("Info");
        objectName = GameObject.Find("Name").GetComponent<Text>();
        hpText = GameObject.Find("HP").GetComponent<Text>();

        firstPrimeStatImage = GameObject.Find("First Icon").GetComponent<Image>();
        secondPrimeStatImage = GameObject.Find("Second Icon").GetComponent<Image>();

        info.SetActive(false);
    }

    public void PrintInfo(IStatus printedObject, params int[] objectStats)
    {
        if (printedObject == null)
            info.SetActive(false);
        else
        {
            int[] stats = new int[objectStats.Length];
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i] = objectStats[i];
            }
            GetPrimeStats(stats);

            firstPrimeStatImage.sprite = Resources.Load<Sprite>("Icons/" + statIconName[primeStats[0]]);
            secondPrimeStatImage.sprite = Resources.Load<Sprite>("Icons/" + statIconName[primeStats[1]]);

            hpText.text = printedObject.GetHP().ToString() + " / " + printedObject.GetMaxHP().ToString();

            info.SetActive(true);
        }

        //TODO:primeStats 전역으로 안 쓰게 리팩토링 할 것
        void GetPrimeStats(int[] stats)
        {

            primeStats[0] = primeStats[1] = 0;
            for (int i = 1; i < stats.Length; i++)
            {
                if (stats[primeStats[1]] < stats[i] || primeStats[0] == primeStats[1])
                {
                    primeStats[1] = i;
                    if (stats[primeStats[0]] < stats[primeStats[1]])
                    {
                        int temp = primeStats[0];
                        primeStats[0] = primeStats[1];
                        primeStats[1] = temp;
                    }
                }
            }
        }
    }
}