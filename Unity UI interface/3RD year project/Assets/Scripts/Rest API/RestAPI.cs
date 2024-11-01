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

    // Method to call when uploading
    public void UploadImage(string filePathFromImage)
    {        
        if (string.IsNullOrEmpty(filePathFromImage))
        {
            resultText.text = "Please enter the file path.";
            return;
        }
        StartCoroutine(UploadImageCoroutine(filePathFromImage));
    }

    public void UploadImage()
    {
        if (string.IsNullOrEmpty(filePathInput))
        {
            resultText.text = "Please enter the file path.";
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

                //Debug.Log(confidenceValue);
                //Debug.Log(confidenceValue.GetType());
                //Debug.Log(confidenceLevel);
                //confidenceText.text = $"Confidence: {confidenceLevel}%";

                char[] ConArray = confidenceValue.ToCharArray();

                //Debug.Log($" the loop confidence is {confidenceValue}");
                //Debug.Log($" the classification is {classValue}");

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

                //Debug.Log($" the loop counter is {loopCounter}");
                //Debug.Log($" the loop max is {loopMax}");
                for (int i = 2; i < loopCounter; i++)
                {
                    //Debug.Log("string manipulation start");
                    if (i!= 4)
                    {
                        finalConfidence = finalConfidence + ConArray[i];
                    }
                    else
                    {
                        finalConfidence = finalConfidence + ",";
                    }
                    //Debug.Log(finalConfidence);
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
