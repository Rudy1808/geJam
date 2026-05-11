using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float delayIncrease;
    public Path path;

    private bool end = false;
    public Sprite WinScreen;
    public Sprite LoseScreen;
    public GameObject UI;

    void Start()
    {
        StartCoroutine(SpawnerRutine());
    }

    private void Update()
    {
        if (end && PoolManager.AllEnemyCount == 0)
        {
            GameObject.FindWithTag("End").GetComponent<SpriteRenderer>().sprite = WinScreen;
            UI.SetActive(false);
        }
        else if (Cave.HP <= 0)
        {
            GameObject.FindWithTag("End").GetComponent<SpriteRenderer>().sprite = LoseScreen;
            UI.SetActive(false);
        }
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
                    PoolManager.Spawn(waves[i].microWaves[j].enemyType, transform.position, path);
                    //Debug.Log("spawn");
                }
            }
            yield return new WaitForSeconds(delayBetweenWave);
            delayBetweenWave += delayIncrease;
        }
        end = true;
    }
}
