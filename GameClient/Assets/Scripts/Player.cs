using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;

    public int[] status;
    public int hp;
    public int[] skillCount;

    public void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
