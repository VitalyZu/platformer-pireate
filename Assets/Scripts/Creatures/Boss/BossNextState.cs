using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNextState : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var spawner = animator.GetComponent<CircularProjectileSpawner>();
        spawner.Stage++;

        var changeColor = animator.GetComponent<ChangeLightComponent>();
        changeColor.SetColor();
    }
}
