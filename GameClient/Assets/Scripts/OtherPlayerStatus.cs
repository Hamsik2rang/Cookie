using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerStatus : MonoBehaviour
{
    //근력
    private int strength;
    //순발력
    private int agility;
    //완력
    private int defence;
    //체력
    private int vitality;
    //hp
    public int hp;

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
        RandomStat();
        hp = 20 + 5 * vitality;
        Debug.Log(this[0] + " " + this[1] + " " + this[2] + " " + this[3]);

    }

    void RandomStat()
    {
        int point = 25;

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
    }
}