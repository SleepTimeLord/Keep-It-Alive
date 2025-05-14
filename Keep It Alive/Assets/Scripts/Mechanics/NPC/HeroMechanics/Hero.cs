using UnityEngine;

public class Hero : MonoBehaviour, IDamageable, ITriggerCommandable
{
    [SerializeField] public float maxHealth { get; set; } = 3f;
    public float currentHealth { get; set; }

    public string command {  get; set; }

    // region is for orginization if i wanted to collapse 
    #region State Machine Variables
    // the states and statemachine aren't monobehaviors, so we gotta call them manually and set up new vars for state machine
    public HeroStateMachine heroStateMachine { get; set; }
    public CommandAttack heroAttack { get; set; }
    public CommandRally heroRally { get; set; }
    public CommandStandStill heroStandStill { get; set; }
    public CommandDodge heroDodge { get; set; }
    #endregion

    private void Awake()
    {
        heroStateMachine = new HeroStateMachine();

        // set up the instance by making the variable be equal to a new instance of each state
        heroAttack = new CommandAttack(this, heroStateMachine);
        heroRally = new CommandRally(this, heroStateMachine);
        heroStandStill = new CommandStandStill(this, heroStateMachine);
        heroDodge = new CommandDodge(this, heroStateMachine);
    }
    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void Start()
    {
        // sets the current health to full and makes sure that the command for the hero is "standstill".
        currentHealth = maxHealth;
        command = "standstill";

        // hero standstill is the starting state when the game is run.
        heroStateMachine.Initialize(heroStandStill);

    }

    private void Update()
    {
        heroStateMachine.CurrentHeroState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        heroStateMachine.CurrentHeroState.PhysicsUpdate();
    }

    public void Die()
    {
        // Hero dies and it plays the death screen
    }

    // this fuct will be used to determine which hero state to switch to.
    public void ActionCommanded(string action)
    {
        command = action;
    }

    // this changes to a different command state depending on 
    #region Check if Switch Command State
    public void ChangeFromStandStillState()
    {
        switch (command)
        {
            case "attack":
                heroStateMachine.ChangeState(heroAttack); break;
            case "rally":
                heroStateMachine.ChangeState(heroRally); break;
            case "dodge":
                heroStateMachine.ChangeState(heroDodge); break;
        }
    }

    public void ChangeFromAttackState()
    {
        switch (command)
        {
            case "rally":
                heroStateMachine.ChangeState(heroRally); break;
            case "dodge":
                heroStateMachine.ChangeState(heroDodge); break;
            case "standstill":
                heroStateMachine.ChangeState(heroStandStill); break;
        }
    }

    public void ChangeFromRallyState()
    {
        switch (command)
        {
            case "attack":
                heroStateMachine.ChangeState(heroAttack); break;
            case "standstill":
                heroStateMachine.ChangeState(heroStandStill); break;
            case "dodge":
                heroStateMachine.ChangeState(heroDodge); break;
        }
    }

    public void ChangeFromDodgeState()
    {
        switch (command)
        {
            case "standstill":
                heroStateMachine.ChangeState(heroStandStill); break;
            case "attack":
                heroStateMachine.ChangeState(heroAttack); break;
            case "rally":
                heroStateMachine.ChangeState(heroRally); break;
        }
    }
    #endregion
}