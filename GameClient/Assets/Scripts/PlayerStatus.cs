using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    //���콺 ��ĥ �� ����� ���� ���� �̹���
    private GameObject infoImage;

    private Slider healthCircle;
    //�ٷ�
    private int strength;
    //���߷�
    private int agility;
    //�Ϸ�
    private int defence;
    //ü��
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
            else
                return 0;
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
        }
    }
    void Awake()
    {
        StatManager statManager = GameObject.Find("StatManager").GetComponent<StatManager>();

        for (int i = 0; i < 4; i++)
        {
            this[i] = statManager.stat[i];
        }

        Debug.Log(this[0] + " " + this[1] + " " + this[2] + " " + this[3]);

        hp = 20 + 5 * vitality;

        healthCircle = GetComponentInChildren<Slider>();

        healthCircle.maxValue = hp;
        healthCircle.value = hp;

    }

    void OnMouseEnter()
    {

    }

    void OnMouseExit()
    {

    }

    public void PlayerDamaged(int damage)
    {
        hp -= damage;
        healthCircle.value = hp;
        Debug.Log(hp);
        if (hp <= 0)
        {
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



    void PrintPlayerInfo()
    {

    }

}