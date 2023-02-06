using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReactionTimes2 : MonoBehaviour
{
    // Variables to store the start time, reaction start time, and target time
    private float startTime = 0;
    private float targetTime = 7.0f;

    // Flag to track if the reaction timer is running
    private bool timerRunning;

    // Start method is called once when the video starts
    private void Start()
    {
        // Store the start time of the video
        startTime = Time.time;
        Time.timeScale = 1;
        timerRunning = true;
    }

    // Update method is called every frame
    private void Update()
    {
        // Calculate the current time
        //float currentTime = Time.time - startTime;

        // Check if the reaction timer should start
        if (startTime > 0 && timerRunning)
        {
            timerRunning = true;
             Debug.Log("startTime: " + startTime);
        }
        if (timerRunning && Input.GetKeyDown(KeyCode.Space))
        {
            startTime = Time.time;
           
        }
        if (startTime == 0)
        {
            startTime = Time.time;
        }

        // Check if the space key is pressed
        if (timerRunning && Input.GetKeyDown(KeyCode.Space))
        {
            timerRunning = false;
            float elapsedTime = Time.time - targetTime;
            Debug.Log("Total time: " + Time.time + " seconds");
            Debug.Log("Reaction time: " + elapsedTime + " seconds");
        }
    }
}
