using UnityEngine;

public class EnemyAttackOneState : EnemyState
{
    private ProjectileType _projectileType;
    private int amountFired;
    public EnemyAttackOneState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();
        _projectileType = enemy.projectileTypes[enemy.randAttack];

        // initial fire 
        GameObject.Instantiate(_projectileType.projectileBase, enemy.enemyProjectileLauchOffset.position, enemy.enemyProjectileLauchOffset.rotation);
        amountFired++;
        enemy.timer = _projectileType.fireRate;

    }

    public override void ExitState()
    {
        base.ExitState();
        amountFired = 0;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
        enemy.timer -= Time.deltaTime;

        if (enemy.timer < 0)
        {
            // instantiates then resets the timer so it can fire again.
            GameObject.Instantiate(_projectileType.projectileBase, enemy.enemyProjectileLauchOffset.position, enemy.enemyProjectileLauchOffset.rotation);
            enemy.timer = _projectileType.fireRate;
            amountFired++;
        }

        if (amountFired == _projectileType.fireAmount)
        {
            enemy.enemyStateMachine.ChangeState(enemy.enemyIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
