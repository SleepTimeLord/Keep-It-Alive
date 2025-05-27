using UnityEngine;

public class CommandAttack : HeroState
{
    private float attackTimer;
    private Enemy enemy;
    public CommandAttack(Hero hero, HeroStateMachine heroStateMachine) : base(hero, heroStateMachine)
    {

    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();

        // finds the enemy gameobject.
        enemy = GameObject.FindAnyObjectByType<Enemy>();

        hero.isMoving = false;

        Debug.Log("is in attack state");

        // start intial attack
        enemy.TakeDamage(hero.damageAmout);
        attackTimer = hero.attackSpeed;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        hero.AttackEnemy();

        attackTimer -= Time.deltaTime;

        if (attackTimer < 0) 
        {
            enemy.TakeDamage(hero.damageAmout);
            Debug.Log("attacked");
            attackTimer = hero.attackSpeed;
        }

        hero.ChangeFromAttackState();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
