using UnityEngine;

public class SpreadShotPattern : IBulletPattern
{
    // how many bullets come out of the spread shot 
    private int bulletAmount;
    // the spacing between the bullets
    private int spreadSpace;

    public SpreadShotPattern(int bulletAmount, int spreadSpace)
    {
        this.bulletAmount = bulletAmount;
        this.spreadSpace = spreadSpace;
    }

    public void BulletPattern(ProjectilePooler pooler, Transform projectileSpawn, IProjectileMovement projectileMovementType, string bulletTag, float projectileSpeed, int projectileDamage)
    {
        float startOfSpace = bulletAmount * spreadSpace;
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject projectile = pooler.SpawnFromPool(bulletTag);

            ProjectileBase projectileEditor = projectile.GetComponent<ProjectileBase>();

            if (projectileEditor != null)
            {
                projectileEditor._bulletMovementType = projectileMovementType;
                projectileEditor.projectileSpeed = projectileSpeed;
                projectileEditor.projectileDamage = projectileDamage;
            }

            Quaternion bulletRot = Quaternion.Euler(0,0,spreadSpace);

            // spawns the bullet in ther spawn position
            projectile.transform.position = projectileSpawn.position;
            projectile.transform.rotation = bulletRot;

            startOfSpace -= spreadSpace;
        }
    }
}
