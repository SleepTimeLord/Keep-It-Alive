using UnityEngine;

public class ShortenedDashCheck : MonoBehaviour
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
        if (enemyTarget != null && _hero.gameObject != null)
        {
            if (collision.gameObject == enemyTarget)
            {
                _hero.shortenedDashtoEnemy = true;
            }

        }
        else
        {
            Debug.Log("cant find target");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemyTarget != null && _hero.gameObject != null)
        {
            if (collision.gameObject == enemyTarget)
            {
                _hero.shortenedDashtoEnemy = false;
            }
        }
    }
}
