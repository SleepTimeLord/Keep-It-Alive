using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    ProjectilePooler pooler;
    public ProjectileMovementSO projectileMovement;
    public float projectileSpeed = 5;
    private GameObject projectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pooler = ProjectilePooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SpawnProjectile();
        }
    }

    public void SpawnProjectile()
    {

        projectile = pooler.SpawnFromPool("redObject");
        // gets the projectileBase from the bullet so it can set what movement type its gonna do 
        // TODO: Make an arguement in SpawnProjectile that is IBulletMovement. It specifies if it's a WAveBulletMovement or StraightBulletMovement
        projectile.GetComponent<ProjectileBase>()._bulletMovementType = projectileMovement.CreateMovement();
        // sets it to the bullet spawn
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        // shoot 
        /*            projectile.transform.position += -projectile.transform.right * Time.deltaTime * projectileSpeed;
        */
    }
}
