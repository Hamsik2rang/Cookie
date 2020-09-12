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
        //�ı� ����
        DontDestroyOnLoad(this);
        //�����ϸ� ĳ���͵� ��ǥ�� �غ� ���������� �̵�
        GetReadyForBattle();
    }

    //Ư�� �޼��尡 ����Ǿ��� �� ���ϴ� ready���� �������� ������ ����
    void CheckReady()
    {
        //�÷��̾�, ��� �÷��̾� �� �� �غ� �� �� ���?
        if (!playerReady && !otherPlayerReady)
        {
            //�÷��̾ act select�� ����
            if (readyForSet)
            {
                SetPlayerAct();
                //TODO:���Ŀ� ȣ���� �޼��忡�� playerReady true�� set�ؾ���
            }
            //ReadyPos�� �̵��� �� �� ���
            else
            {
                GetReadyForBattle();
            }
        }
        //�÷��̾ �غ�� ���
        else if (playerReady && !otherPlayerReady)
        {
            //�÷��̾� set, ��� �÷��̾� unset
            if (readyForSet)
            {
                SetOtherPlayerAct();
            }
            //��븸 ready pos�� �̵� ���� ���
            else
            {

            }
        }
        else if (playerReady && otherPlayerReady)
        {
            //�÷��̾�,����÷��̾� �� �� ready pos�� �̵��� ���
            if (readyForSet)
            {

            }
            //�÷��̾�, ��� �÷��̾� �� �� selected act�� ���
            else
            {
                readyForSet = true;
            }
        }
    }
    //Battle Scene �غ� �޼���. ĳ���͵��� �غ� ���������� �̵���Ŵ
    void GetReadyForBattle()
    {
        battlePhase = BattlePhase.ready;
        Debug.Log("GetReady!");
        BroadcastMessage("MoveReadyPosition");
        CheckReady();
    }

    //�÷��̾� �ൿ ���� ���� �޼���. ���� ��ư Ȱ��ȭ
    void SetPlayerAct()
    {
        GameObject selectUI = GameObject.Find("SelectButton");
        selectUI.SetActive(true);


    }

    void SetOtherPlayerAct()
    {

    }

    //���İ� ���� �޼���
    void DecideFirstActor()
    {

        //�÷��̾�� ��� �÷��̾��� Agility�� �ε����� �޾ƿ�
        int playerAgility = this.transform.GetComponentInChildren<PlayerStatus>()[1];
        int otherPlayerAgility = this.transform.GetComponentInChildren<OtherPlayerStatus>()[1];
        //���� �Ŀ� ���İ� ����
        whoIsFirst = (playerAgility > otherPlayerAgility) ? FirstActor.player : (playerAgility < otherPlayerAgility) ? FirstActor.otherPlayer : (Random.Range(0, 2) == 0) ? FirstActor.player : FirstActor.otherPlayer;
    }

    //���� ������� �̺�Ʈ ����
    void SetPlaySequence()
    {
        //������ �÷��̾��� ���
        if (whoIsFirst == FirstActor.player)
        {

        }
        //������ ��� �÷��̾��� ���
        else
        {

        }
    }

    //���� ���� �޼���. ĳ���͵��� ���� ���������� �̵���Ŵ
    void GoForBattle()
    {
        BroadcastMessage("MoveBattlePosition");
    }




    //�� ���� �޼���
    void EndTurn()
    {
        //�� ī��Ʈ 1 ����
        GameManager.instance.AddTurnCount();
        //���� �� �غ�
        GetReadyForBattle();
    }
}