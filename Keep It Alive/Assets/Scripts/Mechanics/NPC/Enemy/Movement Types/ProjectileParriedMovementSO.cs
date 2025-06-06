using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Parried")]
public class ProjectileParriedMovementSO : ProjectileMovementSO
{
    public Transform target;
    public float projectileSpeed;
    public override IProjectileMovement CreateMovement()
    {
        return new ProjectileParriedMovement(target, projectileSpeed);
    }
}

public class ProjectileParriedMovement : IProjectileMovement
{
    private Transform target;
    private float projectileSpeed;
    public ProjectileParriedMovement(Transform target, float projectileSpeed)    
    { 
        this.target = target;
        this.projectileSpeed = projectileSpeed;
    }

    public void Move(ProjectileBase projectile)
    {
        projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, target.transform.position, Time.deltaTime * projectileSpeed);
    }
}
