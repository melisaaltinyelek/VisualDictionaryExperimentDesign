using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSwitcher: MonoBehaviour
{

    private float startTime=0;
    private float hazardAppearanceTime = 7f;
    private bool hazardActive = false;



    public GameObject[] videos;
    private int currentVideoIndex;

    void Start()
    {
        startTime = Time.time;
        Time.timeScale = 1;
        Debug.Log("Start method called");

        ShowRandomVideo();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowRandomVideo();
        }

        if (Time.time >= hazardAppearanceTime && !hazardActive)
        {
            
            hazardActive = true;
            //Debug.Log("startTime: " + startTime);
            Debug.Log("Hazard active");
        }
        if (hazardActive && Input.GetKeyDown(KeyCode.Space))
        {
            startTime = Time.time;
            Debug.Log("startTime: " + startTime);
        }
          if (startTime == 0)
        {
            startTime = Time.time;
        }
    
        if (startTime > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            float reactionTime = Time.time - hazardAppearanceTime;
            Debug.Log("Reaction time: " + reactionTime + " seconds");
            Debug.Log("Total time: " + Time.time + " seconds");

            // Write the reaction time to the file
            //File.AppendAllText("reaction_times.csv", reactionTime + "\n");
            startTime = 0;
        }

    }

    void ShowRandomVideo()
    {
        if (videos.Length > 0)
        {
            int newVideoIndex = currentVideoIndex;

            while (newVideoIndex == currentVideoIndex)
            {
                newVideoIndex = Random.Range(0, videos.Length);
            }

            currentVideoIndex = newVideoIndex;

            for (int i = 0; i < videos.Length; i++)
            {
                videos[i].SetActive(i == currentVideoIndex);
            }
        }
    }
}

