using UnityEngine;


/*public enum AimType
{
    intialFollow,
    continuousFollow,
    noFollow
}
*/
/*public enum ActionsWhileShooting
{
    randomTeleport,
    circleTeleport,
    noAction
}*/

public enum TypeofProjectile
{
    redProjectile,
    greenProejctile
}


[CreateAssetMenu(menuName = "Scriptable Objects/MakeProjectile")]

public class ProjectileType : ScriptableObject
{
/*    public AimType aimType;
    public ActionsWhileShooting shootingAction;*/
    public TypeofProjectile projectileType;
    public Sprite projectileImage;
    public int fireAmount;
    public float fireRate;
}
