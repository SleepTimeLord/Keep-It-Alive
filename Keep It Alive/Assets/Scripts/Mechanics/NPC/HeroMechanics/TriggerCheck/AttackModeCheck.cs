using UnityEngine;

public class AttackModeCheck : MonoBehaviour
{
    public GameObject enemyTarget;
    public Hero _hero;
    // check if enemy is in radius and

    private void Start()
    {
        enemyTarget = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyTarget != null)
        {
            if (collision.gameObject == enemyTarget)
            {
                _hero.SetAttackRangeBool(true);
            }

        }
        else
        {
            Debug.Log("cant find target");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemyTarget != null)
        {
            if (collision.gameObject == enemyTarget)
            {
                _hero.SetAttackRangeBool(false);   
            }
        }
    }
}