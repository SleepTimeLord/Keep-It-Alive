using System.Collections.Generic;
using UnityEngine;
using static EnemyTeleportState;

public enum CurrentPosition
{
    right,
    left,
    middle,
    nothing
}

// TODO: instead of making a attack one two or three state, make a list of the attacks using scriptable objects
public class Enemy : MonoBehaviour
{

    #region State Machine Vars
    public EnemyStateMachine enemyStateMachine;
    // this is to determine what attack the enemy is going to do
    public EnemyIdleState enemyIdleState;
    // does attack then switches to idle
    public EnemyAttackOneState enemyAttackOneState;
    // if player is too close, it teleports away.
    public EnemyTeleportState enemyTeleportState;

    #endregion

    [HideInInspector] public SpriteRenderer m_Renderer;
    public float timer;
    [Header("Health Settings")]
    [SerializeField] public int maxHealth = 10;
    [SerializeField] public int currentHealth;

    [Header("Attack Settings")]
    public int attackCooldown = 3;
    public int randAttack;
    public int randTeleport;
    public Transform enemyProjectileLauchOffset;
    public int fireDelayTime;
    // this determines how much time until the enemy slams the ground;
    public int teleportTime = 3;
    public GameObject rightWarning;
    public GameObject leftWarning;
    public GameObject middleWarning;
    [HideInInspector] public bool isInTeleportState = false;

    [Header("Projectiles")] 
    // this is for left and right
    public EnemyAttackSO[] enemyAttackList;
    // when the enemy is at the middle
    public EnemyAttackSO[] enemyAttackListMiddle;
    public EnemyAttackSO slamAttack;
    public ProjectilePooler projectilePooler;
    public EnemyAttackSO middleAttack;

    [Header("Sprites")]
    public Sprite idle;
    public Sprite attack;
    public Sprite slam;
    public Sprite spread;
    public Sprite dead;

    [HideInInspector] public int damageCount = 0;
    [HideInInspector] public CurrentPosition currentPosition;

    private void Awake()
    {
        enemyStateMachine = new EnemyStateMachine();

        // set up the instance by making the variable be equal to a new instance of each state
        enemyIdleState = new EnemyIdleState(this,enemyStateMachine);
        enemyAttackOneState = new EnemyAttackOneState(this, enemyStateMachine);
        enemyTeleportState = new EnemyTeleportState(this,enemyStateMachine);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        // starts with the enemy idle state
        enemyStateMachine.Initialize(enemyTeleportState);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        damageCount++;

        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    private void CorrectFlip()
    {
        // get the Z-rotation in degrees (0 → 360)
        float zAngle = transform.eulerAngles.z;

        m_Renderer.flipY = (zAngle > 90f && zAngle < 270f);

    }

    private void Die()
    {
        m_Renderer.sprite = dead;
        this.enabled = false;
        // enemy dies and player wins
    }

    public void GetLayer(int layer)
    {
        gameObject.layer = layer;
    }
    // Update is called once per frame
    void Update()
    {
        enemyStateMachine.CurrentEnemyState.FrameUpdate();
        CorrectFlip();
    }

    private void FixedUpdate()
    {
        enemyStateMachine.CurrentEnemyState.PhysicsUpdate();
    }
}
