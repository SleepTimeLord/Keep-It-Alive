using UnityEngine;

public class CommandStandStill : HeroState
{
    public CommandStandStill(Hero hero, HeroStateMachine heroStateMachine) : base(hero, heroStateMachine)
    {

    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("is in standstill state");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        hero.ChangeFromStandStillState();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
