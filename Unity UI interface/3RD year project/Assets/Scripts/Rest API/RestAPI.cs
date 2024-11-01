using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class RestAPI : MonoBehaviour
{
    public string filePathInput;
    //public Image previewImage;
    public Text resultText;
    public Text confidenceText;
    private string uploadUrl = "http://127.0.0.1:8000/predict";
    public WaitForServer serverBtnInteraction;

    public bool StartedUpload = false;

    private void Update()
    {
        if (serverBtnInteraction != null && !StartedUpload)
        {
            confidenceText.text = "";
            if (!serverBtnInteraction.WaitDone && !serverBtnInteraction.WaitStarted)
            {
                resultText.text = "Please start the server to process the images";
            }
            else if (!serverBtnInteraction.WaitDone && serverBtnInteraction.WaitStarted)
            {
                resultText.text = "Server is starting please wait for your cmd to finish";
            }
            else
            {
                resultText.text = "Upload a image to start";
            }
        }
    }

    // Method to call when uploading
    public void UploadImage(string filePathFromImage)
    {
        StartedUpload = true;
        if (string.IsNullOrEmpty(filePathFromImage))
        {
            resultText.text = "Please enter the file path.";
            confidenceText.text = "";

            return;
        }
        StartCoroutine(UploadImageCoroutine(filePathFromImage));
    }

    public void UploadImage()
    {
        StartedUpload = true;
        if (string.IsNullOrEmpty(filePathInput))
        {
            resultText.text = "Please enter the file path.";
            confidenceText.text = "";

            return;
        }
        StartCoroutine(UploadImageCoroutine(filePathInput));
    }

    private IEnumerator UploadImageCoroutine(string filePath)
    {
        // Load the file as a texture
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);

        // Prepare the form data
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, System.IO.Path.GetFileName(filePath), "image/jpeg");

        // Send the POST request
        using (UnityWebRequest www = UnityWebRequest.Post(uploadUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                resultText.text = $"Error: {www.error}";
                confidenceText.text = "";
            }
            else
            {
                string jsonResult = www.downloadHandler.text;
                Debug.Log("Server response: " + jsonResult);

                // Extract the class and confidence using regular expressions
                string classValue = Regex.Match(jsonResult, "\"Class\":\"(.*?)\"").Groups[1].Value;
                var confidenceValue = Regex.Match(jsonResult, "\"Confidence\":(\\d+(?:\\.\\d+)?)").Groups[1].Value;

                // Display results
                resultText.text = $"Class: {classValue}";


                char[] ConArray = confidenceValue.ToCharArray();
                string finalConfidence = "";

                int loopMax = ConArray.Length;
                int loopCounter = 0;
                if (loopMax >6)
                {
                    loopCounter = 6;
                }
                else
                {
                    loopCounter = loopMax;
                }
                for (int i = 2; i < loopCounter; i++)
                {
                    if (i!= 4)
                    {
                        finalConfidence = finalConfidence + ConArray[i];
                    }
                    else
                    {
                        finalConfidence = finalConfidence + ",";
                    }
                }
                confidenceText.text = $"Confidence: {finalConfidence}%";
            }
        }
    }

    // Class to parse the JSON response
    [System.Serializable]
    public class PredictionResult
    {
        public string Class;
        public float Confidence;
    }
}
