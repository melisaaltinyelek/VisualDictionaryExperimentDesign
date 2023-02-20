using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSwitcher: MonoBehaviour
{
    public GameObject[] videos;
    private int currentVideoIndex;

    void Start()
    {
        ShowRandomVideo();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowRandomVideo();
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

