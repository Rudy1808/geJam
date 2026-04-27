using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MicroWave 
{
    public EnemyType enemyType;
    public int amount;
    public float delay;
}

public class Spawner : MonoBehaviour
{
    public List<List<MicroWave>> waves = new List<List<MicroWave>>();
    public float delayBetweenWave;
    public float delayIncrase;

    void Start()
    {
        
    }
    //IEnumerator SpawnerRutine()
    //{

    //}
    void Update()
    {
        
    }
}
