using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetectedState : BossState
{
    private BossController bossController;
    private BossStateMachine stateMachine;
    private float detectionDuration = 2f;
    private float timer;

    public BossDetectedState(BossController controller, BossStateMachine machine)
    {
        this.bossController = controller;
        this.stateMachine = machine;
        timer = 0f;
    }

    public override void OnEnter()
    {
        bossController.animator.SetBool("isDetected", true);
        timer = 0f;
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (bossController.IsPlayerInRange())
        {
            stateMachine.ChangeState(new BossLookForPlayerState(bossController, stateMachine));
        }
        else if (timer >= detectionDuration)
        {
            stateMachine.ChangeState(new BossPatrolState(bossController, stateMachine));
        }
    }

    public override void OnExit()
    {
        bossController.animator.SetBool("isDetected", false);
    }
}
