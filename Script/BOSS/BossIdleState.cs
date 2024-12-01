using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    private BossController bossController;
    private BossStateMachine stateMachine;
    private float idleTime = 2f;
    private float timer = 0f;

    public BossIdleState(BossController controller, BossStateMachine machine)
    {
        this.bossController = controller;
        this.stateMachine = machine;
    }

    public override void OnEnter()
    {
        bossController.animator.SetBool("isIdle", true);
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= idleTime)
        {
            bossController.Flip();
            stateMachine.ChangeState(new BossPatrolState(bossController, stateMachine));
        }
    }

    public override void OnExit()
    {
        bossController.animator.SetBool("isIdle", false);
    }
}
