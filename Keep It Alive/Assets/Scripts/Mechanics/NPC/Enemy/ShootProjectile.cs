using Unity.VisualScripting;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public ProjectileBase projectileType;
    public Transform enemyProjectileLauchOffset;
    private int amountFired;
    private float delayTimer;
    private bool canFire;
    public int fireAmount = 2;
    public float repeatDelay = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
/*        canFire = true;
        amountFired = 0;
        delayTimer = 0;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (amountFired < fireAmount && canFire)
        {
        }
        Shoot();
    }

    void Shoot()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectileType, enemyProjectileLauchOffset.position, transform.rotation);
            /*            amountFired++;
                        canFire = false;
                        delayTimer = repeatDelay;
                        StartShotDelay();*/
        }
    }

/*    private void StartShotDelay()
    {
        delayTimer -= Time.deltaTime;
        Debug.Log("thisworks: " + delayTimer);

        if (delayTimer < 0)
        {
            Debug.Log("thisworks");
            amountFired = 0;
            canFire = true;
        }
    }*/
}
