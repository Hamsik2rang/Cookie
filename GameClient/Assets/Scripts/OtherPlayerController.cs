using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerController : MonoBehaviour, IController
{
    Rigidbody otherRigidbody;
    Animator otherAnimator;
    AudioSource otherAudio;

    public Vector3 battlePosition
    {
        get;
        set;
    }

    public void MoveBattlePosition()
    {

    }

    public void MoveReadyPosition()
    {

    }

    public void GetAttack()
    {

    }
    public void GetFirstSkill()
    {

    }

    public void PlayAttackAnimation()
    {

    }

    public void GetSecondSkill()
    {

    }


    public void PlayEvadeAnimation()
    {

    }

    public void GetThirdSkill()
    {

    }

    public void PlayDeadAnimation()
    {

    }
}
