using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTrialManager : MonoBehaviour
{
    public Transform[] checkpoints;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalTimeText;
    private int currentCheckpoint = 0;
    private float timer = 0f;
    private bool raceStarted = false;
    private bool raceFinished = false;

    void Update()
    {
        if (raceStarted && !raceFinished)
        {
            timer += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    public void StartRace()
    {
        timer = 0f;
        currentCheckpoint = 0;
        raceStarted = true;
        raceFinished = false;
        finalTimeText.gameObject.SetActive(false);
    }

    public void HitCheckpoint(Checkpoint checkpoint)
    {
        if (!raceStarted) return;

        if (checkpoint.checkpointIndex == currentCheckpoint)
        {
            currentCheckpoint++;
            if (currentCheckpoint >= checkpoints.Length)
            {
                FinishRace();
            }
        }
    }

    public bool IsRaceStarted()
    {
        return raceStarted;
    }




    void FinishRace()
    {
        raceFinished = true;
        raceStarted = false;
        finalTimeText.text = "Final Time: " + timer.ToString("F2") + "s";
        finalTimeText.gameObject.SetActive(true);
    }

    void UpdateTimerUI()
    {
        timerText.text = timer.ToString("F2") + "s";
    }
}
