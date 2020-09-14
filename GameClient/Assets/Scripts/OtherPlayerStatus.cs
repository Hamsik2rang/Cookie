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
            if (index == 4)
                hp = value;
        }
    }

    void Awake()
    {
        hp = 20 + 5 * vitality;
        agility = 10;
    }
}