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
    public Dictionary<string, float> hazardAppearanceToVideoMap = new Dictionary<string, float> {
        {"Video_1", 5.0f},
        {"Video_2", 7.0f},
        {"Video_3", 10.0f},
        {"Video_4", 13.0f},
        {"Video_5", 17.0f},
        {"Video_6", 19.0f},
        {"Video_7", 5.0f},
        {"Video_8", 3.0f},
        {"Video_9", 12.0f}
    };

    private int[] usedVideos;
    private int currentSceneIndex;
    private int currentVideoIndex;
    private float hazardAppearanceTime = 0f;
    private float startTime = 0f;
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

            string filePath = Path.Combine(Application.dataPath, "reaction_times.csv");
            StreamWriter writer = new StreamWriter(filePath, true);
            string videoName = videos[currentVideoIndex].GetComponent<VideoPlayer>().clip.name;
            Debug.Log("Video name: " + videoName);
            writer.WriteLine(participantID + "," + reactionTime.ToString() + "," + videoName);
            writer.Close();

            videoCounter++;
            if (videoCounter >= videosPerTrial)
            {
                videoCounter = 0;
                SceneManager.LoadScene(currentSceneIndex + 1 > SceneManager.sceneCountInBuildSettings ? 0 : currentSceneIndex + 1);
            }
            else
            {
                ShowRandomVideo();
            }
        }
    }

   void ShowRandomVideo()
{
    if (videos.Length > 0 && videoCounter < videosPerTrial)
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

        VideoPlayer currentVideoPlayer = videos[currentVideoIndex].GetComponent<VideoPlayer>();
        currentVideoPlayer.Prepare();

        string videoName = currentVideoPlayer.clip.name;
        if (hazardAppearanceToVideoMap.TryGetValue(videoName, out float hazardTime))
        {
            hazardAppearanceTime = hazardTime;
            hazardActive = false;
            Debug.Log("Hazard appearance time: " + hazardAppearanceTime);
        }
        else
        {
            Debug.LogWarning("Hazard appearance time not found for video: " + videoName);
        }

        currentVideoPlayer.started += (vp) =>
        {
            hazardActive = true;
            Debug.Log("Hazard active");
        };

        currentVideoPlayer.seekCompleted += (vp) =>
        {
            if (hazardActive && startTime == 0)
            {
                startTime = Time.time;
                Debug.Log("Start time: " + startTime);
            }
        };
    }
}

    // private void OnGUI()
    // {
    //     GUILayout.Label("Participant ID:");
    //     participantID = GUILayout.TextField(participantID, GUILayout.Width(200));
    // }
}
