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

        // If colliding with first checkpoint start the race
        if (checkpointIndex == 0 && !manager.IsRaceStarted())
        {
            manager.StartRace();
        }

        //let the manager know the checkpoint was hit 
        manager.HitCheckpoint(this);
    }
}


