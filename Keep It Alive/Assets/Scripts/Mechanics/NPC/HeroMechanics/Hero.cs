using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : MonoBehaviour, IDamageable, ITriggerCommandable, ITriggerInAttackRangeCheckable
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

    public ChaseState heroChase { get; set; }
    public bool isInAttackRange { get; set; }
    #endregion

    private Transform heroTransform;
    public GameObject spirit;

    // gets enemy from parent.
    public GameObject enemyContainer;
    #region Mechanic Vars
    [SerializeField] private float heroSpeed = 3;
/*    [SerializeField] private float heroDamage = 1;
    [SerializeField] private float heroDodgeLength = 2;*/
    #endregion
    private void Awake()
    {
        heroStateMachine = new HeroStateMachine();

        // set up the instance by making the variable be equal to a new instance of each state
        heroAttack = new CommandAttack(this, heroStateMachine);
        heroRally = new CommandRally(this, heroStateMachine);
        heroStandStill = new CommandStandStill(this, heroStateMachine);
        heroDodge = new CommandDodge(this, heroStateMachine);
        heroChase = new ChaseState(this, heroStateMachine);
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
        heroTransform = GetComponent<Transform>();
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

    public void SetAttackRangeBool(bool inAttackRange)
    {
        isInAttackRange = inAttackRange;
    }

    // this changes to a different command state depending on 
    #region Check if Switch Command State
    public void ChangeFromStandStillState()
    {
        switch (command)
        {
            case "chase":
                heroStateMachine.ChangeState(heroChase); break;
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
            case "chase":
                heroStateMachine.ChangeState(heroChase); break;
            case "rally":
                heroStateMachine.ChangeState(heroRally); break;
            case "dodge":
                heroStateMachine.ChangeState(heroDodge); break;
            case "standstill":
                heroStateMachine.ChangeState(heroStandStill); break;
        }
    }

    public void ChangeFromChaseState()
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
            case "chase":
                heroStateMachine.ChangeState(heroChase); break;
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
            case "chase":
                heroStateMachine.ChangeState(heroChase); break;
            case "rally":
                heroStateMachine.ChangeState(heroRally); break;
        }
    }
    #endregion

    // this are the mechanics for the hero (standstill, rally, dodge, attack)
    #region Hero Mechanics

    // chases the enemy until in attack range then attacks.
    #region Chase/Attack Enemy
    public void ChaseEnemy()
    {
        if (GetEnemyGameObject() != null)
        {
            Transform enemyTransform = GetEnemyGameObject().transform;
            heroTransform.position = Vector3.Lerp(heroTransform.position, enemyTransform.position, Time.deltaTime * heroSpeed);
        }
        else
        {
            // happens if there is no enemy
            heroStateMachine.ChangeState(heroStandStill);
        }
        if (isInAttackRange)
        {
            // this ensures that it doesn't glitch
            command = "attack";
            heroStateMachine.ChangeState(heroAttack);
        }
    }

    // finds the enemy and places a marker for the hero to attack that spot once it gets close enough to the enemy position
    public void AttackEnemy()
    {
        // attacks enemy
        // if not in range, it goes to standstill state until says so
        if (!isInAttackRange)
        {
            heroStateMachine.ChangeState(heroStandStill);
        }
    }

    public GameObject GetEnemyGameObject()
    {
        return GameObject.FindGameObjectWithTag("Enemy");
    }
    #endregion

    // goes to the position of the spirit and keeps following
    public void Rally()
    {
        Transform spiritPosition = spirit.transform;

        heroTransform.position = Vector3.Lerp(heroTransform.position, spiritPosition.position, Time.deltaTime * heroSpeed);
    }

    // dodges in place
    public void Dodge()
    {

    }

    // make dash

    // defult stand still
    public void StandStill()
    {

    }
    #endregion
}