using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]

public class WaveConfig : ScriptableObject
{

    [SerializeField] GameObject enemyPrefab, pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f, spawnRandomFactor = 0.3f, moveSpeed = 2f;
    [SerializeField] int numberOfEnemies = 5;


    // Method that sets each waypoint's transform in a wave path
    public List<Transform> GetWaypoints()
    {
        // Creating a list of empty Transforms to use their positions
        var waveWaypoints = new List<Transform>();

        // Setting each transform to be each waypoint's transform in the wave path
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }


    // Getters 
    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public float GetTimeBetweenSpawns()  { return timeBetweenSpawns; }

    public float GeSpawnRandomFactor() { return spawnRandomFactor; }

    public float GetMoveSpeed() { return moveSpeed; }

    public int GetNumberOfEnemies() { return numberOfEnemies; }
}
