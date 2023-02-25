// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;
// using UnityEngine.SceneManagement;

// public class ReactionTimes : MonoBehaviour
// {
//     private float startTime=0;
//     private float hazardAppearanceTime = 7f;
//     private bool hazardActive = false;

//     private void Start ()
//     {
//         startTime = Time.time;
//         Time.timeScale = 1;
//         Debug.Log("Start method called");
        
//         // Check if the file already exists. If it doesn't, create the file and add a header row
//         //if (!File.Exists("reaction_times.csv"))
//         //{
//             //File.WriteAllText("reaction_times.csv", "Reaction Time (seconds)\n");
//         //}
//     }

//     private void Update()
//     {
//         if (Time.time >= hazardAppearanceTime && !hazardActive)
//         {
            
//             hazardActive = true;
//             //Debug.Log("startTime: " + startTime);
//             Debug.Log("Hazard active");
//         }
//         if (hazardActive && Input.GetKeyDown(KeyCode.Joystick1Button0))
//         {
//             startTime = Time.time;s
//             Debug.Log("startTime: " + startTime);
//         }
//           if (startTime == 0)
//         {
//             startTime = Time.time;
//         }
    
//         if (startTime > 0 && Input.GetKeyDown(KeyCode.Joystick1Button0))
//         {
//             float reactionTime = Time.time - hazardAppearanceTime;
//             Debug.Log("Reaction time: " + reactionTime + " seconds");
//             Debug.Log("Total time: " + Time.time + " seconds");

//             // Write the reaction time to the file
//             //File.AppendAllText("reaction_times.csv", reactionTime + "\n");
//             startTime = 0;

//             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
//         }



        
//     }
// }
