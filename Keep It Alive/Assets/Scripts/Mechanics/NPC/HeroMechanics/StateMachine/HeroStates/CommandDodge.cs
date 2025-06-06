using UnityEngine;

public class CommandDodge : HeroState
{
    public CommandDodge(Hero hero, HeroStateMachine heroStateMachine) : base(hero, heroStateMachine)
    {

    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();
        hero.tag = "Dodge";
        // starts the cooldown when enter the state
        hero.dodgeTimer = hero.dodgeTime;
        hero.cannotBeDamaged = true;
    }

    public override void ExitState()
    {
        base.ExitState();
        hero.cannotBeDamaged = false;
        hero.tag = "Hero";
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        hero.Dodge();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
