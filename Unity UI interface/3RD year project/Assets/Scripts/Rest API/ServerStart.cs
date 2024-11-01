using UnityEngine;
using System.Diagnostics;
public class ServerStart : MonoBehaviour
{
    // Specify the path to your .bat file here
    private string batFilePath = "C:/PythonNNApi/run.bat";

    void Start()
    {
        Application.OpenURL(batFilePath);
    }

    //private void RunBatFile()
    //{
    //    if (!string.IsNullOrEmpty(batFilePath))
    //    {
    //        ProcessStartInfo processInfo = new ProcessStartInfo
    //        {
    //            FileName = batFilePath,
    //            UseShellExecute = true
    //        };

    //        try
    //        {
    //            Process process = Process.Start(processInfo);
    //            UnityEngine.Debug.Log("Batch file started successfully.");
    //        }
    //        catch (System.Exception e)
    //        {
    //            UnityEngine.Debug.LogError("Failed to start batch file: " + e.Message);
    //        }
    //    }
    //    else
    //    {
    //        UnityEngine.Debug.LogError("Batch file path is not set.");
    //    }
    //}
}
