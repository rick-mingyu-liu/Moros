using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrolState : BossState
{
    private BossController bossController;
    private BossStateMachine stateMachine;

    public BossPatrolState(BossController controller, BossStateMachine machine)
    {
        this.bossController = controller;
        this.stateMachine = machine;
    }

    public override void OnEnter()
    {
        bossController.animator.SetBool("isWalking", true);
        bossController.animator.SetBool("isIdle", false);
    }

    public override void Update()
    {
        bossController.Patrol();


        if (bossController.IsPlayerInRange())
        {
            stateMachine.ChangeState(new BossLookForPlayerState(bossController, stateMachine));
        }
    }

    public override void OnExit()
    {
        bossController.animator.SetBool("isWalking", false);
    }
}
