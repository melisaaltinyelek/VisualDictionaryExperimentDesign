using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ParticipantID : MonoBehaviour
{
    public string fileName = "reaction_times.csv";
    private string participantID = "";

    private void OnGUI()
{
    // Create a new GUIStyle
    GUIStyle style = new GUIStyle(GUI.skin.label);
    style.alignment = TextAnchor.MiddleCenter;
    style.fontSize = 30;

    // Create a label and a text field for the participant ID
    GUI.Label(new Rect(0, Screen.height / 2 - 75, Screen.width, 50), "Participant ID:", style);

    // Create a GUIStyle for the text field with a larger font size
    GUIStyle textFieldStyle = new GUIStyle(GUI.skin.textField);
    textFieldStyle.fontSize = 30;

    participantID = GUI.TextField(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 25, 300, 40), participantID, textFieldStyle);

    // Create a button that saves the participant ID to a CSV file when clicked
    GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
    buttonStyle.fontSize = 25;
    buttonStyle.fixedWidth = 150;
    buttonStyle.fixedHeight = 40;

    if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 50, 150, 40), "Save ID", buttonStyle))
    {
        SaveParticipantID();
    }
}


    private void SaveParticipantID()
    {
         // Save the participant ID
        PlayerPrefs.SetString("ParticipantID", participantID);
        PlayerPrefs.Save();
        // Append the participant ID to a CSV file
        string filePath = Path.Combine(Application.persistentDataPath, "reaction_times.csv");
        StreamWriter writer = new StreamWriter(filePath, true);
        writer.WriteLine(participantID);
        writer.Close();

        // Load the next scene (we can put name of the scene e.g. "welcome screen")
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}

