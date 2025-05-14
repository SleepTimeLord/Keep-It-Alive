using UnityEngine;

public class HeroStateMachine // keeps track of which state the hero is currently in
{
    // this checks what the current state is.
    public HeroState CurrentHeroState { get; set; }

    // this ensures that the script knows which state it starts at.
    public void Initialize(HeroState startingState)
    {
        CurrentHeroState = startingState;
        CurrentHeroState.EnterState();
    }

    // this is if we want to enter a new state. We exit then enter a new state.
    public void ChangeState (HeroState newState)
    {
        CurrentHeroState.ExitState();
        CurrentHeroState = newState;
        CurrentHeroState.EnterState();
    }
}
