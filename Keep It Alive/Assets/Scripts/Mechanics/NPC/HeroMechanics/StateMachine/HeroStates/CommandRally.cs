using UnityEngine;

public class CommandRally : HeroState
{
    public CommandRally(Hero hero, HeroStateMachine heroStateMachine) : base(hero, heroStateMachine)
    {

    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("is in rally state");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        hero.ChangeFromRallyState();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
