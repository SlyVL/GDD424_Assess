using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTrialManager : MonoBehaviour
{
    //Sets everything needed for the checkpoints such as the checkpoints themselves along with the timer texts
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
    //starts the timer for the race
    public void StartRace()
    {
        timer = 0f;
        currentCheckpoint = 0;
        raceStarted = true;
        raceFinished = false;
        finalTimeText.gameObject.SetActive(false);
    }
    //hit checkpoints and checks if you are hitting it in order along with hitting the last one to finish the race
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



    //finish race timer
    void FinishRace()
    {
        raceFinished = true;
        raceStarted = false;
        finalTimeText.text = "Final Time: " + timer.ToString("F2") + "s";
        finalTimeText.gameObject.SetActive(true);
    }
    //chat gpt to update the timer UI 
    void UpdateTimerUI()
    {
        timerText.text = timer.ToString("F2") + "s";
    }
}
