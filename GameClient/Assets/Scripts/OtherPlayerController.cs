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

    public void PlayAttackAnimation()
    {

    }

    public void PlayEvadeAnimation()
    {

    }

    public void PlayDeadAnimation()
    {

    }
}
