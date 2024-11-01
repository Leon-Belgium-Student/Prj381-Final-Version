using UnityEngine;
using System.Diagnostics;
using System.IO;

public class ServerStart : MonoBehaviour
{
    // Specify the path to your .bat file here
    private string batFilePath = "C:/PythonNNApi/run.bat";

    private string serverStatusFilePath;
    public bool isServerRunning { get; private set; }

    private void Start()
    {
        serverStatusFilePath = Path.Combine(Application.streamingAssetsPath, "The server is up and running");

        // Check if the server status file exists
        if (File.Exists(serverStatusFilePath))
        {
            isServerRunning = true;
        }
        else
        {
            isServerRunning = false;
            CreateServerStatusFile();
            StartServer();  // Your function to start the server
        }
    }
    private void Update()
    {
            
    }

    private void CreateServerStatusFile()
    {
        // Create the server status file to signal that the server is running
        File.WriteAllText(serverStatusFilePath, "The server is up and running");
    }

    private void StartServer()
    {
        isServerRunning = true;
        startbatFile();
    }

    public void GoBackToHomePage()
    {
        // Check the server status file before starting the server again
        if (!isServerRunning)
        {
            StartServer();
        }
    }

    public void startbatFile()
    {
        Application.OpenURL(batFilePath);
    }

}
