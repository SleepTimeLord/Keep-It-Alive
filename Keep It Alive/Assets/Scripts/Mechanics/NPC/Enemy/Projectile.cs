using UnityEngine;

// gets put on each projectile
public class Projectile : MonoBehaviour
{
    private Hero _hero;
    public int projectileDamage = 1;
    public int projectileSpeed = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _hero = FindAnyObjectByType<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        // shoot 
        transform.position += -transform.right * Time.deltaTime * projectileSpeed;

        Vector3 viewPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool isVisible = viewPoint.z > 0 && viewPoint.x >= 0 && viewPoint.x <= 1 && viewPoint.y >= 0 && viewPoint.y <= 1;

        if (!isVisible) 
        { 
            if(gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            _hero.Damage(projectileDamage);
            Destroy(gameObject);
        }
    }
}
