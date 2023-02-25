using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ParticipantID : MonoBehaviour
{
    public string fileName = "ParticipantIDs.csv";
    private string participantID = "";

    private void OnGUI()
    {
        // Create a label and a text field for the participant ID
        GUILayout.Label("Participant ID:");
        participantID = GUILayout.TextField(participantID, GUILayout.Width(200));

        // Create a button that saves the participant ID to a CSV file when clicked
        if (GUILayout.Button("Save ID", GUILayout.Width(100)))
        {
            SaveParticipantID();
        }
    }

    private void SaveParticipantID()
    {
        // Append the participant ID to a CSV file
        string filePath = Path.Combine(Application.dataPath, "participantIDs.csv");
        StreamWriter writer = new StreamWriter(filePath, true);
        writer.WriteLine(participantID);
        writer.Close();

        // Load the next scene (we can put name of the scene e.g. "welcome screen")
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
