using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject prefab;
    public int enemyCount;

    Queue<GameObject> ObjectPool = new Queue<GameObject>();

    public void CreateObject()
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        ObjectPool.Enqueue(obj);
    }
    public GameObject SpawnObject(Vector3 position)
    {
        if (ObjectPool.Count <= 0)
            CreateObject();

        GameObject obj = ObjectPool.Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        enemyCount++;

        return obj;
    }
    public void DespawnObject(GameObject obj)
    {
        enemyCount--;
        obj.SetActive(false);
        ObjectPool.Enqueue(obj);
    }
} 
