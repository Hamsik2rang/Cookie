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

        //Set Battle Position
        playerBattlePosition = distance + distance.normalized;
        player.GetComponent<PlayerController>().battlePosition = playerBattlePosition;
        otherPlayerBattlePosition = distance - distance.normalized;
        otherPlayer.GetComponent<OtherPlayerController>().battlePosition = otherPlayerBattlePosition;


    }


    void GetReadyForBattle()
    {


    }

    void GoBattlePosition()
    {
        BroadcastMessage("MoveBattlePosition");
    }
}
