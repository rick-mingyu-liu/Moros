using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeState : BossState
{
    private BossController bossController;
    private BossStateMachine stateMachine;
    private Vector3 targetPosition;
    private float chargeSpeed;

    public BossChargeState(BossController controller, BossStateMachine machine)
    {
        this.bossController = controller;
        this.stateMachine = machine;
        chargeSpeed = bossController.chargeSpeed;
    }

    public override void OnEnter()
    {
        targetPosition = bossController.player.position;
        bossController.animator.SetBool("isCharging", true);
    }

    public override void Update()
    {
        bossController.transform.position = Vector3.MoveTowards(bossController.transform.position, targetPosition, chargeSpeed * Time.deltaTime);

        if (Vector3.Distance(bossController.transform.position, targetPosition) < 0.1f)
        {
            stateMachine.ChangeState(new BossDetectedState(bossController, stateMachine));
        }
    }

    public override void OnExit()
    {
        bossController.animator.SetBool("isCharging", false);
    }
}
