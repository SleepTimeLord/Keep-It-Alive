using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float attackCooldownTimer;
    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();
        // starts timer before it moves to another scene
        enemy.timer = enemy.attackCooldown;
        enemy.m_Renderer.sprite = enemy.idle;
        // gets random int to switch to a specific scene
        if (enemy.currentPosition == CurrentPosition.middle)
        {
            enemy.randAttack = Random.Range(0,enemy.enemyAttackListMiddle.Length);
        }
        else
        {
            enemy.randAttack = Random.Range(0, enemy.enemyAttackList.Length);
        }

    }

    public override void ExitState()
    {
        base.ExitState();

        enemy.damageCount = 0;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.timer -= Time.deltaTime;

        if (enemy.timer < 0 )
        {
            enemy.enemyStateMachine.ChangeState(enemy.enemyAttackOneState);
        }

        // go to teleport state if get hit 2 times
        if (enemy.damageCount >= 2)
        {
            enemy.enemyStateMachine.ChangeState(enemy.enemyTeleportState);
            enemy.damageCount = 0;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
