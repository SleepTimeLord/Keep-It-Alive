using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "Projectile/Movement/Straight")]
public class StraightBulletMovementSO : ProjectileMovementSO
{
    public override IProjectileMovement CreateMovement()
    {
        return new StraightBulletMovement();
    }
}

public class StraightBulletMovement : IProjectileMovement
{
    // we have this because want to create a new instance of this script in the StraightBulletMovementSO
    public StraightBulletMovement() { }

    public void Move(ProjectileBase projectile)
    {
        projectile.transform.position += -projectile.transform.right * Time.deltaTime * projectile.projectileSpeed;
    }
}
