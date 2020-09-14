using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position
{
    //플레이어의 준비 포지션 좌표
    public Vector3 playerReadyPosition
    {
        get;
        private set;
    }
    //상대 플레이어의 준비 포지션 좌표
    public Vector3 otherReadyPosition
    {
        get;
        private set;
    }
    //플레이어의 전투 포지션 좌표
    public Vector3 playerBattlePosition
    {
        get;
        private set;
    }
    //상대 플레이어의 전투 포지션 좌표
    public Vector3 otherBattlePosition
    {
        get;
        private set;
    }
    //전장의 중앙 좌표
    public Vector3 centerPosition
    {
        get;
        private set;
    }
    //플레이어의 회피 좌표
    public Vector3 playerEvadePosition
    {
        get;
        private set;
    }
    //상대 플레이어의 회피 좌표
    public Vector3 otherEvadePosition
    {
        get;
        private set;
    }

    public Position()
    {
        playerReadyPosition = new Vector3(-11f, 0f, -6f);
        otherReadyPosition = new Vector3(2f, 0f, -6f);
        playerBattlePosition = new Vector3(-5.5f, 0f, 6f);
        otherBattlePosition = new Vector3(-3.5f, 0f, 6);
        centerPosition = new Vector3(-4.5f, 0f, 6f);
        playerEvadePosition = new Vector3(-6.5f, 0f, 6f);
        otherEvadePosition = new Vector3(-2.5f, 0f, 6f);
    }
}
