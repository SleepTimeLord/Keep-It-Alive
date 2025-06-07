using System.Collections.Generic;
using UnityEditor.SpeedTree.Importer;
using UnityEngine;
using UnityEngine.Rendering;
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
        // gets a random attack from the list and changes attack to differnet attack depending on if its on the mdidle 
        // resets the rotation or enemy;
        switch (enemy.currentPosition)
        {
            case CurrentPosition.right:
                enemy.transform.rotation = Quaternion.Euler(0,0,0);
                enemyAttack = enemy.enemyAttackList[enemy.randAttack];
                break;
            case CurrentPosition.left:
                enemy.transform.rotation = Quaternion.Euler(0, 0, -180);
                enemyAttack = enemy.enemyAttackList[enemy.randAttack];
                break;
            case CurrentPosition.middle:
                enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                enemyAttack = enemy.enemyAttackListMiddle[enemy.randAttack];
                break;
        }

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
    }

    public override void ExitState()
    {
        base.ExitState();
        amountFired = 0;
        enemy.damageCount = 0;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

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
            if (enemyAttack == enemy.middleAttack)
            {
                pattern.ProjectilePattern(enemy.projectilePooler, enemy.transform, movement, enemyAttack.projectileTag, enemyAttack.projectileSpeed, enemyAttack.projectileDamage);
                enemy.m_Renderer.sprite = enemy.spread;
            }
            else
            {
                pattern.ProjectilePattern(enemy.projectilePooler, enemy.enemyProjectileLauchOffset, movement, enemyAttack.projectileTag, enemyAttack.projectileSpeed, enemyAttack.projectileDamage);
                enemy.m_Renderer.sprite = enemy.attack;
            }
            enemy.timer = enemyAttack.fireRate;
            amountFired++;
        }

        if (amountFired == enemyAttack.fireAmount)
        {
            enemy.enemyStateMachine.ChangeState(enemy.enemyIdleState);
        }

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
