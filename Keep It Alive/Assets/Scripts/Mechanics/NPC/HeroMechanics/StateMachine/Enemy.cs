using System.Collections.Generic;
using UnityEngine;

// TODO: instead of making a attack one two or three state, make a list of the attacks using scriptable objects
public class Enemy : MonoBehaviour
{

    #region State Machine Vars
    public EnemyStateMachine enemyStateMachine;
    // this is to determine what attack the enemy is going to do
    public EnemyIdleState enemyIdleState;
    // does attack then switches to idle
    public EnemyAttackOneState enemyAttackOneState;
    public EnemyAttackTwoState enemyAttackTwoState;
    // if player is too close, it teleports away.
    public EnemyTeleportState enemyTeleportState;
    #endregion

    public float timer;
    [Header("Health Settings")]
    [SerializeField] private int maxHealh = 10;
    [SerializeField] private int currentHealth;

    [Header("Attack Settings")]
    public int attackCooldown = 3;
    public int randAttack;
    public Transform enemyProjectileLauchOffset;
    public int fireDelayTime;

    [Header("Projectiles")] 
    public EnemyAttackSO[] enemyAttackList;
    private Dictionary<GameObject, Queue<GameObject>> _pools;
    private Queue<GameObject> pool = new Queue<GameObject>();
    private void Awake()
    {
        enemyStateMachine = new EnemyStateMachine();

        // set up the instance by making the variable be equal to a new instance of each state
        enemyIdleState = new EnemyIdleState(this,enemyStateMachine);
        enemyAttackOneState = new EnemyAttackOneState(this, enemyStateMachine);
        enemyAttackTwoState = new EnemyAttackTwoState(this, enemyStateMachine);
        enemyTeleportState = new EnemyTeleportState(this,enemyStateMachine);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealh;
        // starts with the enemy idle state
        enemyStateMachine.Initialize(enemyIdleState);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0) 
        {
            Die();
        }
    }
    private void Die()
    {
        // enemy dies and player wins
    }

    // Update is called once per frame
    void Update()
    {
        enemyStateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        enemyStateMachine.CurrentEnemyState.PhysicsUpdate();
    }
}
