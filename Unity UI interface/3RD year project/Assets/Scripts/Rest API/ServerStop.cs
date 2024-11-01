using UnityEngine;
using System.Diagnostics;
using System.IO;

public class ServerStop : MonoBehaviour
{
    // Specify the path to your .bat file here
    private string batFilePath = "C:/PythonNNApi/run.bat";
    private string filePath = "C:/PythonNNApi/";

    private string serverStatusFilePath;
    public bool isServerRunning { get; private set; }

    private void Start()
    {
        
    }

    private void OnApplicationQuit()
    {
        DeleteServerStatusFile();
    }

    private void OnDestroy()
    {
        DeleteServerStatusFile();
    }

    private void DeleteServerStatusFile()
    {
        if (File.Exists(serverStatusFilePath))
        {
            File.Delete(serverStatusFilePath);
        }
    }
}
