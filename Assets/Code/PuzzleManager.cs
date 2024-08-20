using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] emptyBlocks; // Array to hold references to the empty box blocks
    public Sprite fullBoxSprite; // Sprite for the full box
    public Sprite emptyBoxSprite; // Sprite for the empty box
    public string newSceneName; // Name of the new scene to load
    public GameObject errorPanel; // Reference to the error panel GameObject
    public TextMeshProUGUI proceedText; // Reference to the TextMesh Pro UI Text for "Press Enter to proceed"

    private void Start()
    {
        // Ensure all blocks start with the empty sprite
        foreach (GameObject block in emptyBlocks)
        {
            block.GetComponent<SpriteRenderer>().sprite = emptyBoxSprite;
        }

        // Ensure the error panel and proceed text are hidden at the start
        if (errorPanel != null)
        {
            errorPanel.SetActive(false);
        }

        if (proceedText != null)
        {
            proceedText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Check if Enter key is pressed
        {
            CheckPattern();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Assuming your player has the "Player" tag
        {
            // Change the sprite of the block the player collided with
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer.sprite == emptyBoxSprite)
            {
                spriteRenderer.sprite = fullBoxSprite;
            }

            // Show the "Press Enter to proceed" text
            if (proceedText != null)
            {
                proceedText.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Hide the text when the player moves away
        {
            if (proceedText != null)
            {
                proceedText.gameObject.SetActive(false);
            }
        }
    }

    private void CheckPattern()
    {
        // Get all sprites in the order they are defined
        Sprite[] sprites = new Sprite[emptyBlocks.Length];
        for (int i = 0; i < emptyBlocks.Length; i++)
        {
            sprites[i] = emptyBlocks[i].GetComponent<SpriteRenderer>().sprite;
        }

        // Check for the pattern: three consecutive empty sprites followed by one full sprite
        bool patternMatched = false;
        for (int i = 0; i <= sprites.Length - 4; i++)
        {
            if (sprites[i] == emptyBoxSprite && sprites[i + 1] == emptyBoxSprite && sprites[i + 2] == emptyBoxSprite && sprites[i + 3] == fullBoxSprite)
            {
                patternMatched = true;
                break;
            }
        }

        if (patternMatched)
        {
            Debug.Log("Pattern matched! Loading the new scene.");
            LoadNewScene();
        }
        else
        {
            Debug.Log("Pattern not matched. Showing error panel.");
            ShowErrorPanel();
        }
    }

    private void LoadNewScene()
    {
        if (!string.IsNullOrEmpty(newSceneName))
        {
            SceneManager.LoadScene(newSceneName);
        }
        else
        {
            Debug.LogError("New scene name not assigned.");
        }
    }

    private void ShowErrorPanel()
    {
        if (errorPanel != null)
        {
            errorPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Error panel not assigned.");
        }
    }
}
