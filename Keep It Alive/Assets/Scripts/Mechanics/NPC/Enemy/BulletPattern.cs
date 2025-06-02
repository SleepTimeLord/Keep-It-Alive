using UnityEngine;

// different types of bullet patterns to shoot
public class BulletPattern : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // straight bullet shot
    public void Straight(float projectileSpeed, Transform projectile)
    {
        // this is based of of the rotation of the intial spawn of the
        projectile.position += -transform.right * Time.deltaTime * projectileSpeed;


    }
    
    public void SinWave(float projectileSpeed, Transform projectile)
    {

    }
}
