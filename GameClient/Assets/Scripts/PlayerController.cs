using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IController
{
    Rigidbody playerRigidbody;
    Animator playerAnimator;
    AudioSource playerAudio;

    public Vector3 readyPosition
    {
        get;
        set;
    }
    public Vector3 battlePosition
    {
        get;
        set;
    }
    public Vector3 normalDirectionVector
    {
        get;
        private set;
    }
    Vector3 frontNormalVector;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        normalDirectionVector = Vector3.Cross(new Vector3(this.transform.position.x, 0, 0), new Vector3(0, this.transform.position.y, 0));  //외적으로 frontVector 계산
        readyPosition = this.transform.position;

        frontNormalVector = new Vector3(0, 0, 1);
        readyPosition = this.transform.localPosition;

    }
    public void MoveBattlePosition()
    {
        StartCoroutine(MovePositionCoroutine(readyPosition, battlePosition));
    }

    public void MoveReadyPosition()
    {
        this.transform.position = readyPosition;
        this.battlePosition = battlePosition;
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


    IEnumerator MovePositionCoroutine(Vector3 start, Vector3 dest)
    {
        playerAnimator.SetFloat("Speed_f", 1.0f);
        playerAnimator.SetBool("Static_b", false);

        for (float i = start.z; i < dest.z; i += Time.deltaTime)
        {
            Vector3 temp = new Vector3(0, 0, i);
            this.transform.position += temp;
            yield return null;
        }

        playerAnimator.SetFloat("Speed_f", 0f);
        playerAnimator.SetBool("Static_b", true);
    }
}
