using UnityEngine;
using System.IO;

public class TextRead : MonoBehaviour
{
    private string filePath = "D:/FOBI/Coding/FOBI_FACE/face.txt";
    private float readInterval = 0.5f;
    private float clearInterval = 2f;

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
                Debug.Log("File Contents: " + fileContents);
            }
            else
            {
                Debug.LogError("File not found at path: " + filePath);
            }

            // Wait for the specified interval
            yield return new WaitForSeconds(readInterval);

            // Clear the console output
            Debug.ClearDeveloperConsole();

            // Wait for the specified interval before reading the file again
            yield return new WaitForSeconds(clearInterval - readInterval);
        }
    }
}
