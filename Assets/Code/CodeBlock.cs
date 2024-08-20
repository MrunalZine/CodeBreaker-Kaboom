using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodeBlock : MonoBehaviour
{
    public string mainSceneName; // Set this in the Inspector
    public string codeBlockID; // Unique ID for this code block

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Mark this code block as destroyed
            PlayerPrefs.SetInt(codeBlockID, 1);
            PlayerPrefs.Save();

            // Destroy the code block and load the main scene
            Destroy(gameObject);
            SceneManager.LoadScene(mainSceneName);
        }
    }
}
