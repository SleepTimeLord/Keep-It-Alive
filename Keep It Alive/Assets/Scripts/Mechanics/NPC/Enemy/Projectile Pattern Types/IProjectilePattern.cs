using UnityEngine;

public interface IProjectilePattern
{

    void ProjectilePattern(ProjectilePooler pooler, Transform projectileSpawn, IProjectileMovement projectileMovementType, string bulletTag, float projectileSpeed, int projectileDamage)
    {

    }
}
