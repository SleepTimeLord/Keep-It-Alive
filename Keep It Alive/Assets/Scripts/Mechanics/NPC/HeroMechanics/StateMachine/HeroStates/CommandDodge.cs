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
        Debug.Log("is in dodge state");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        hero.ChangeFromDodgeState();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
