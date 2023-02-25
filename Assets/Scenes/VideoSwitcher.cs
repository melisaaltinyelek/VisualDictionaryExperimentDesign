using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.IO;

public class VideoSwitcher : MonoBehaviour
{
    public int videosPerTrial = 3;
    public GameObject[] videos;
    public float[][] hazardAppearanceTimes = new float[][] {
        new float[] {5.0f, 10.0f, 15.0f},
        new float[] {6.0f, 12.0f, 18.0f},
        new float[] {7.0f, 14.0f, 21.0f}
    };
    
    private int[] usedVideos;
    private int currentSceneIndex;
    private int currentVideoIndex;
    private float startTime = 0f;
    private float hazardAppearanceTime = 7f;
    private bool hazardActive = false;
    private int videoCounter = 0;
    private string participantID = "";

    void Start()
    {
        Time.timeScale = 1f;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Initialize the usedVideos array
        usedVideos = new int[videosPerTrial];
        for (int i = 0; i < usedVideos.Length; i++)
        {
            usedVideos[i] = -1;
        }

        ShowRandomVideo();
    }

    void Update()
    {
        if (Time.time >= hazardAppearanceTime && !hazardActive)
        {
            hazardActive = true;
            Debug.Log("Hazard active");
        }

        if (hazardActive && Input.GetKeyDown(KeyCode.Space))
        {
            startTime = Time.time;
            Debug.Log("Start time: " + startTime);
        }

        if (startTime > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            float reactionTime = Time.time - hazardAppearanceTime;
            Debug.Log("Reaction time: " + reactionTime + " seconds");
            Debug.Log("Total time: " + Time.time + " seconds");

            startTime = 0f;

            // Append the participant ID and reaction time to a CSV file
            string filePath = Path.Combine(Application.dataPath, "reaction_times.csv");
            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine(participantID + "," + reactionTime.ToString());
            writer.Close();

            videoCounter++;
            if (videoCounter >= videosPerTrial)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
            else
            {
                ShowRandomVideo();
            }
        }
    }

    void ShowRandomVideo()
    {
        if (videos.Length > 0 && currentVideoIndex < videos.Length)
        {
            int newVideoIndex;
            do
            {
                newVideoIndex = Random.Range(0, videos.Length);
            } while (System.Array.IndexOf(usedVideos, newVideoIndex) != -1);

            currentVideoIndex = newVideoIndex;
            usedVideos[videoCounter] = currentVideoIndex;

            for (int i = 0; i < videos.Length; i++)
            {
                videos[i].SetActive(i == currentVideoIndex);
            }

            hazardAppearanceTime = hazardAppearanceTimes[currentVideoIndex][videoCounter];
            Debug.Log("Hazard appearance time: " + hazardAppearanceTime);
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Participant ID:");
        participantID = GUILayout.TextField(participantID, GUILayout.Width(200));
    }
}
