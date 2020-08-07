using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{
    WaveConfig waveConfig;
    [SerializeField] List<Transform> waveWaypoints;

    int waypointIndex = 0;
    float waveMoveSpeed = 0f;
    bool mustDie = false;

    // Start is called before the first frame update
    void Start()
    {
        waveWaypoints = waveConfig.GetWaypoints();
        transform.position = waveWaypoints[0].transform.position;
        waveMoveSpeed = waveConfig.GetMoveSpeed();
    }


    // Setts what's the actual enemy wave
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }


    // Update is called once per frame
    void Update()
    {
        // Make enemy follow it's wave path
        FollowPath();
    }


    // Method to enemy follow it's wave path in visible time
    private void FollowPath()
    {
        if (waypointIndex <= waveWaypoints.Count - 1)
        {
            // Getting first waypoint in wave path
            var targetPosition = waveWaypoints[waypointIndex].transform.position;

            // Making it visible for humans
            var movementThisFrame = waveMoveSpeed * Time.deltaTime;

            // Actual movement already in human time
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            // Getting next waypoint in wave path
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            mustDie = true;
        }
    }

    // Getters
    public bool GetMustDie()
    {
        return mustDie;
    }

}
