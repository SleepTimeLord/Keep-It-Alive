using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    ProjectilePooler pooler;
    public ProjectileMovementSO projectileMovement;
    public ProjectilePatternSO projectilePattern;
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

        //projectile = pooler.SpawnFromPool("redObject");
        IProjectilePattern pattern = projectilePattern.CreatePattern();
        IProjectileMovement movement = projectileMovement.CreateMovement();

        // its all done through the projectilepattern
        pattern.ProjectilePattern(pooler, this.transform, movement, "redObject", projectileSpeed, 1);


    }
}
