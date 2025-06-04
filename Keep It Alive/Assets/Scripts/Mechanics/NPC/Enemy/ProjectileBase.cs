using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    private Hero _hero;
    [Header("Projectile Settings")]
    public int projectileDamage;
    public float projectileSpeed;
/*    public float waveFrequency;
    // keep the amplitude < 1
    public float waveAmplitude;*/
    ProjectilePooler _pooler;
    public IProjectileMovement _bulletMovementType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pooler = ProjectilePooler.Instance;
        _hero = FindAnyObjectByType<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        _bulletMovementType.Move(this);

        Vector3 viewPoint = Camera.main.WorldToViewportPoint(transform.position);

        bool isVisible = viewPoint.z > 0 && viewPoint.x >= 0 && viewPoint.x <= 1 && viewPoint.y >= 0 && viewPoint.y <= 1;

        if (!isVisible)
        {
            _pooler.ReturnProjectile(this.gameObject, "redObject");
        }

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
