using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        TimeTrialManager manager = FindObjectOfType<TimeTrialManager>();

        // If first checkpoint, start race
        if (checkpointIndex == 0 && !manager.IsRaceStarted())
        {
            manager.StartRace();
        }

        // Notify the manager this checkpoint was hit
        manager.HitCheckpoint(this);
    }
}


