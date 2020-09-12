using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //�ٷ�
    private int strength;
    //���߷�
    private int agility;
    //�Ϸ�
    private int defence;
    //ü��
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
