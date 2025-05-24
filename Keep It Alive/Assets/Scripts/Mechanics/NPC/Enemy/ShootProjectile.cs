using Unity.VisualScripting;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public Projectile projectileType;
    public Transform enemyProjectileLauchOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            Instantiate(projectileType, enemyProjectileLauchOffset.position, transform.rotation);
        }
    }
}
