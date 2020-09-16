using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IController
{
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private AudioSource playerAudio;

    private Position worldPos;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        worldPos = new Position();
    }

    // void FixedUpdate()
    // {
    //     if (this.transform.position.y < 0)
    //         this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
    // }

    void OnCollisionEnter(Collision collider)
    {

        this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
        playerRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void MoveBattlePosition()
    {
        StartCoroutine(MovePositionCoroutine(worldPos.playerBattlePosition));
    }

    public void MoveReadyPosition()
    {
        StartCoroutine(MovePositionCoroutine(worldPos.playerReadyPosition));
    }

    public void PlayerAttackAnimation()
    {
        playerAnimator.SetInteger("WeaponType_int", 12);
        playerAnimator.SetInteger("MeleeType_int", 1);
        playerAnimator.SetTrigger("AttackTrigger");
    }

    public void PlayerFirstSkillAnimation()
    {
        playerAnimator.SetInteger("WeaponType_int", 12);
        playerAnimator.SetInteger("MeleeType_int", 2);
        playerAnimator.SetTrigger("AttackTrigger");
    }

    public void PlayerSecondSkillAnimation()
    {
        StopCoroutine("PlayerAttackAnimation");
        PlayerFirstSkillAnimation();
        playerRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        playerRigidbody.AddForce(new Vector3(5f, 5f, 0f), ForceMode.Impulse);
        playerAnimator.SetTrigger("JumpTrigger");
    }

    public void PlayerThirdSkillAnimation()
    {
        playerAnimator.SetInteger("WeaponType_int", 10);
        playerAnimator.SetTrigger("HealTrigger");
    }

    public void PlayerEvadeAnimation()
    {
        Debug.Log("Evade");
        StartCoroutine("EvadeAnimationCoroutine");
    }
    public void PlayerHitAnimation()
    {
        playerAnimator.SetTrigger("CrouchTrigger");
    }
    public void PlayDeadAnimation()
    {
        playerAnimator.SetBool("Death_b", true);
        playerAnimator.SetInteger("DeathType_int", 2);

        playerAnimator.SetTrigger("CrouchTrigger");

        GameManager.instance.isGameOver = true;
    }

    IEnumerator MovePositionCoroutine(Vector3 dest)
    {
        playerAnimator.SetFloat("Speed_f", 1.0f);
        playerAnimator.SetBool("Static_b", false);

        //move to battle position
        if (this.transform.position.x < dest.x)
        {
            for (float xPos = this.transform.position.x; xPos < dest.x; xPos += 4 * Time.deltaTime)
            {
                this.transform.position = new Vector3(xPos, this.transform.position.y, this.transform.position.z);
                yield return null;
            }
        }
        //move to ready position
        else
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            for (float xPos = this.transform.position.x; xPos > dest.x; xPos -= 4 * Time.deltaTime)
            {
                this.transform.position = new Vector3(xPos, this.transform.position.y, this.transform.position.z);
                yield return null;
            }
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }

        playerAnimator.SetFloat("Speed_f", 0f);
        playerAnimator.SetBool("Static_b", true);

        yield return new WaitForSeconds(0.2f);

        BattleManager.instance.playerReady = true;
    }

    IEnumerator EvadeAnimationCoroutine()
    {
        playerRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        playerRigidbody.AddForce(new Vector3(0f, 5f, 0f), ForceMode.Impulse);
        playerAnimator.SetTrigger("JumpTrigger");

        yield return new WaitForSeconds(2f);
    }
}
