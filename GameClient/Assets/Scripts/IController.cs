using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    void MoveBattlePosition();
    void MoveReadyPosition();
    void PlayAttackAnimation();
    void PlayEvadeAnimation();
    void PlayDeadAnimation();

}
