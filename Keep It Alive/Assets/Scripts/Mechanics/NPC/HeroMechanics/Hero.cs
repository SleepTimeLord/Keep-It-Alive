using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : MonoBehaviour, ITriggerCommandable, ITriggerInAttackRangeCheckable
{
    public int maxHealth = 20;
    public int currentHealth;

    public string command {  get; set; }

    // region is for orginization if i wanted to collapse 
    #region State Machine Variables
    // the states and statemachine aren't monobehaviors, so we gotta call them manually and set up new vars for state machine
    public HeroStateMachine heroStateMachine { get; set; }
    public CommandAttack heroAttack { get; set; }
    public CommandRally heroRally { get; set; }
    public CommandStandStill heroStandStill { get; set; }
    public CommandDodge heroDodge { get; set; }
    public CommandDash heroDash { get; set; }
    public ChaseState heroChase { get; set; }
    #endregion
    public bool isInAttackRange { get; set; }
    public bool isMoving = false;

    private Transform heroTransform;
    public GameObject spirit;
    public Vector3 initialSpiritPos;

    public bool cannotBeDamaged = false;

    [Header("Dash Settings")]
    public float intialDashSpeed = 100;
    public float dashSpeed = 100f;
    public float intialDashSpeedFalloff = 30f;
    public float dashSpeedFalloff = 30f;

    private bool dashingToRally = false;
    private Vector3 dashDir;

    [Header("Dodge Settings")]
    public float dodgeTime = .5f;
    public float dodgeTimer = 0;
    private bool dodgeToAttack = false;

    [Header("Cooldown Settings")]
    public float commandCooldown;
    public float dodgeCooldown;
    public float standStillCooldown;

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
        heroDash = new CommandDash(this, heroStateMachine);
        heroChase = new ChaseState(this, heroStateMachine);
    }
    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Start()
    {
        heroTransform = GetComponent<Transform>();
        // sets the current health to full and makes sure that the command for the hero is "standstill".
        //currentHealth = maxHealth;
        command = "standstill";

        // hero standstill is the starting state when the game is run.
        heroStateMachine.Initialize(heroStandStill);

    }

    private void Update()
    {
        heroStateMachine.CurrentHeroState.FrameUpdate();

        if (cannotBeDamaged)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
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

    // this are the mechanics for the hero (standstill, rally, dodge, attack)
    #region Hero Mechanics

    #region Stand Still
    public void ChangeFromStandStillState()
    {
        switch (command)
        {
            case "chase":
                heroStateMachine.ChangeState(heroChase); break;
            case "rally":
                heroStateMachine.ChangeState(heroRally); break;
            case "dodge":
                dodgeToAttack = false;
                heroStateMachine.ChangeState(heroDodge); break;
        }
    }


    #endregion

    #region Chase
    // chases the enemy until in attack range then attacks.
    public void ChaseEnemy()
    {
        if (GetEnemyGameObject() != null)
        {
            Transform enemyTransform = GetEnemyGameObject().transform;
            heroTransform.position = Vector3.MoveTowards(heroTransform.position, enemyTransform.position, Time.deltaTime * heroSpeed);
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

    public void ChangeFromChaseState()
    {
        switch (command)
        {
            case "rally":
                heroStateMachine.ChangeState(heroRally); break;
            case "dash":
                dashingToRally = false;
                heroStateMachine.ChangeState(heroDash); break;
            case "standstill":
                heroStateMachine.ChangeState(heroStandStill); break;
        }
    }

    public GameObject GetEnemyGameObject()
    {
        return GameObject.FindGameObjectWithTag("EnemyAttackPos");
    }
    #endregion

    #region Attack
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

    public void ChangeFromAttackState()
    {
        switch (command)
        {
            case "chase":
                heroStateMachine.ChangeState(heroChase); break;
            case "rally":
                heroStateMachine.ChangeState(heroRally); break;
            case "dodge":
                dodgeToAttack = true;
                heroStateMachine.ChangeState(heroDodge); break;
            case "standstill":
                heroStateMachine.ChangeState(heroStandStill); break;
        }
    }
    #endregion

    #region Rally
    // goes to the position of the spirit and keeps following
    public void Rally()
    {
        heroTransform.position = Vector3.MoveTowards(heroTransform.position, initialSpiritPos, Time.deltaTime * heroSpeed);

        if (heroTransform.position == initialSpiritPos)
        {
            command = "standstill";
            heroStateMachine.ChangeState(heroStandStill);
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
            case "dash":
                dashingToRally = true;
                heroStateMachine.ChangeState(heroDash); break;
        }
    }
    #endregion

    // TODO: change from coroutine to time.deltatiem to calculate cooldown.
    #region Dodge
    // dodges in place
    public void Dodge()
    {
        // this applies the cooldown
        dodgeTimer -= Time.deltaTime;

        if (dodgeTimer < 0)
        {
            // change to new scene
            if (dodgeToAttack && isInAttackRange)
            {
                command = "attack";
                heroStateMachine.ChangeState(heroAttack);
            }
            else
            {
                command = "standstill";
                heroStateMachine.ChangeState(heroStandStill);
            }
        }
    }
    #endregion


    #region Dash
    public void DashtoEnemyorRally()
    {
        if (dashingToRally)
        {
            Transform spiritTransform = spirit.transform;
            dashDir = (spiritTransform.position - heroTransform.position).normalized;

            if (dashSpeed <= heroSpeed)
            {
                command = "rally";
                heroStateMachine.ChangeState(heroRally);
            }
            else if (heroTransform.position == spirit.transform.position)
            {
                command = "standstill";
                heroStateMachine.ChangeState(heroStandStill);
            }
        }
        else
        {
            print("dash to enemy");
            Transform enemyTransform = GetEnemyGameObject().transform;
            dashDir = (enemyTransform.position - heroTransform.position).normalized;

            if (dashSpeed <= heroSpeed)
            {
                command = "chase";
                heroStateMachine.ChangeState(heroChase);
            }
            else if (isInAttackRange)
            {
                dashSpeed = 0;
                command = "attack";
                heroStateMachine.ChangeState(heroAttack);
            }
        }
        heroTransform.position += dashDir * dashSpeed * Time.deltaTime;

        dashSpeed -= dashSpeed * dashSpeedFalloff * Time.deltaTime;
    }
    #endregion

    #endregion
}