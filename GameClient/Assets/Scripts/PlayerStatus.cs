using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IStatus
{
    private Slider healthCircle;
    //근력
    private int strength;
    //순발력
    private int agility;
    //완력
    private int defence;
    //체력
    private int vitality;
    //hp
    private int hp;

    private int[] skill;

    public Text GameOverText;
    public int this[int index]
    {
        get
        {
            if (index == 0)
                return strength;
            else if (index == 1)
                return agility;
            else if (index == 2)
                return defence;
            else if (index == 3)
                return vitality;
            else if (index == 4)
                return hp;
            else if (index == 5)
                return skill[0];
            else if (index == 6)
                return skill[1];
            else if (index == 7)
                return skill[2];
            else
                return -1;
        }
        set
        {
            if (index == 0)
                strength = value;
            else if (index == 1)
                agility = value;
            else if (index == 2)
                defence = value;
            else if (index == 3)
                vitality = value;
            else if (index == 4)
                hp = value;
            else if (index == 5)
                skill[0] = value;
            else if (index == 6)
                skill[1] = value;
            else if (index == 7)
                skill[2] = value;
        }
    }
    void Awake()
    {
        skill = new int[3];
        StatManager statManager = GameObject.Find("StatManager").GetComponent<StatManager>();

        for (int i = 0; i < 4; i++)
        {
            this[i] = statManager.stat[i];
        }
        for (int i = 0; i < statManager.skillStat.Length; i++)
        {
            this[i + 5] = statManager.skillStat[i];
        }

        Debug.Log("player : " + this[0] + " " + this[1] + " " + this[2] + " " + this[3] + "//" + this[5] + " " + this[6] + " " + this[7]);

        hp = 20 + 5 * vitality;

        healthCircle = GetComponentInChildren<Slider>();

        healthCircle.maxValue = hp;
        healthCircle.value = hp;

    }

    void OnMouseEnter()
    {
        InfoLoader.instance.PrintInfo(this, this[0], this[1], this[2], this[3]);
    }

    void OnMouseExit()
    {
        InfoLoader.instance.PrintInfo(null);
    }

    public void PlayerDamaged(int damage)
    {
        hp -= damage;
        healthCircle.value = hp;
        Debug.Log(hp);
        if (hp <= 0)
        {
            GameOverText.text = "플레이어 사망";
            PlayerDead();
        }
        else if (hp > healthCircle.maxValue)
        {
            hp = (int)healthCircle.maxValue;
        }
    }

    void PlayerDead()
    {
        SendMessage("PlayDeadAnimation");
    }

    public int GetHP()
    {
        return this[4];
    }

    public int GetMaxHP()
    {
        return (int)healthCircle.maxValue;
    }

}