using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxManager : MonoBehaviour
{
    public GameObject[] boxes; // Assign boxes here
    public Sprite correctSprite; // Assign sprite 5 here
    public string correctBoxName = "Box3"; // The name of the correct box
    public GameObject errorPanel; // Assign the error panel here

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckCombination();
        }
    }

    private void CheckCombination()
    {
        bool isCorrectCombination = false;

        foreach (GameObject box in boxes)
        {
            BoxInteraction boxInteraction = box.GetComponent<BoxInteraction>();
            if (boxInteraction != null && boxInteraction.IsTurned())
            {
                if (boxInteraction.GetCurrentSprite() == correctSprite && box.name == correctBoxName)
                {
                    isCorrectCombination = true;
                    break;
                }
            }
        }

        if (isCorrectCombination)
        {
            SceneManager.LoadScene("DesktopScreen"); // Change to your next scene name
        }
        else
        {
            if (errorPanel != null)
            {
                errorPanel.SetActive(true);
            }
        }
    }

    public void RestartScene()
    {
        // Reset all boxes to their original state
        foreach (GameObject box in boxes)
        {
            BoxInteraction boxInteraction = box.GetComponent<BoxInteraction>();
            if (boxInteraction != null)
            {
                boxInteraction.ResetBox();
            }
        }

        // Reload the current scene
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
