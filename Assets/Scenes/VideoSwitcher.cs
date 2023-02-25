using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSwitcher : MonoBehaviour
{
    private float startTime = 0;
    private float hazardAppearanceTime = 7f;
    private bool hazardActive = false;
    private int videoCounter = 0;
    public int videosPerTrial = 3;
    public GameObject[] videos;
    public float[][] hazardAppearanceTimes = new float[][]
    {
        new float[] {5.0f, 10.0f, 15.0f},
        new float[] {6.0f, 12.0f, 18.0f},
        new float[] {7.0f, 14.0f, 21.0f}
    };
    private int[] usedVideos;
    private int currentSceneIndex;
    private int currentVideoIndex;

    void Start()
    {
        startTime = Time.time;
        Time.timeScale = 1;
        Debug.Log("Start method called");

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowRandomVideo();

            videoCounter++;
            if (videoCounter >= videosPerTrial)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
        }

        if (Time.time >= hazardAppearanceTime && !hazardActive)
        {

            hazardActive = true;
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

            startTime = 0;
        }

    }

    private List<int> playedVideoIndices = new List<int>();

    void ShowRandomVideo()
    {
        if (videos.Length > 0 && playedVideoIndices.Count < videos.Length)
        {
            int newVideoIndex;
            do
            {
                newVideoIndex = Random.Range(0, videos.Length);
            } while (playedVideoIndices.Contains(newVideoIndex));

            currentVideoIndex = newVideoIndex;

            playedVideoIndices.Add(currentVideoIndex);

            for (int i = 0; i < videos.Length; i++)
            {
                videos[i].SetActive(i == currentVideoIndex);
            }
            hazardAppearanceTime = hazardAppearanceTimes[currentVideoIndex][playedVideoIndices.Count - 1];
            Debug.Log("Hazard appearance time: " + hazardAppearanceTime);
        }
    }
}
