using UnityEngine;
using SFB;
using System.IO;
using System.Collections;

public class ImageLoader : MonoBehaviour
{
    public Renderer[] imageDisplayRenderer;
    public RestAPI server;

    public void OpenImageExplorer()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Open Image", "", new[] { new ExtensionFilter("Image Files", "png", "jpg", "jpeg") }, false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            string filePath = paths[0];
            Debug.Log("Selected file: " + filePath);
            StartCoroutine(LoadImage(filePath));
        }

    }

    private IEnumerator LoadImage(string filePath)
    {
        // Check if renderer is assigned
        if (imageDisplayRenderer == null)
        {
            Debug.LogError("imageDisplayRenderer is not assigned.");
            yield break;
        }

        // Load file data
        byte[] fileData = File.ReadAllBytes(filePath);
        if (fileData == null || fileData.Length == 0)
        {
            Debug.LogError("Failed to load file data.");
            yield break;
        }

        // Create texture and load data
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        // Check if texture is valid
        Debug.Log("Image Loaded: " + texture.width + "x" + texture.height);
        if (texture == null)
        {
            Debug.LogError("Texture creation failed.");
            yield break;
        }

        // Apply texture to the renderer
        foreach (Renderer renderer in imageDisplayRenderer)
        {
            renderer.material.mainTexture = texture;
            Debug.Log("Image is uploaded");
            if (filePath != null)
            {
                server.filePathInput = filePath;
            }
            else
            {
                Debug.LogWarning("File path is empty");
            }
        }
        yield return null;
    }

    public void uploadToServer(string path)
    {
        if (server != null)
        {
            server.UploadImage(path);
        }
    }
}
