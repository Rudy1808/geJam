using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public struct MicroWave 
{
    public EnemyType enemyType;
    public int amount;
    public float delay;
}
[System.Serializable]
public class Wave
{
    public List<MicroWave> microWaves;
}

public class Spawner : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();
    public float delayBetweenWave;
    public float delayIncrase;

    void Start()
    {
        StartCoroutine(SpawnerRutine());
    }
    IEnumerator SpawnerRutine()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            for (int j = 0; j < waves[i].microWaves.Count; j++)
            {
                for (int k = 0; k < waves[i].microWaves[j].amount; k++)
                {
                    yield return new WaitForSeconds(waves[i].microWaves[j].delay);
                    //PoolManager.Spawn(waves[i].microWaves[j].enemyType,transform.position);
                    //game.explode();
                    Debug.Log("spawn");
                }
            }
            yield return new WaitForSeconds(delayBetweenWave);
            delayBetweenWave += delayIncrase;
        }
    }
}
