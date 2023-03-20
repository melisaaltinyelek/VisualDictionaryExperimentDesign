using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.IO;

public class VideoSwitcher : MonoBehaviour
{
    public int videosPerTrial = 15;
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
        {"Video_9", 12.0f},
        {"Video_10", 13.0f},
        {"Video_11", 17.0f},
        {"Video_12", 19.0f},
        {"Video_13", 5.0f},
        {"Video_14", 3.0f},
        {"Video_15", 12.0f},
        {"Video_16", 5.0f},
        {"Video_17", 7.0f},
        {"Video_18", 10.0f},
        {"Video_19", 13.0f},
        {"Video_20", 17.0f},
        {"Video_21", 19.0f},
        {"Video_22", 5.0f},
        {"Video_23", 3.0f},
        {"Video_24", 12.0f},
        {"Video_25", 13.0f},
        {"Video_26", 17.0f},
        {"Video_27", 19.0f},
        {"Video_28", 5.0f},
        {"Video_29", 3.0f},
        {"Video_30", 12.0f},
        {"Video_31", 5.0f},
        {"Video_32", 7.0f},
        {"Video_33", 10.0f},
        {"Video_34", 13.0f},
        {"Video_35", 17.0f},
        {"Video_36", 19.0f},
        {"Video_37", 5.0f},
        {"Video_38", 3.0f},
        {"Video_39", 12.0f},
        {"Video_40", 13.0f},
        {"Video_41", 17.0f},
        {"Video_42", 19.0f},
        {"Video_43", 5.0f},
        {"Video_44", 3.0f},
        {"Video_45", 12.0f}
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
        // Get the participant ID
        participantID = PlayerPrefs.GetString("ParticipantID");
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
        
            // Save the participant ID to player preferences
            PlayerPrefs.SetString("participantID", participantID);

            // Write the reaction times to a CSV file
            string filePath = Path.Combine(Application.dataPath, "reaction_times.csv");

            // Check if the file exists
            bool fileExists = File.Exists(filePath);

            StreamWriter writer = new StreamWriter(filePath, true);

            // Write the column titles only if the file doesn't exist
            if (!fileExists)
            {
                writer.WriteLine("ID,RTs,Video Name");
            }

            string videoName = videos[currentVideoIndex].GetComponent<VideoPlayer>().clip.name;
            Debug.Log("Video name: " + videoName);

            // Get the participant ID from player preferences
            string savedParticipantID = PlayerPrefs.GetString("participantID");

            // Write the data for the current video
            writer.WriteLine(savedParticipantID + "," + reactionTime.ToString() + "," + videoName);

            writer.Close();


            videoCounter++;

            // //  if (videoCounter >= videosPerTrial)
            // // {
            // //     videoCounter = 0;
            // //     if (currentSceneIndex == 8 && currentVideoIndex == videosPerTrial - 1) // if it is the last scene of the 3rd trial and the last video of the trial
            // //     {
            // //         Application.Quit(); // quit the application
            // //     }
            // //     else
            // //     {
            // //         SceneManager.LoadScene(currentSceneIndex + 1 > SceneManager.sceneCountInBuildSettings ? 0 : currentSceneIndex + 1);
            // //     }
            // // }
            // // else
            // // {
            // //     ShowRandomVideo();
            // }
            
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