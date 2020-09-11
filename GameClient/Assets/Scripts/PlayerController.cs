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

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        normalDirectionVector = Vector3.Cross(new Vector3(this.transform.position.x, 0, 0), new Vector3(0, this.transform.position.y, 0));  //외적으로 frontVector 계산
        readyPosition = this.transform.position;
    }
    public void MoveBattlePosition()
    {
        StartCoroutine("MoveBPositionCoroutine");
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

    IEnumerator MoveBPositionCoroutine()
    {
        playerAnimator.SetFloat("Speed_f", 1.0f);
        playerAnimator.SetBool("Static_b", false);

        for (float i = this.transform.position.z; i < battlePosition.z; i += Time.deltaTime)
        {
            Vector3 temp = new Vector3(0, 0, i);
            this.transform.position += temp;
            yield return null;
        }

        playerAnimator.SetFloat("Speed_f", 0f);
        playerAnimator.SetBool("Static_b", true);
    }
}
