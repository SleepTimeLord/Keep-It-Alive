using UnityEngine;

// TODO: Edit this script so that it takes in IBulletPattern and lists the specifics of what vars they need
// this includes the fireAmount and fireRate

public enum TypeofProjectile
{
    redProjectile,
    greenProejctile
}


[CreateAssetMenu(menuName = "Scriptable Objects/MakeProjectile")]
public class EnemyAttackSO : ScriptableObject
{
    public ProjectileMovementSO projectileMovement;
    public ProjectilePatternSO projectilePattern;
    public int fireAmount;
    public float fireRate;
}
