using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> WaveConfigs=null;
    [SerializeField] bool looping = false;
    int StartingWave =0;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);

    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = StartingWave; waveIndex < WaveConfigs.Count; waveIndex++)
        {
            WaveConfig currentWave = WaveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
           GameObject newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            float random = UnityEngine.Random.Range(0f, waveConfig.GetSpawnRandomFactor());

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns()+random);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
