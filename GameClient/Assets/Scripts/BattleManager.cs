using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance = null;
    public Vector3 playerBattlePosition;
    public Vector3 otherPlayerBattlePosition;

    private GameObject player;
    private GameObject otherPlayer;


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

        player = GameObject.FindGameObjectWithTag("Player");
        otherPlayer = GameObject.FindGameObjectWithTag("OtherPlayer");

        Vector3 distance = player.transform.position - otherPlayer.transform.position;

        playerBattlePosition = distance + distance.normalized;
        otherPlayerBattlePosition = distance - distance.normalized;
    }


    void GetReadyForBattle()
    {
        BroadcastMessage("MoveReadyPosition");
    }

	void GoBattlePosition(){
        BroadcastMessage("MoveBattlePosition");
    }
}
