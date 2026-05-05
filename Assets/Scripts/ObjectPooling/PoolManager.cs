using System;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    Bronze,
    Maid,
    BronzeGreen,
    BronzeRed,
    Catnip,
    GoldBlue,
    GoldPurple,
    Rudy,
    Silver,
    SilverBoots,
    SilverRed,
    Slipper,
    Wizard
}
public class PoolManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    static Dictionary<EnemyType, Transform> enemyPoolDict = new Dictionary<EnemyType, Transform>();
    static Dictionary<EnemyType, GameObject> enemyPrefabDict = new Dictionary<EnemyType, GameObject>();
    static Transform parent;
    private static int _allEnemyCount = 0;
    public static int AllEnemyCount 
    {
        get
        {
            foreach (Transform transform in enemyPoolDict.Values) {
                ObjectPooling pool = transform.GetComponent<ObjectPooling>();
                return pool.enemyCount;
            }

            return _allEnemyCount;
        }
        private set
        {
            _allEnemyCount = value;
        }
    }

    private void Awake()
    {
        parent = GetComponent<Transform>();

        foreach (GameObject prefab in prefabs)
        {
            Enemy enemy = prefab.GetComponent<Enemy>();

            EnemyType type = enemy.enemyType;

            enemyPrefabDict.Add(type, prefab);
        }
    }

    private void Start()
    {
        foreach(EnemyType type in Enum.GetValues(typeof(EnemyType)))
        {
            string name = type.ToString() + "Pool";

            GameObject newPool = new GameObject(name);
            newPool.transform.SetParent(parent);

            enemyPoolDict.Add(type, newPool.transform);
            newPool.SetActive(false);
            newPool.AddComponent<ObjectPooling>().prefab = enemyPrefabDict[type];
            newPool.SetActive(true);
        }
    }

    public static void Spawn(EnemyType type, Vector3 spawnPosition, Path path)
    {
        ObjectPooling pool = enemyPoolDict[type].GetComponent<ObjectPooling>();
        GameObject obj = pool.SpawnObject(spawnPosition);
        PathFollowing pathFollowing = obj.GetComponent<PathFollowing>();
        pathFollowing.path = path;
    }
}
