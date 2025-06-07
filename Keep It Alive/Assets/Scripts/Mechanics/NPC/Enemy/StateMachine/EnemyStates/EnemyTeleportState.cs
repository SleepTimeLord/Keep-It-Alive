using UnityEngine;

public class EnemyTeleportState : EnemyState
{
    private Vector3 initialPosition;
    private Vector3 teleportRight = new Vector3 (7,0,0);
    private Vector3 teleportLeft = new Vector3 (-7,0,0);
    private Vector3 teleportMiddle = new Vector3 (0,0,0);
    private CurrentPosition newPosition;
    private IProjectilePattern pattern;
    private IProjectileMovement movement;
    private GameObject enemyGO;
    private int fireAmount;
    private int slamTime = 1;
    private float slamAniTimer;

    public EnemyTeleportState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.isInTeleportState = true;
        initialPosition = enemy.transform.position;

        // figures out which position the enemy is currently in
        if (initialPosition == teleportRight)
        {
            enemy.currentPosition = CurrentPosition.right;
        }
        else if (initialPosition == teleportLeft)
        {
            enemy.currentPosition = CurrentPosition.left;
        }
        else if (initialPosition == teleportMiddle)
        {
            enemy.currentPosition = CurrentPosition.middle;
        }
        else
        {
            Debug.Log("intial Position does not equal to anything proceeding to rand pos");

            enemy.currentPosition = CurrentPosition.nothing;
        }

        // gets a random int and teleports the boss into a new place and sets the enemy current postion to the new position
        enemy.randTeleport = Random.Range(0, 2);

        switch (enemy.currentPosition)
        {
            case CurrentPosition.right:
                enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                if (enemy.randTeleport == 0)
                {
                    enemy.currentPosition = CurrentPosition.left;
                    enemy.transform.position = teleportLeft;
                }
                else
                {
                    enemy.currentPosition = CurrentPosition.middle;
                    enemy.transform.position = teleportMiddle;
                }
                break;
            case CurrentPosition.left:
                enemy.transform.rotation = Quaternion.Euler(0, 0, -180);
                if (enemy.randTeleport == 0)
                {
                    enemy.currentPosition = CurrentPosition.right;
                    enemy.transform.position = teleportRight;
                }
                else
                {
                    enemy.currentPosition = CurrentPosition.middle;
                    enemy.transform.position = teleportMiddle;
                }
                break;
            case CurrentPosition.middle:
                enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                if (enemy.randTeleport == 0)
                {
                    enemy.currentPosition = CurrentPosition.right;
                    enemy.transform.position = teleportRight;
                }
                else
                {
                    enemy.currentPosition = CurrentPosition.left;
                    enemy.transform.position = teleportLeft;
                }
                break;
            case CurrentPosition.nothing:
                int newRandTeleport = Random.Range(0, 3);
                if (newRandTeleport == 0)
                {
                    enemy.currentPosition = CurrentPosition.left;
                    enemy.transform.position = teleportLeft;
                }
                else if (newRandTeleport == 1)
                {
                    enemy.currentPosition = CurrentPosition.right;
                    enemy.transform.position = teleportRight;
                }
                else
                {
                    enemy.currentPosition = CurrentPosition.middle;
                    enemy.transform.position = teleportMiddle;
                }
                break;
        }

        fireAmount = 0;
        pattern = enemy.slamAttack.projectilePattern.CreatePattern();
        movement = enemy.slamAttack.projectileMovement.CreateMovement();
        enemy.m_Renderer.enabled = false;
        enemy.GetLayer(10);
        enemy.timer = enemy.teleportTime;
        slamAniTimer = slamTime;
    }
    public override void ExitState()
    {
        base.ExitState();
        enemy.isInTeleportState = false;
    }

    // TODO: does a little warning when teleporting then teleports when it does if the player is on it, it damages player pounds them back
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
        enemy.timer -= Time.deltaTime;

        if (enemy.timer < 0)
        {
            enemy.m_Renderer.enabled = true;
            slamAniTimer -= Time.deltaTime;
            enemy.m_Renderer.sprite = enemy.slam;
            enemy.rightWarning.SetActive(false);
            enemy.leftWarning.SetActive(false);
            enemy.middleWarning.SetActive(false);

            if (fireAmount != 1)
            {
                fireAmount++;
                pattern.ProjectilePattern(enemy.projectilePooler, enemy.transform, movement, enemy.slamAttack.projectileTag, enemy.slamAttack.projectileSpeed, enemy.slamAttack.projectileDamage);
                enemy.GetLayer(7);
            }
            if (slamAniTimer < 0)
            {
                enemy.enemyStateMachine.ChangeState(enemy.enemyIdleState);
            }
        }
        else
        {
            switch (enemy.currentPosition)
            {
                case CurrentPosition.right:
                    enemy.rightWarning.SetActive(true);
                    break;
                case CurrentPosition.left:
                    enemy.leftWarning.SetActive(true);
                    break;
                case CurrentPosition.middle:
                    enemy.middleWarning.SetActive(true);
                    break;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
