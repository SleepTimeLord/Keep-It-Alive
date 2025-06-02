using System.Collections.Generic;
using UnityEditor.SpeedTree.Importer;
using UnityEngine;

public class EnemyAttackOneState : EnemyState
{
    private ProjectileType _projectileType;
    private int amountFired;
    private Hero hero;
    private Queue<GameObject> pool = new Queue<GameObject>();
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

        // turns to look at the hero position
        hero = GameObject.FindAnyObjectByType<Hero>();
        Transform heroTransform = hero.transform;

/*        if (_projectileType.aimType== AimType.intialFollow)
        {
            Vector3 diff = hero.transform.position - enemy.transform.position;
            diff.Normalize();
            float rot_Z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            enemy.transform.rotation = Quaternion.Euler(0, 0, rot_Z - 180);
        }*/

        // initial fire 
/*        GameObject.Instantiate(_projectileType.projectile.transform, enemy.enemyProjectileLauchOffset.position, enemy.enemyProjectileLauchOffset.rotation);
        amountFired++;
        enemy.timer = _projectileType.fireRate;*/

    }

    public override void ExitState()
    {
        base.ExitState();
        amountFired = 0;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
/*
        if (_projectileType.aimType == AimType.continuousFollow)
        {
            Vector3 diff = hero.transform.position - enemy.transform.position;
            diff.Normalize();
            float rot_Z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            enemy.transform.rotation = Quaternion.Euler(0, 0, rot_Z - 180);
        }

        // do cooldown
        enemy.timer -= Time.deltaTime;

        if (enemy.timer < 0)
        {
            // instantiates then resets the timer so it can fire again.
            if (_projectileType.aimType == AimType.continuousFollow)
            {
                Vector3 diff = hero.transform.position - enemy.transform.position;
                diff.Normalize();
                float rot_Z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                enemy.transform.rotation = Quaternion.Euler(0, 0, rot_Z - 180);
            }
            GameObject.Instantiate(_projectileType.projectile.transform, enemy.enemyProjectileLauchOffset.position, enemy.enemyProjectileLauchOffset.rotation);
            enemy.timer = _projectileType.fireRate;
            amountFired++;
        }

        if (amountFired == _projectileType.fireAmount)
        {
            enemy.enemyStateMachine.ChangeState(enemy.enemyIdleState);
        }*/
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
