using System.Collections.Generic;
using UnityEngine;

public class MultiObjectPool : MonoBehaviour
{
    public static MultiObjectPool Instance { get; private set; }

    public List<GameObject> objectPrefabs;
    public int initialPoolSize = 10;

    private Dictionary<string, Queue<GameObject>> objectPoolDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeObjectPool();
    }

    private void InitializeObjectPool()
    {
        objectPoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (GameObject prefab in objectPrefabs)
        {
            string poolKey = prefab.name;
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject obj = Instantiate(prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            objectPoolDictionary.Add(poolKey, objectPool);
        }
    }

    public GameObject SpawnFromPool(string poolKey, Vector3 position, Quaternion rotation)
    {
        if (objectPoolDictionary.ContainsKey(poolKey))
        {
            GameObject objectToSpawn = objectPoolDictionary[poolKey].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            objectPoolDictionary[poolKey].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        Debug.LogWarning("Object pool with key " + poolKey + " does not exist.");
        return null;
    }
}

