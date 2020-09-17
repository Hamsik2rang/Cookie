using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatManager : MonoBehaviour
{
    public static StatManager instance = null;
    public Text str;
    public Text agl;
    public Text def;
    public Text vit;

    public Text[] skillText = new Text[3];

    int StatPoint = 25;
    int skillStatPoint = 5;
    const int MAX_STAT = 10;

    const int MAX_SKILL_STAT = 3;
    public int[] stat = new int[] { 0, 0, 0, 0 };
    public int[] skillStat = new int[3];

    public bool isStat = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //겹치면 새로만들어진거 파괴 
            Destroy(gameObject);
        }

        RefTexts();
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void RefTexts()
    {
        GameObject parentUI = GameObject.Find("Canvas");
        str = GameObject.Find("STR").transform.GetChild(0).GetComponent<Text>();
        agl = GameObject.Find("AGL").transform.GetChild(0).GetComponent<Text>();
        def = GameObject.Find("DEF").transform.GetChild(0).GetComponent<Text>();
        vit = GameObject.Find("VIT").transform.GetChild(0).GetComponent<Text>();

        skillText[0] = GameObject.Find("FirstSkill").transform.GetChild(0).GetComponent<Text>();
        skillText[1] = GameObject.Find("SecondSkill").transform.GetChild(0).GetComponent<Text>();
        skillText[2] = GameObject.Find("ThirdSkill").transform.GetChild(0).GetComponent<Text>();
    }


    private void Update()
    {
        RefTexts();

        if(isStat == true)
        { 
            while (StatPoint > 0)
            {
                int randomStat = Random.Range(0, MAX_STAT + 1); //0 ~ MAX_STAT의 랜덤값
                int idx = Random.Range(0, 4); // 0 ~ 3의 랜덤값 인덱스로 활용

                if (stat[idx] < MAX_STAT && randomStat != 0)
                {
                    if (randomStat > StatPoint) continue;

                    else if (stat[idx] + randomStat > MAX_STAT)
                    {
                        int temp = MAX_STAT - stat[idx];
                        stat[idx] += temp;
                        StatPoint -= temp;
                    }

                    else
                    {
                        stat[idx] += randomStat;
                        StatPoint -= randomStat;
                    }
                }
            }

            str.text = stat[0].ToString();
            agl.text = stat[1].ToString();
            def.text = stat[2].ToString();
            vit.text = stat[3].ToString();

            while (skillStatPoint > 0)
            {
                int randomStat = Random.Range(0, MAX_SKILL_STAT + 1);
                int idx = Random.Range(0, 3);

                if (skillStat[idx] < MAX_SKILL_STAT && randomStat != 0)
                {
                    if (randomStat > skillStatPoint)
                        continue;
                    else if (skillStat[idx] + randomStat > MAX_SKILL_STAT)
                    {
                        int temp = MAX_SKILL_STAT - skillStat[idx];
                        skillStat[idx] += temp;
                        skillStatPoint -= temp;
                    }
                    else
                    {
                        skillStat[idx] += randomStat;
                        skillStatPoint -= randomStat;
                    }
                }
            }
            for (int i = 0; i < skillText.Length; i++)
            {
                skillText[i].text = skillStat[i].ToString();
            }

            isStat = false;
            Debug.Log(isStat);
        }
    }
}
