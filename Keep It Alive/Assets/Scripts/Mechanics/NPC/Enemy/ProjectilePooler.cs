using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProjectilePooler;

public class ProjectilePooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
    }

    //public IBulletMovement bulletMoveChosen = new StraightBulletMovement();

    #region Singleton

    public static ProjectilePooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public Dictionary<string, GameObject> prefabDictionary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        prefabDictionary = new Dictionary<string, GameObject>();

        // adds each pool to the list with the specific gameobject
        foreach (var pool in pools)
        {
            prefabDictionary.Add(pool.tag, pool.prefab);
            poolDictionary.Add(pool.tag, new Queue<GameObject>());
        }
    }

    public GameObject SpawnFromPool (string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError($"Pool with tag '{tag}' doesn't exist!");
            return null;
        }

        Queue<GameObject> objectPool = poolDictionary[tag];

        if (objectPool.Count > 0)
        {
            // reuse a disable instance of the gameobject
            Debug.Log("spawned projectile");
            GameObject objectToSpawn = objectPool.Dequeue();
            objectToSpawn.SetActive(true);
            return objectToSpawn;
        }
        else
        {
            // if no free instacne in the pool: instanctiate new instance of gameobject
            GameObject newObj = Instantiate(prefabDictionary[tag]);
            return newObj;
        }
    }

    // when projectile is reutrned or used, call this function.
    // pass the gameobject instacne you want to pool
    public void ReturnProjectile(GameObject obj, string tag)
    {
        // if object doesn't have any tags, it destroys the gameobject instead of putting it back in the queue
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log($"Pool with tag {tag} does not exist");
            Destroy (obj);
            return;
        }

        // disables and enqueues for reuse later
        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
