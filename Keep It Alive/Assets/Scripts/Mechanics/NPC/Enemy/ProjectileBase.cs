using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    private Hero _hero;
    private Enemy _enemy;
    private bool gotParried = false;
    [Header("Projectile Settings")]
    public int projectileDamage;
    public float projectileSpeed;
/*    public float waveFrequency;
    // keep the amplitude < 1
    public float waveAmplitude;*/
    ProjectilePooler _pooler;
    public IProjectileMovement _bulletMovementType;
    public ProjectileMovementSO parriedMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        _pooler = ProjectilePooler.Instance;
        _hero = FindAnyObjectByType<Hero>();
        _enemy = FindAnyObjectByType<Enemy>();
        gameObject.tag = "Bullet";
        gameObject.layer = 6;
    }
    private void OnDisable()
    {
        gotParried = false;
    }

    // Update is called once per frame
    void Update()
    {
        _bulletMovementType.Move(this);

        if (gotParried)
        {
            gameObject.tag = "ParriedBullet";
            gameObject.layer = 9;
            //_bulletMovementType = parriedMovement.CreateMovement();
            _bulletMovementType = new ProjectileParriedMovement(_enemy.transform, 20);
        }

        Vector3 viewPoint = Camera.main.WorldToViewportPoint(transform.position);

        bool isVisible = viewPoint.z > 0 && viewPoint.x >= 0 && viewPoint.x <= 1 && viewPoint.y >= 0 && viewPoint.y <= 1;

        if (!isVisible)
        {
            // change the return to green or any other one 
            _pooler.ReturnProjectile(this.gameObject, "redObject");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            _hero.Damage(projectileDamage);
            _pooler.ReturnProjectile(this.gameObject, "redObject");
        }
        else if (collision.gameObject.tag == "ParriedBullet")
        {
            _pooler.ReturnProjectile(this.gameObject, "redObject");
        }
        else if (collision.gameObject.tag == "Dodge")
        {
            gotParried = true;
        }

        if (gotParried)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                _enemy.TakeDamage(projectileDamage);
                _pooler.ReturnProjectile(this.gameObject, "redObject");
            }
            if (collision.gameObject.tag == "Bullet")
            {
                _pooler.ReturnProjectile(this.gameObject, "redObject");
            }
        }
    }
}
