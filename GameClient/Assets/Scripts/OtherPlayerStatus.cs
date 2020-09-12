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
            else
                return 0;
        }
    }
}