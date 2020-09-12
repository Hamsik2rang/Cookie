using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IController
{
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private AudioSource playerAudio;
    private Position worldPos;

    //법선벡터 == 정면 방향벡터
    public Vector3 normalDirectionVector
    {
        get;
        private set;
    }

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        worldPos = new Position();

        //플레이어의 법선벡터(정면벡터) 계산은 좋은데, 이거 그래서 언제쓸거?
        normalDirectionVector = Vector3.Cross(new Vector3(this.transform.position.x, 0, 0), new Vector3(0, this.transform.position.y, 0)).normalized;  //외적으로 frontVector 계산
    }

    public void MoveBattlePosition()
    {
        StartCoroutine(MovePositionCoroutine(worldPos.playerReadyPosition, worldPos.playerBattlePosition));
    }

    public void MoveReadyPosition()
    {
        StartCoroutine(MovePositionCoroutine(this.transform.position, worldPos.playerReadyPosition));
        //his.transform.position = worldPos.playerReadyPosition;
    }

    public void PlayAttackAnimation()
    {

    }

    public void PlayEvadeAnimation()
    {

    }

    public void PlayDeadAnimation()
    {

    }

    public void GetAttack()
    {

    }

    public void GetFirstSkill()
    {

    }

    public void GetSecondSkill()
    {

    }

    public void GetThirdSkill()
    {

    }

    public void GetDamage()
    {

    }

    IEnumerator MovePositionCoroutine(Vector3 start, Vector3 dest)
    {
        playerAnimator.SetFloat("Speed_f", 1.0f);
        playerAnimator.SetBool("Static_b", false);

        Vector3 dirVector = dest - start;

        while (this.transform.position != dest)
        {
            this.transform.position = new Vector3(this.transform.position.x + (dirVector.x * Time.deltaTime), this.transform.position.y, this.transform.position.z);
            yield return null;
        }

        playerAnimator.SetFloat("Speed_f", 0f);
        playerAnimator.SetBool("Static_b", true);

        BattleManager.instance.playerReady = true;
    }
}
