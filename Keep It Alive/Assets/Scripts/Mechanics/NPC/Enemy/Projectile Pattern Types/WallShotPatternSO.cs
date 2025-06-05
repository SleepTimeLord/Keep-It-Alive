using UnityEngine;

public class WallShotPatternSO : ProjectilePatternSO
{
    public float projectileSpacing;
    public int projectileAmount;
    public override IProjectilePattern CreatePattern()
    {
        return new WallShotPattern(projectileSpacing, projectileAmount);
    }


}

public class WallShotPattern : IProjectilePattern
{
    private float projectileSpacing;
    private int projectileAmount;
    public WallShotPattern(float projectileSpacing, int projectileAmount) 
    { 
        this.projectileSpacing = projectileSpacing;
        this.projectileAmount = projectileAmount;
    }

    public void ProjectilePattern(ProjectilePooler pooler, Transform projectileSpawn, IProjectileMovement projectileMovementType, string bulletTag, float projectileSpeed, int projectileDamage)
    {
        for (int i = 0; i < projectileAmount; i++) 
        { 

        }
    }
}