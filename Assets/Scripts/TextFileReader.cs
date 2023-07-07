
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TextFileReader : MonoBehaviour
{
    public Sprite angrySprite;
    public Sprite smileSprite;

    public Canvas canvas;
    public Image imagePrefab;

    private string filePath = "D:/FOBI/Coding/FOBI_FACE/face.txt";
    private float readInterval = 0.5f;

    private Image currentImage;

    private void Start()
    {
        // Start the coroutine to read the file continuously
        StartCoroutine(ReadFileRoutine());
    }

    private System.Collections.IEnumerator ReadFileRoutine()
    {
        while (true)
        {
            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read the contents of the file
                string fileContents = File.ReadAllText(filePath);

                // Determine the emotion based on the file contents
                if (fileContents.Contains("angry"))
                {
                    InstantiateImage(imagePrefab, canvas.transform, angrySprite);
                }
                else if (fileContents.Contains("smile"))
                {
                    InstantiateImage(imagePrefab, canvas.transform, smileSprite);
                }
                else
                {
                    Debug.LogWarning("Unrecognized emotion: " + fileContents);
                }
            }
            else
            {
                Debug.LogError("File not found at path: " + filePath);
            }

            // Wait for the specified interval
            yield return new WaitForSeconds(readInterval);
        }
    }

    private void InstantiateImage(Image prefab, Transform parent, Sprite sprite)
    {
        // Destroy the current image if it exists
        if (currentImage != null)
        {
            Destroy(currentImage.gameObject);
        }

        // Create a new image object and set the sprite
        GameObject imageObj = new GameObject("Image");
        imageObj.transform.SetParent(parent);

        currentImage = imageObj.AddComponent<Image>();
        currentImage.sprite = sprite;

        // Optionally, you can set the image's rectTransform properties
        currentImage.rectTransform.anchoredPosition = Vector2.zero;
        currentImage.rectTransform.sizeDelta = new Vector2(100f, 100f);
    }
}

