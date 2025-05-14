using UnityEngine;

public interface IDamageable
{
    // How much damage npc takes
    void Damage(float damageAmount);

    // when dies
    void Die();

    // npc max health
    float maxHealth {  get; set; }

    // npc current health
    float currentHealth { get; set; }
}