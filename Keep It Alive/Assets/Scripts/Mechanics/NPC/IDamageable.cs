using UnityEngine;

public interface IDamageable
{
    // How much damage npc takes
    void Damage(int damageAmount);

    // when dies
    void Die();

    // npc max health
    int maxHealth {  get; set; }

    // npc current health
    int currentHealth { get; set; }
}