using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerController : MonoBehaviour, IController
{
    Rigidbody otherRigidbody;
    Animator otherAnimator;
    AudioSource otherAudio;

    private Position worldPos;

    public Vector3 battlePosition
    {
        get;
        set;
    }

    void Awake()
    {
        otherRigidbody = GetComponent<Rigidbody>();
        otherAnimator = GetComponent<Animator>();
        otherAudio = GetComponent<AudioSource>();

        worldPos = new Position();
    }

    void FixedUpdate()
    {
        if (this.transform.position.y < -2)
            this.transform.position = new Vector3(this.transform.position.x, -2f, this.transform.position.z);
    }

    void OnCollisionEnter(Collision collider)
    {
        otherRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void MoveBattlePosition()
    {
        StartCoroutine(MovePositionCoroutine(worldPos.otherBattlePosition));
    }

    public void MoveReadyPosition()
    {
        StartCoroutine(MovePositionCoroutine(worldPos.otherReadyPosition));
    }

    public void OtherAttackAnimation()
    {
        otherAnimator.SetInteger("WeaponType_int", 12);
        otherAnimator.SetInteger("MeleeType_int", 1);
        otherAnimator.SetTrigger("AttackTrigger");
    }

    public void OtherFirstSkillAnimation()
    {
        otherAnimator.SetInteger("WeaponType_int", 12);
        otherAnimator.SetInteger("MeleeType_int", 2);
        otherAnimator.SetTrigger("AttackTrigger");
    }

    public void OtherSecondSkillAnimation()
    {
        OtherFirstSkillAnimation();
        otherRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        otherRigidbody.AddForce(new Vector3(-5f, 5f, 0f), ForceMode.Impulse);
        otherAnimator.SetTrigger("JumpTrigger");
    }

    public void OtherThirdSkillAnimation()
    {
        otherAnimator.SetInteger("WeaponType_int", 10);
        otherAnimator.SetTrigger("HealTrigger");
    }

    public void OtherEvadeAnimation()
    {
        Debug.Log("Evade");
        StartCoroutine("EvadeAnimationCoroutine");
    }

    public void OtherHitAnimation()
    {
        StartCoroutine("HitAnimationCoroutine");
    }

    public void PlayDeadAnimation()
    {
        otherAnimator.SetBool("Crouch_b", true);
        otherAnimator.SetBool("Death_b", true);
        otherAnimator.SetInteger("DeathType_int", 2);
    }

    IEnumerator MovePositionCoroutine(Vector3 dest)
    {
        otherAnimator.SetFloat("Speed_f", 1.0f);
        otherAnimator.SetBool("Static_b", false);

        //move to ready position
        if (this.transform.position.x < dest.x)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            for (float xPos = this.transform.position.x; xPos < dest.x; xPos += 4 * Time.deltaTime)
            {
                this.transform.position = new Vector3(xPos, this.transform.position.y, this.transform.position.z);
                yield return null;
            }
            this.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
        //move to battle position
        else
        {
            for (float xPos = this.transform.position.x; xPos > dest.x; xPos -= 4 * Time.deltaTime)
            {
                this.transform.position = new Vector3(xPos, this.transform.position.y, this.transform.position.z);
                yield return null;
            }
        }

        otherAnimator.SetFloat("Speed_f", 0f);
        otherAnimator.SetBool("Static_b", true);
        BattleManager.instance.otherPlayerReady = true;
    }

    IEnumerator EvadeAnimationCoroutine()
    {
        otherRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        otherRigidbody.AddForce(new Vector3(5f, 5f, 0f), ForceMode.Impulse);
        otherAnimator.SetTrigger("JumpTrigger");

        yield return new WaitForSeconds(2.0f);

        StartCoroutine(MovePositionCoroutine(worldPos.otherBattlePosition));
    }

    IEnumerator HitAnimationCoroutine()
    {
        otherAnimator.SetBool("Crouch_b", true);

        yield return new WaitForSeconds(0.2f);

        otherAnimator.SetBool("Crouch_b", false);
    }
}