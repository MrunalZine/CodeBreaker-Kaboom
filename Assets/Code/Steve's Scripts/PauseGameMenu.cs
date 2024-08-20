using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameMenu : MonoBehaviour
{
    public string SwitchSceneTo; // Name of your pause menu scene
    private bool isPaused = false;

    void Update()
    {
        // Check for the Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        // Freeze game time
        Time.timeScale = 0f;

        // Load the pause menu scene additively
        SceneManager.LoadScene(SwitchSceneTo, LoadSceneMode.Additive);

        isPaused = true;
    }

    void ResumeGame()
    {
        // Unload the pause menu scene
        SceneManager.UnloadSceneAsync(SwitchSceneTo);

        // Resume game time
        Time.timeScale = 1f;

        isPaused = false;
    }
}