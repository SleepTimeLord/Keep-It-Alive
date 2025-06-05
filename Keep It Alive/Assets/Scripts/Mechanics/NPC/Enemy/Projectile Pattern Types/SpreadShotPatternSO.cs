using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;


[CreateAssetMenu(menuName = "Projectile/Pattern/Spread")]
public class SpreadShotPatternSO : ProjectilePatternSO
{
    public int projectileAmount;
    public float spreadAngle;
    public float projectileSpacing;

    //public int spreadSpacePos;

    public override IProjectilePattern CreatePattern()
    {
        return new SpreadShotPattern(projectileAmount, spreadAngle, projectileSpacing);
    }
}

public class SpreadShotPattern : IProjectilePattern
{
    private int projectileAmount;
    private float spreadAngle; // How much angle to spread (e.g., 45 degrees total)
    private float projectileSpacing;
 

    public SpreadShotPattern(int projectileAmount, float spreadAngle, float projectileSpacing)
    {
        this.projectileAmount = projectileAmount;
        this.spreadAngle = spreadAngle;
        this.projectileSpacing = projectileSpacing;
    }

    public void ProjectilePattern(ProjectilePooler pooler, Transform projectileSpawn, IProjectileMovement projectileMovementType, string bulletTag, float projectileSpeed, int projectileDamage)
    {
        // Calculate how far apart each bullet’s angle should be (in degrees):
        float angleStep = 0f;
        if (projectileAmount > 1)
            angleStep = spreadAngle / (projectileAmount - 1);

        // The “starting” spread angle is half to the left of forward:
        float startAngle = -spreadAngle / 2f;

        // Get the shooter’s current Z‐rotation in degrees:
        float shooterZ = projectileSpawn.eulerAngles.z;

        // for position offset
        float offsetStep = projectileSpacing;

        float startOffsetStep = -offsetStep * ((projectileAmount - 1 )/ 2);

        for (int i = 0; i < projectileAmount; i++)
        {
            GameObject projectile = pooler.SpawnFromPool(bulletTag);
            if (projectile == null) continue;

            // Assign movement and stats:
            ProjectileBase projectileEditor = projectile.GetComponent<ProjectileBase>();
            if (projectileEditor != null)
            {
                projectileEditor._bulletMovementType = projectileMovementType;
                projectileEditor.projectileSpeed = projectileSpeed;
                projectileEditor.projectileDamage = projectileDamage;
            }

            // Compute this bullet’s local spread‐angle, then add the shooter’s rotation:
            //    e.g. if startAngle = -30°, angleStep = 15°, and shooterZ = 45°,
            //    then for i=0: bulletAngle = -30 + 0*15 + 45 = 15° (world space).
            float localAngle = startAngle + angleStep * i;
            float bulletAngle = localAngle + shooterZ;

            // same thing for the angles but its for the position
            float localPosOffset = startOffsetStep + offsetStep * i;
            Vector3 worldOffset = -projectileSpawn.up * localPosOffset;

            // Build a 2D direction vector from bulletAngle:
            float rad = bulletAngle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f);

            // Position the bullet at the spawn point:
            projectile.transform.position = projectileSpawn.position + worldOffset;

            // Rotate the bullet so that its “right” (or “up”, depending on your sprite) faces ⟂ direction.
            projectile.transform.rotation = Quaternion.Euler(0f, 0f, bulletAngle);
        }
    }
}