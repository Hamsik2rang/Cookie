using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    enum BattlePhase { ready, setPlayer, setOtherPlayer, go, play };
    enum FirstActor { player, otherPlayer };
    enum ActNumber { playerAttack, playerSkill_1, playerSkill_2, playerSkill_3, otherAttack, otherSkill_1, otherSkill_2, otherSkill_3 };

    public static BattleManager instance = null;
    public bool playerReady;
    public bool otherPlayerReady;
    public bool readyForSet;
    private bool canNextAction;

    private PlayerStatus playerStatus;
    private OtherPlayerStatus otherPlayerStatus;
    private BattlePhase battlePhase;
    private FirstActor whoIsFirst;
    private ActNumber?[] actParam;

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
        actParam = new ActNumber?[2];

        playerReady = false;
        otherPlayerReady = false;
        canNextAction = true;
        battlePhase = BattlePhase.ready;

        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        otherPlayerStatus = GameObject.FindGameObjectWithTag("OtherPlayer").GetComponent<OtherPlayerStatus>();

        DontDestroyOnLoad(this);

        DecideFirstActor();
    }

    void Update()
    {
        if (canNextAction)
            SetNextSequance();
        // else if (canNextAction && battlePhase > BattlePhase.play)
        // {
        //     battlePhase = BattlePhase.ready;

        // }
        else if (((battlePhase == BattlePhase.ready) || (battlePhase == BattlePhase.go)) && (playerReady && otherPlayerReady))
        {
            Next();
        }
    }


    void SetNextSequance()
    {
        canNextAction = false;


        if (battlePhase == BattlePhase.ready)
        {

            GetReadyForBattle();
        }

        else if (battlePhase == BattlePhase.setPlayer)
        {
            SetPlayerAct();
        }

        else if (battlePhase == BattlePhase.setOtherPlayer)
        {
            SetOtherPlayerAct();
        }

        else if (battlePhase == BattlePhase.go)
        {
            GoForBattle();
        }
        else if (battlePhase == BattlePhase.play)
        {
            SetSequence();
            StartCoroutine("PlaySequenceCoroutine");
        }
    }

    void GetReadyForBattle()
    {
        BroadcastMessage("MoveReadyPosition");
    }


    void SetPlayerAct()
    {
        GameObject selectUI;
        try
        {
            selectUI = GameObject.Find("Canvas").transform.Find("SelectImage").gameObject;
            if (selectUI == null)
                throw new System.Exception();
            selectUI.SetActive(true);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    void FinishSetting()
    {
        GameObject selectUI = GameObject.Find("Canvas").transform.Find("SelectImage").gameObject;
        selectUI.SetActive(false);
        Invoke("Next", 2);
    }


    void SetOtherPlayerAct()
    {
        GameObject selectUI = GameObject.Find("Canvas").transform.Find("SelectImage").gameObject;
        selectUI.SetActive(true);
    }


    void DecideFirstActor()
    {

        int playerAgility = playerStatus[1];
        int otherPlayerAgility = otherPlayerStatus[1];

        whoIsFirst = (playerAgility > otherPlayerAgility) ? FirstActor.player : (playerAgility < otherPlayerAgility) ? FirstActor.otherPlayer : (Random.Range(0, 2) == 0) ? FirstActor.player : FirstActor.otherPlayer;
    }

    void AddAttackSequence()
    {
        if (actParam[0] == null)
        {
            actParam[0] = ActNumber.playerAttack;
        }
        else
        {
            actParam[1] = ActNumber.otherAttack;
        }
    }

    void AddFirstSkillSequence()
    {
        if (actParam[0] == null)
        {
            actParam[0] = ActNumber.playerSkill_1;
        }
        else
        {
            actParam[1] = ActNumber.otherSkill_1;
        }
    }

    void AddSecondSkillSequence()
    {
        if (actParam[0] == null)
        {
            actParam[0] = ActNumber.playerSkill_2;
        }
        else
        {
            actParam[1] = ActNumber.otherSkill_2;
        }
    }

    void AddThirdSkillSequence()
    {
        if (actParam[0] == null)
        {
            actParam[0] = ActNumber.playerSkill_3;
        }
        else
        {
            actParam[1] = ActNumber.otherSkill_3;
        }
    }


    void SetSequence()
    {
        if (whoIsFirst == FirstActor.otherPlayer)
        {
            ActNumber? temp = actParam[0];
            actParam[0] = actParam[1];
            actParam[1] = temp;
        }
    }

    void GoForBattle()
    {
        BroadcastMessage("MoveBattlePosition");
    }

    public void Next()
    {
        canNextAction = true;
        playerReady = false;
        otherPlayerReady = false;
        battlePhase++;
        if (battlePhase > BattlePhase.play)
            battlePhase = BattlePhase.ready;
    }

    void PlayerAttack()
    {
        StartCoroutine("PlayerAttackCoroutine");
    }

    void PlayerFirstSkill()
    {
        StartCoroutine("PlayerFirstSkillCoroutine");
    }

    void PlayerSecondSkill()
    {
        StartCoroutine("PlayerSecondSkillCoroutine");
    }
    void PlayerThirdSkill()
    {
        StartCoroutine("PlayerThirdSkillCoroutine");
    }
    void OtherAttack()
    {
        StartCoroutine("OtherAttackCoroutine");
    }
    void OtherFirstSkill()
    {
        StartCoroutine("OtherFirstSkillCoroutine");
    }
    void OtherSecondSkill()
    {
        StartCoroutine("OtherSecondSkillCoroutine");
    }
    void OtherThirdSkill()
    {
        StartCoroutine("OtherThridSkillCoroutine");
    }

    void EndTurn()
    {
        GameManager.instance.AddTurnCount();
        GetReadyForBattle();
    }

    IEnumerator PlayerAttackCoroutine()
    {
        BroadcastMessage("PlayerAttackAnimation");
        bool canEvade = false;

        if (otherPlayerStatus[1] * 7 >= Random.Range(0, 101))
        {
            canEvade = true;
        }

        yield return new WaitForSeconds(0.4f);

        if (canEvade)
        {
            BroadcastMessage("OtherEvadeAnimation");
        }
        else
        {
            BroadcastMessage("OtherHitAnimation");
            int damage = (int)(0.9f * (playerStatus[0] - otherPlayerStatus[2]) - (0.5f * otherPlayerStatus[2]) + 10 + Random.Range(-3, 4));
            otherPlayerStatus.OtherPlayerDamaged(damage);
        }

    }

    IEnumerator PlayerFirstSkillCoroutine()
    {
        BroadcastMessage("PlayerFirstSkillAnimation");
        bool canEvade = false;
        if (otherPlayerStatus[1] * 7 >= Random.Range(0, 101))
        {
            canEvade = true;
        }
        if (canEvade)
        {
            BroadcastMessage("OtherEvadeAnimation");
        }

        yield return new WaitForSeconds(0.2f);

        if (!canEvade)
        {
            BroadcastMessage("OtherHitAnimation");
            int damage = (int)(1.2 * playerStatus[0] + 10 + Random.Range(-3, 4));
            otherPlayerStatus.OtherPlayerDamaged(damage);
        }
    }
    IEnumerator PlayerSecondSkillCoroutine()
    {
        BroadcastMessage("PlayerAttackAnimation");
        bool canEvade = false;
        if (otherPlayerStatus[1] * 7 >= Random.Range(0, 101))
        {
            canEvade = true;
        }

        if (canEvade)
        {
            BroadcastMessage("OtherEvadeAnimation");
            yield return new WaitForSeconds(0.2f);
            BroadcastMessage("PlayerSecondSkillAnimation");
            yield return new WaitForSeconds(0.2f);
            BroadcastMessage("OtherHitAnimation");
            int damage = playerStatus[0] + 12;
            otherPlayerStatus.OtherPlayerDamaged(damage);
        }

        yield return new WaitForSeconds(0.2f);

        if (!canEvade)
        {
            BroadcastMessage("OtherHitAnimation");
            int damage = (int)(0.5 * playerStatus[0] + 1);
            otherPlayerStatus.OtherPlayerDamaged(damage);
        }
    }

    IEnumerator PlayerThirdSkillCoroutine()
    {
        BroadcastMessage("PlayerThirdSkillAnimation");
        yield return new WaitForSeconds(0.2f);
        int lostHp = 20 + playerStatus[3] - playerStatus[4];
        int heal = (int)((0.4f * lostHp) + (0.7f * playerStatus[3]));
        heal *= -1;
        playerStatus.PlayerDamaged(heal);
    }

    IEnumerator OtherAttackCoroutine()
    {
        BroadcastMessage("OtherAttackAnimation");
        bool canEvade = false;

        if (playerStatus[1] * 7 >= Random.Range(0, 101))
        {
            canEvade = true;
        }

        yield return new WaitForSeconds(0.4f);

        if (canEvade)
        {
            BroadcastMessage("PlayerEvadeAnimation");
        }

        else
        {
            BroadcastMessage("PlayerHitAnimation");
            int damage = (int)(0.9f * (otherPlayerStatus[0] - playerStatus[2]) - (0.5f * playerStatus[2]) + 10 + Random.Range(-3, 4));
            playerStatus.PlayerDamaged(damage);
        }
    }

    IEnumerator OtherFirstSkillCoroutine()
    {
        BroadcastMessage("OtherFirstSkillAnimation");
        bool canEvade = false;
        if (otherPlayerStatus[1] * 7 >= Random.Range(0, 101))
        {
            canEvade = true;
        }
        if (canEvade)
        {
            BroadcastMessage("PlayerEvadeAnimation");
        }

        yield return new WaitForSeconds(0.2f);

        if (!canEvade)
        {
            BroadcastMessage("PlayerHitAnimation");
            int damage = (int)(1.2 * otherPlayerStatus[0] + 10 + Random.Range(-3, 4));
            playerStatus.PlayerDamaged(damage);
        }
    }

    IEnumerator OtherSecondSkillCoroutine()
    {
        BroadcastMessage("OtherAttackAnimation");
        bool canEvade = false;
        if (playerStatus[1] * 7 >= Random.Range(0, 101))
        {
            canEvade = true;
        }

        if (canEvade)
        {
            BroadcastMessage("PlayerEvadeAnimation");
            yield return new WaitForSeconds(0.2f);
            BroadcastMessage("OtherSecondSkillAnimation");
            yield return new WaitForSeconds(0.2f);
            BroadcastMessage("PlayerHitAnimation");
            int damage = otherPlayerStatus[0] + 12;
            playerStatus.PlayerDamaged(damage);
        }

        yield return new WaitForSeconds(0.2f);

        if (!canEvade)
        {
            BroadcastMessage("PlayerHitAnimation");
            int damage = (int)(0.5 * otherPlayerStatus[0] + 1);
            playerStatus.PlayerDamaged(damage);
        }
    }

    IEnumerator OtherThridSkillCoroutine()
    {
        BroadcastMessage("OtherThirdSkillAnimation");
        yield return new WaitForSeconds(0.2f);
        int lostHp = 20 + otherPlayerStatus[3] - otherPlayerStatus[4];
        int heal = (int)((0.4f * lostHp) + (0.7f * otherPlayerStatus[3]));
        heal *= -1;
        otherPlayerStatus.OtherPlayerDamaged(heal);
    }

    IEnumerator PlaySequenceCoroutine()
    {
        for (int i = 0; i < 2; i++)
        {
            switch (actParam[i])
            {
                case ActNumber.playerAttack:
                    PlayerAttack();
                    break;
                case ActNumber.playerSkill_1:
                    PlayerFirstSkill();
                    break;
                case ActNumber.playerSkill_2:
                    PlayerSecondSkill();
                    break;
                case ActNumber.playerSkill_3:
                    PlayerThirdSkill();
                    break;
                case ActNumber.otherAttack:
                    OtherAttack();
                    break;
                case ActNumber.otherSkill_1:
                    OtherFirstSkill();
                    break;
                case ActNumber.otherSkill_2:
                    OtherSecondSkill();
                    break;
                case ActNumber.otherSkill_3:
                    OtherThirdSkill();
                    break;
                default:
                    break;
            }
            actParam[i] = null;

            yield return new WaitForSeconds(2.0f);
        }

        Next();
        EndTurn();
    }
}