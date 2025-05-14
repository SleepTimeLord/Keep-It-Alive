using UnityEngine;


public class HeroState// this is a base for all the hero states
{
    // protect acts like a private var but acts as a public to any script that derives from this script. This means that all our individual states can access these vars
    protected Hero hero;
    protected HeroStateMachine heroStateMachine;

    // this is used to pass data in when we create an instance of the HeroState
    public HeroState(Hero hero, HeroStateMachine heroStateMachine)
    {
        this.hero = hero;
        this.heroStateMachine = heroStateMachine;
    }

    // this lists all the states
    // public virtual void means that we can overrides these methods, meaning that it's optional if we call these or not.
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent() { }
}