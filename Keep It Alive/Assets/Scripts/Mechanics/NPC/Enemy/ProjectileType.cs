using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/MakeProjectile")]
public class ProjectileType : ScriptableObject
{
    public ProjectileBase projectileBase;
    public int fireAmount;
    public float fireRate;
}
