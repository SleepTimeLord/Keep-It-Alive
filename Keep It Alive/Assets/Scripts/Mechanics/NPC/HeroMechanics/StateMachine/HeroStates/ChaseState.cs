using UnityEngine;

public class ChaseState : HeroState
{
    public ChaseState(Hero hero, HeroStateMachine heroStateMachine) : base(hero, heroStateMachine)
    {
    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Is in chase State");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        hero.ChaseEnemy();
        hero.ChangeFromChaseState();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
