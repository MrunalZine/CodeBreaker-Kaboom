using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorPanelManager : MonoBehaviour
{
    public GameObject errorPanel; // Assign the error panel here

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && errorPanel.activeSelf)
        {
            BoxManager boxManager = FindObjectOfType<BoxManager>();
            if (boxManager != null)
            {
                boxManager.RestartScene();
            }
        }
    }
}
