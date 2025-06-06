using UnityEngine;

public class CommandDash : HeroState
{
    public CommandDash(Hero hero, HeroStateMachine heroStateMachine) : base(hero, heroStateMachine)
    {

    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();
        hero.cannotBeDamaged = true;
        hero.ChangeLayerToDash();
    }

    public override void ExitState()
    {
        base.ExitState();
        hero.cannotBeDamaged = false;
        hero.ChangeLayerToHero();
    }

    public override void FrameUpdate()
    {
        hero.DashtoEnemyorRally();
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

