using System.Collections.Generic;
using UnityEditor.SpeedTree.Importer;
using UnityEngine;
using static ProjectilePooler;

public class EnemyAttackOneState : EnemyState
{
    private EnemyAttackSO enemyAttack;
    private int amountFired;
    private Hero hero;
    private IProjectilePattern pattern;
    private IProjectileMovement movement;
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
        // gets a random attack from the list
        enemyAttack = enemy.enemyAttackList[enemy.randAttack];

        if (enemyAttack == null )
        {
            Debug.LogError("No enemy attack in list! going back to idle state");
            enemy.enemyStateMachine.ChangeState(enemy.enemyIdleState);
        }

        pattern = enemyAttack.projectilePattern.CreatePattern();
        movement = enemyAttack.projectileMovement.CreateMovement();

        // turns to look at the hero position
        hero = GameObject.FindAnyObjectByType<Hero>();
        Transform heroTransform = hero.transform;

        if (enemyAttack.trackingType == TrackingType.initialTracking)
        {
            Vector3 diff = hero.transform.position - enemy.transform.position;
            diff.Normalize();
            float rot_Z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            enemy.transform.rotation = Quaternion.Euler(0, 0, rot_Z - 180);
        }

        // initial fire 
        pattern.ProjectilePattern(enemy.projectilePooler, enemy.enemyProjectileLauchOffset, movement, enemyAttack.projectileTag, enemyAttack.projectileSpeed, enemyAttack.projectileDamage);
        amountFired++;
        enemy.timer = enemyAttack.fireRate;

    }

    public override void ExitState()
    {
        base.ExitState();
        amountFired = 0;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

/*        if (enemyAttack.trackingType == TrackingType.continuedTracking)
        {
            Vector3 diff = hero.transform.position - enemy.transform.position;
            diff.Normalize();
            float rot_Z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            enemy.transform.rotation = Quaternion.Euler(0, 0, rot_Z - 180);
        }*/

        // do cooldown
        enemy.timer -= Time.deltaTime;

        if (enemy.timer < 0)
        {
            // instantiates then resets the timer so it can fire again.
            if (enemyAttack.trackingType == TrackingType.continuedTracking)
            {
                Vector3 diff = hero.transform.position - enemy.transform.position;
                diff.Normalize();
                float rot_Z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                enemy.transform.rotation = Quaternion.Euler(0, 0, rot_Z - 180);
            }
            pattern.ProjectilePattern(enemy.projectilePooler, enemy.enemyProjectileLauchOffset, movement, enemyAttack.projectileTag, enemyAttack.projectileSpeed, enemyAttack.projectileDamage);
            enemy.timer = enemyAttack.fireRate;
            amountFired++;
        }

        if (amountFired == enemyAttack.fireAmount)
        {
            enemy.enemyStateMachine.ChangeState(enemy.enemyIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
