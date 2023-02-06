using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionTimes : MonoBehaviour
{
    private float startTime=0;
    private float hazardAppearanceTime = 7f;
    private bool hazardActive = false;
    
    private void start ()
    {
        startTime = Time.time;
        Time.timeScale = 1;
        Debug.Log("Start method called");
        
    }

    private void Update()
    {
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
            startTime = 0;
        }
        //if (Input.GetKeyDown(KeyCode.Space) && hazardActive)
        //{
            //float reactionTime = Time.time - startTime;
            //Debug.Log("Reaction time: " + reactionTime + " seconds");
        //}
    }
}

