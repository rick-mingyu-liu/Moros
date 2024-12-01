using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLookForPlayerState : BossState
{
    private BossController bossController;
    private BossStateMachine stateMachine;
    private float detectionTime;
    private Vector3 targetPosition;

    public BossLookForPlayerState(BossController controller, BossStateMachine machine)
    {
        this.bossController = controller;
        this.stateMachine = machine;
        detectionTime = 0f;
    }

    public override void OnEnter()
    {
        bossController.animator.SetBool("isLookingForPlayer", true);
        detectionTime = 0f;
        targetPosition = bossController.player.position;

    }

    public override void Update()
    {
        if (bossController.IsPlayerInRange() && 
            ((bossController.transform.position.x - targetPosition.x > 0 && bossController.isFaceRight == true) || 
            (bossController.transform.position.x - targetPosition.x < 0 && bossController.isFaceRight == false)))
        {
            bossController.Flip();
        }

        if (bossController.IsPlayerInRange())
        {
            detectionTime += Time.deltaTime;
            if (detectionTime >= bossController.detectionTimeThreshold)
            {
                stateMachine.ChangeState(new BossChargeState(bossController, stateMachine));
            }
        }
        else
        {
            stateMachine.ChangeState(new BossPatrolState(bossController, stateMachine));
        }
    }

    public override void OnExit()
    {
        bossController.animator.SetBool("isLookingForPlayer", false);
    }
}
