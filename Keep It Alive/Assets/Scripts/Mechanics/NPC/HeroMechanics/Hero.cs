using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour, ITriggerCommandable
{


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
    public bool isMoving = false;

    private Transform heroTransform;
    private Enemy enemy;
    [HideInInspector] public GameObject heroGameObject;
    [HideInInspector]public SpriteRenderer heroRenderer;
    public SpriteRenderer spiritRenderer;
    public GameObject spirit;
    public Vector3 initialSpiritPos;

    public bool cannotBeDamaged = false;

    [Header("Health Settings")]
    public int maxHealth = 20;
    public int currentHealth;

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

    [Header("Attack Settings")]
    public float attackSpeed = 1f;
    public int damageAmout = 1;
    public GameObject enemyAttackPos1;
    public GameObject enemyAttackPos2;

    [Header("Sprite Settings")]
    public Sprite idle;
    public Sprite die;
    public Sprite run;
    public Sprite parry;
    public Sprite dash;
    public Sprite attack;
    public Sprite spiritCry;
    public Sprite spiritIdle;
    public Sprite spiritCommand;
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
        if (!cannotBeDamaged)
        {
            currentHealth -= damageAmount;
        }


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Start()
    {
        heroTransform = GetComponent<Transform>();
        heroRenderer = GetComponent<SpriteRenderer>();
        heroGameObject = GetComponent<GameObject>();
        enemy = FindAnyObjectByType<Enemy>();
        // sets the current health to full and makes sure that the command for the hero is "standstill".
        //currentHealth = maxHealth;
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
        heroRenderer.sprite = die;
        spiritRenderer.sprite = spiritCry;
        this.enabled = false;
        StartCoroutine(LoseScreen());

    }

    IEnumerator LoseScreen()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("LoseScene");
    }

    // this fuct will be used to determine which hero state to switch to.
    public void ActionCommanded(string action)
    {
        command = action;
    }
    #region Hero Mechanics

    #region Stand Still
    public void ChangeFromStandStillState()
    {
        heroRenderer.sprite = idle;
        spiritRenderer.sprite = spiritIdle;
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
    public GameObject GetEnemyGameObject()
    {
        float distance1 = Vector3.Distance(transform.position, enemyAttackPos1.transform.position);
        float distance2 = Vector3.Distance(transform.position, enemyAttackPos2.transform.position);

        if (distance1 < distance2) 
        { 
            return enemyAttackPos1 ;
        }
        else
        {
            return enemyAttackPos2 ;
        }
    }
    public bool IsInAttackPos()
    {
        Transform enemyTransform = GetEnemyGameObject().transform;

        if (heroTransform.position == enemyTransform.position)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // chases the enemy until in attack range then attacks.
    public void ChaseEnemy()
    {
        if (!enemy.isInTeleportState)
        {
            spiritRenderer.sprite = spiritCommand;
            heroRenderer.sprite = run;
            if (GetEnemyGameObject() != null)
            {
                Vector2 direction = enemy.transform.position - transform.position;

                if (direction.x > 0.01f)
                    heroRenderer.flipX = true; // Facing right
                else if (direction.x < -0.01f)
                    heroRenderer.flipX = false;  // Facing left

                Transform enemyTransform = GetEnemyGameObject().transform;
                heroTransform.position = Vector3.MoveTowards(heroTransform.position, enemyTransform.position, Time.deltaTime * heroSpeed);

                if (IsInAttackPos())
                {
                    command = "attack";
                    heroStateMachine.ChangeState(heroAttack);
                }
            }
            else
            {
                // happens if there is no enemy
                command = "standstill";
                heroStateMachine.ChangeState(heroStandStill);
            }
        }
        else
        {
            command = "standstill";
            heroStateMachine.ChangeState(heroStandStill);
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
    #endregion

    #region Attack
    // finds the enemy and places a marker for the hero to attack that spot once it gets close enough to the enemy position
    public void AttackEnemy()
    {
        spiritRenderer.sprite = spiritCommand;
        // attacks enemy
        // if not in range, it goes to standstill state until says so
        if (!IsInAttackPos())
        {
            command = "standstill";
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
        spiritRenderer.sprite = spiritCommand;

        heroRenderer.sprite = run;
        Vector2 direction = initialSpiritPos - transform.position;

        if (direction.x > 0.01f)
            heroRenderer.flipX = true; // Facing right
        else if (direction.x < -0.01f)
            heroRenderer.flipX = false;  // Facing left

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

    #region Dodge
    // dodges in place
    public void Dodge()
    {
        spiritRenderer.sprite = spiritCommand;
        heroRenderer.sprite = parry;
        // this applies the cooldown
        dodgeTimer -= Time.deltaTime;

        if (dodgeTimer < 0)
        {
            // change to new scene
            if (dodgeToAttack && IsInAttackPos())
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
        spiritRenderer.sprite = spiritCommand;
        heroRenderer.sprite = dash;
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
            else if (IsInAttackPos())
            {
                dashSpeed = 0;
                command = "attack";
                heroStateMachine.ChangeState(heroAttack);
            }
        }
        heroTransform.position += dashDir * dashSpeed * Time.deltaTime;

        dashSpeed -= dashSpeed * dashSpeedFalloff * Time.deltaTime;
    }

    public void ChangeLayerToDash()
    {
        gameObject.layer = 8;
    }

    public void ChangeLayerToHero()
    {
        gameObject.layer = 3;
    }
    #endregion

    #endregion
}