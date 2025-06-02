using UnityEngine;

public enum TypeOfShot
{
    straight,
    circle,
}
public class ProjectileBase : MonoBehaviour
{
    public TypeOfShot typeOfShot;
    private Hero _hero;
    private Enemy _enemy;
    [Header("Projectile Settings")]
    public int projectileDamage;
    public int projectileSpeed;
    ProjectilePooler _pooler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pooler = ProjectilePooler.Instance;
        _hero = FindAnyObjectByType<Hero>();
        _enemy = FindAnyObjectByType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        // shoot 
        transform.position += -transform.right * Time.deltaTime * projectileSpeed;


        Vector3 viewPoint = Camera.main.WorldToViewportPoint(transform.position);
        // NOTE: this might be the problem i think
        bool isVisible = viewPoint.z > 0 && viewPoint.x >= 0 && viewPoint.x <= 1 && viewPoint.y >= 0 && viewPoint.y <= 1;

/*        if (!isVisible)
        {
            if (gameObject != null)
            {
               _pooler.ReturnProjectile("redObject");
            }
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            Debug.Log("activated");
            _hero.Damage(projectileDamage);
            _pooler.ReturnProjectile(this.gameObject, "redObject");
        }
    }
}
