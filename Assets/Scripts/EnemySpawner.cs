using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool loop = false;
    Vector2 Vec = new Vector2(0, 10);

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllEnemyWaves());
        }
        while (loop);
    }


    // Coroutine that spawns all enemies in a single wave
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            // Spawns an enemy
            var newEnemyWave = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);

            // Make it follow it's wave path
            newEnemyWave.GetComponent<Pathing>().SetWaveConfig(waveConfig);

            // Wait it's wave TimeBetweenSpawns to spawn another enemy in the same wave
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }


    // Coroutine that spawns all enemy waves
    private IEnumerator SpawnAllEnemyWaves()
    {
        for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            // Sets actual wave        
            var currentWave = waveConfigs[waveIndex];

            // Wait for a wave to be entirely spawned before spawning next wave
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

}
