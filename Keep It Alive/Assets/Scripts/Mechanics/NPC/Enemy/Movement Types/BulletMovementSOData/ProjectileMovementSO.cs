using UnityEngine;

public abstract class ProjectileMovementSO : ScriptableObject
{
    // createMovemenet creates a new instance of a movement type like straight or wave
    public abstract IProjectileMovement CreateMovement();
}
