using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class NextScreen : MonoBehaviour
{
    private bool isLastScene = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            isLastScene = true;
            StartCoroutine(QuitAfterDelay());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex <= 8)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                isLastScene = true;
                StartCoroutine(QuitAfterDelay());
            }
        }
    }

    private IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        Process.GetCurrentProcess().Kill();
    }
}