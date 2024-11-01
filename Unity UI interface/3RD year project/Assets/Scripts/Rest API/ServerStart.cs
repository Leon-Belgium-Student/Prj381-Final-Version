using UnityEngine;
using System.Diagnostics;
using System.IO;

public class ServerStart : MonoBehaviour
{
    // Specify the path to your .bat file here
    private string batFilePath = "C:/PythonNNApi/run.bat";
    public bool isServerRunning { get; private set; }
    public WaitForServer serverBtnInteraction;

    private void Start()
    {
        if (serverBtnInteraction == null)
        {
            serverBtnInteraction = this.GetComponent<WaitForServer>();
        }
        isServerRunning = ServerStarted.ServerRunning;
        if (isServerRunning)
        {
            serverBtnInteraction.ActivateBtns();
        }
    }

    public void StartServer()
    {
        if (ServerStarted.ServerRunning == false)
        {
            isServerRunning = true;
            startbatFile();
        }
    }


    public void startbatFile()
    {
        ServerStarted.ServerRunning = isServerRunning;
        Application.OpenURL(batFilePath);
        if (serverBtnInteraction != null)
        {
            serverBtnInteraction.StartWait();
        }
    }

}
