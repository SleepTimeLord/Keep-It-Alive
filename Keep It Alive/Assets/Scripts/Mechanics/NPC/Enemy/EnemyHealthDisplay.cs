using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class EnemyHealthDisplay : MonoBehaviour
{
    private Enemy enemy;
    public Image healthBar;
    void Start()
    {
        enemy = FindAnyObjectByType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        float maxHealth = enemy.maxHealth;
        float health = enemy.currentHealth;
        float frac = health / maxHealth;
        healthBar.fillAmount = Mathf.Clamp(frac, 0, 1);
    }
}
