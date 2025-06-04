using UnityEngine;

public class SingleShotPattern : IBulletPattern
{

    // we do this to get the specifics
    // to use this we reference this in another script
    public SingleShotPattern()
    {

    }

    public void BulletPattern(ProjectilePooler pooler, Transform projectileSpawn, IProjectileMovement projectileMovementType, string bulletTag, float projectileSpeed, int projectileDamage)
    {
        // spawns specific bullet from projectileSpawn
        GameObject projectile = pooler.SpawnFromPool(bulletTag);

        // gets the script to control the bullets so it can set up what type of movement the bullet should do (straight or wave), the speed, and the damage.
        ProjectileBase projectileEditor = projectile.GetComponent<ProjectileBase>();
        if (projectileEditor != null)
        {
            projectileEditor._bulletMovementType = projectileMovementType;
            projectileEditor.projectileDamage = projectileDamage;
            projectileEditor.projectileSpeed = projectileSpeed;
        }

        // spawns the bullet in ther spawn position
        projectile.transform.position = projectileSpawn.position;
        projectile.transform.rotation = projectileSpawn.rotation;
    }
}