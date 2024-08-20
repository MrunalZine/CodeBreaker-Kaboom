using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteManager : MonoBehaviour
{
    public GameObject errorPanel; // Assign this in the Inspector
    public string newSceneName; // Assign this in the Inspector
    public Sprite sprite4; // Assign Sprite 4 in the Inspector

    public GameObject sprite0; // Assign GameObject 0 in the Inspector
    public GameObject sprite1; // Assign GameObject 1 in the Inspector
    public GameObject sprite2; // Assign GameObject 2 in the Inspector
    public GameObject sprite3; // Assign GameObject 3 in the Inspector

    private bool sprite0Touched = false;
    private bool sprite1Touched = false;
    private bool sprite2Touched = false;
    private bool sprite3Touched = false;

    void Start()
    {
        // Ensure the error panel is hidden at the start
        if (errorPanel != null)
        {
            errorPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Check for Enter key press
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Check if only sprite 3 is turned into Sprite 4
            if (sprite3Touched && !sprite0Touched && !sprite1Touched && !sprite2Touched)
            {
                // Load the new scene
                LoadNewScene();
            }
            else
            {
                // Show the error panel
                ShowErrorPanel();
            }
        }

        // Check for R key press to restart the scene
        if (Input.GetKeyDown(KeyCode.R) && errorPanel.activeSelf)
        {
            RestartScene();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check which GameObject was touched and update the state
        if (collision.gameObject == sprite0)
        {
            sprite0.GetComponent<SpriteRenderer>().sprite = sprite4;
            sprite0Touched = true;
        }
        else if (collision.gameObject == sprite1)
        {
            sprite1.GetComponent<SpriteRenderer>().sprite = sprite4;
            sprite1Touched = true;
        }
        else if (collision.gameObject == sprite2)
        {
            sprite2.GetComponent<SpriteRenderer>().sprite = sprite4;
            sprite2Touched = true;
        }
        else if (collision.gameObject == sprite3)
        {
            sprite3.GetComponent<SpriteRenderer>().sprite = sprite4;
            sprite3Touched = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check which GameObject was exited and reset the state
        if (collision.gameObject == sprite0)
        {
            sprite0Touched = false;
        }
        else if (collision.gameObject == sprite1)
        {
            sprite1Touched = false;
        }
        else if (collision.gameObject == sprite2)
        {
            sprite2Touched = false;
        }
        else if (collision.gameObject == sprite3)
        {
            sprite3Touched = false;
        }
    }

    private void LoadNewScene()
    {
        Debug.Log("Attempting to load new scene: " + newSceneName);
        // Load the scene assigned in the Inspector
        SceneManager.LoadScene(newSceneName);
    }

    private void ShowErrorPanel()
    {
        if (errorPanel != null)
        {
            errorPanel.SetActive(true);
        }
    }

    private void RestartScene()
    {
        if (errorPanel != null)
        {
            errorPanel.SetActive(false);
        }
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
