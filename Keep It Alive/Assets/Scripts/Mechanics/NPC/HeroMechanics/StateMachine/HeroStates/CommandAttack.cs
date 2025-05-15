using UnityEngine;

public class CommandAttack : HeroState
{
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
        Debug.Log("is in attack state");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        hero.AttackEnemy();

        hero.ChangeFromAttackState();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
