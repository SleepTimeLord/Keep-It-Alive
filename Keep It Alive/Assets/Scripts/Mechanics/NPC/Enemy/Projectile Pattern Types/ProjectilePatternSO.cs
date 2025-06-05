using UnityEngine;

public abstract class ProjectilePatternSO : ScriptableObject
{
    public abstract IProjectilePattern CreatePattern(); 
}
