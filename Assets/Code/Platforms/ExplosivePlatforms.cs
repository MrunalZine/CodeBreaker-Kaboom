using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExplosivePlatforms : MonoBehaviour
{
    public float countdownTime = 3f; // Time before explosion
    private float timer;
    private bool isPlayerOnPlatform = false;
    private bool hasExploded = false;
    public TextMeshPro timerText; // Reference to the TextMeshPro component
    public GameObject explosionEffect; // Prefab for the explosion effect

    void Start()
    {
        // Hide the timer text initially
        timerText.text = "";
    }

    void Update()
    {
        if (isPlayerOnPlatform && !hasExploded)
        {
            timer -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timer).ToString(); // Update the timer text

            if (timer <= 0)
            {
                Explode();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasExploded)
        {
            isPlayerOnPlatform = true;
            timer = countdownTime; // Start the countdown
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasExploded)
        {
            isPlayerOnPlatform = false;
            timerText.text = ""; // Hide the timer when the player leaves the platform
        }
    }

    private void Explode()
    {
        hasExploded = true;
        timerText.text = ""; // Clear the timer text

        // Trigger the explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        // Destroy the platform
        Destroy(gameObject);
    }
}
