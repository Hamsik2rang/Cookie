using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void EventHandler();

public class BattleManager : MonoBehaviour
{
    enum BattlePhase { ready, setPlayer, setOtherPlayer, play };
    enum FirstActor { player, otherPlayer };

    public static BattleManager instance = null;
    public event EventHandler battleChain;
    public bool playerReady;
    public bool otherPlayerReady;
    public bool readyForSet;

    private BattlePhase battlePhase;
    private FirstActor whoIsFirst;

    void Awake()
    {
        //Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        playerReady = false;
        otherPlayerReady = true;
        //파괴 방지
        DontDestroyOnLoad(this);
        //시작하면 캐릭터들 좌표를 준비 포지션으로 이동
        GetReadyForBattle();
    }

    //특정 메서드가 수행되었을 때 변하는 ready값을 기준으로 시퀀스 진행
    void CheckReady()
    {
        //플레이어, 상대 플레이어 둘 다 준비가 안 된 경우?
        if (!playerReady && !otherPlayerReady)
        {
            //플레이어가 act select할 차례
            if (readyForSet)
            {
                SetPlayerAct();
                //TODO:이후에 호출한 메서드에서 playerReady true로 set해야함
            }
            //ReadyPos로 이동을 안 한 경우
            else
            {
                GetReadyForBattle();
            }
        }
        //플레이어만 준비된 경우
        else if (playerReady && !otherPlayerReady)
        {
            //플레이어 set, 상대 플레이어 unset
            if (readyForSet)
            {
                SetOtherPlayerAct();
            }
            //상대만 ready pos로 이동 안한 경우
            else
            {

            }
        }
        else if (playerReady && otherPlayerReady)
        {
            //플레이어,상대플레이어 둘 다 ready pos로 이동한 경우
            if (readyForSet)
            {

            }
            //플레이어, 상대 플레이어 둘 다 selected act한 경우
            else
            {
                readyForSet = true;
            }
        }
    }
    //Battle Scene 준비 메서드. 캐릭터들을 준비 포지션으로 이동시킴
    void GetReadyForBattle()
    {
        battlePhase = BattlePhase.ready;
        Debug.Log("GetReady!");
        BroadcastMessage("MoveReadyPosition");
        CheckReady();
    }

    //플레이어 행동 결정 시작 메서드. 선택 버튼 활성화
    void SetPlayerAct()
    {
        GameObject selectUI = GameObject.Find("SelectButton");
        selectUI.SetActive(true);


    }

    void SetOtherPlayerAct()
    {

    }

    //선후공 결정 메서드
    void DecideFirstActor()
    {

        //플레이어와 상대 플레이어의 Agility를 인덱서로 받아옴
        int playerAgility = this.transform.GetComponentInChildren<PlayerStatus>()[1];
        int otherPlayerAgility = this.transform.GetComponentInChildren<OtherPlayerStatus>()[1];
        //비교한 후에 선후공 결정
        whoIsFirst = (playerAgility > otherPlayerAgility) ? FirstActor.player : (playerAgility < otherPlayerAgility) ? FirstActor.otherPlayer : (Random.Range(0, 2) == 0) ? FirstActor.player : FirstActor.otherPlayer;
    }

    //전투 순서대로 이벤트 구성
    void SetPlaySequence()
    {
        //선공이 플레이어인 경우
        if (whoIsFirst == FirstActor.player)
        {

        }
        //선공이 상대 플레이어인 경우
        else
        {

        }
    }

    //전투 시작 메서드. 캐릭터들을 전투 포지션으로 이동시킴
    void GoForBattle()
    {
        BroadcastMessage("MoveBattlePosition");
    }




    //턴 종료 메서드
    void EndTurn()
    {
        //턴 카운트 1 증가
        GameManager.instance.AddTurnCount();
        //다음 턴 준비
        GetReadyForBattle();
    }
}