using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    ProjectilePooler pooler;
    public float projectileSpeed = 5;
    private GameObject projectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pooler = ProjectilePooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {

            projectile = pooler.SpawnFromPool("redObject");
            // sets it to the bullet spawn
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;
            // shoot 
/*            projectile.transform.position += -projectile.transform.right * Time.deltaTime * projectileSpeed;
*/
        }
    }


}
