using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherPlayerStatus : MonoBehaviour, IStatus
{
    public Text GameOverText;
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

    int[] skill;

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
        RandomStat();
        hp = 20 + 5 * vitality;

        Debug.Log("other : " + this[0] + " " + this[1] + " " + this[2] + " " + this[3] + "//" + this[5] + " " + this[6] + " " + this[7]);

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

    void RandomStat()
    {
        int point = 25, skillPoint = 5;

        while (point > 0)
        {
            int rand = Random.Range(0, 4);
            if (this[rand] < 10)
            {
                this[rand]++;
                point--;
            }
            else
                continue;
        }
        while (skillPoint > 0)
        {
            int rand = Random.Range(5, 8);
            if (this[rand] < 3)
            {
                this[rand]++;
                skillPoint--;
            }
            else
                continue;
        }
    }

    public void OtherPlayerDamaged(int damage)
    {
        hp -= damage;
        healthCircle.value = hp;
        Debug.Log(hp);
        if (hp <= 0)
        {
            GameOverText.text = "적 사망";
            OtherPlayerDead();
        }
        else if (hp > healthCircle.maxValue)
        {
            hp = (int)healthCircle.maxValue;
        }
    }

    void OtherPlayerDead()
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