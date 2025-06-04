using UnityEngine;

public interface IBulletPattern
{

    void BulletPattern(ProjectilePooler pooler, Transform projectileSpawn, IProjectileMovement projectileMovementType, string bulletTag, float projectileSpeed, int projectileDamage)
    {

    }
}
