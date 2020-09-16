using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InfoLoader : MonoBehaviour
{
    delegate int[] getPrimeStats(int[] stats);

    public static InfoLoader instance
    {
        get;
        private set;
    } = null;
    private Text objectName;
    private Text healthPoint;
    private Text maxHealthPoint;
    private Image firstPrimeStatImage;
    private Image secondPrimeStatImage;

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

    }

    public void PrintInfo<T>(T printedObject)
    {
        getPrimeStats = (printedObject) =>
        {
            int[] primeStats = new int[2];
            int first = printedObject[0];
            int second = printedObject[0];
            for (int i = 1; i < 4; i++)
            {
                if (second < printedObject[i])
                {
                    second = printedObject[i];
                    if (first < second)
                    {
                        int temp = first;
                        first = second;
                        second = temp;
                    }
                }
            }
            primeStats[0] = first;
            primeStats[1] = second;
            return primeStats;
        };
    }

}
