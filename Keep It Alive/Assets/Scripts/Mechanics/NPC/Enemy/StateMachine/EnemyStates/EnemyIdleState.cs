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
        // gets random int to switch to a specific scene
        enemy.randAttack = Random.Range(0, enemy.enemyAttackList.Length);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.timer -= Time.deltaTime;

        if (enemy.timer < 0 )
        {
            enemy.enemyStateMachine.ChangeState(enemy.enemyAttackOneState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
