using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
public enum EnemyType
{
    TestEnemy,
}
public class EnemyManager : MonoBehaviour
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
            if (enemy == null)
            {
                //DebugError - nie ma skryptu
                continue;
            }
            EnemyType type = enemy.enemyType;
            if (enemyPrefabDict.ContainsKey(type))
            {
                //DebugError - powtórzenie
                continue;
            }
            enemyPrefabDict.Add(type, prefab);
        }
    }


    public static void Spawn(EnemyType type, Vector3 spawnPosition)
    {
        if (!enemyPoolDict.TryGetValue(type, out Transform value))
        {
            string name = type.ToString() + "Pool";
            GameObject newPool = new GameObject(name);
            Instantiate(newPool, parent);
            enemyPoolDict.Add(type, newPool.GetComponent<Transform>());
            ObjectPooling newPoolingScript = newPool.AddComponent<ObjectPooling>();
            newPoolingScript.prefab = enemyPrefabDict[type];

        }
        ObjectPooling pool = enemyPoolDict[type].GetComponent<ObjectPooling>();
        pool.SpawnObject(spawnPosition);
    }
}
