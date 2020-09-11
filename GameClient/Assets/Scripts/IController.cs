using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    void MoveBattlePosition();
    void MoveReadyPosition();
    void GetAttack();
    void GetFirstSkill();
    void GetSecondSkill();
    void GetThirdSkill();
    void PlayDeadAnimation();
}
