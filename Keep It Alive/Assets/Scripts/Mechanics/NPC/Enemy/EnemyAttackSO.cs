using UnityEngine;

// TODO: Edit this script so that it takes in IBulletPattern and lists the specifics of what vars they need
// this includes the fireAmount and fireRate

public enum TrackingType
{
    noTracking,
    initialTracking,
    continuedTracking
}


[CreateAssetMenu(menuName = "Enemy/Attacks")]
public class EnemyAttackSO : ScriptableObject
{
    public ProjectileMovementSO projectileMovement;
    public ProjectilePatternSO projectilePattern;
    public string projectileTag;
    public TrackingType trackingType;
    public float projectileSpeed;
    public int projectileDamage;
    public int fireAmount;
    public float fireRate;
}
