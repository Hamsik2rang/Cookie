using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IController
{
    Rigidbody playerRigidbody;
    Animator playerAnimator;
    AudioSource playerAudio;
    Vector3 readyPosition;
    Vector3 battlePosition;
    Vector3 normalDirectionVector;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        normalDirectionVector = new Vector3(0, 0, 1);
        readyPosition = this.transform.position;
    }
    public void MoveBattlePosition()
    {
        StartCoroutine("MoveBPositionCoroutine");
    }

    public void MoveReadyPosition()
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
