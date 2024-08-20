using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    public Vector3[] positions; // Array to hold target positions
    public float speed = 2f;
    private int currentTargetIndex = 0;
    private Vector3 previousPosition; // To track the platform's previous position

    void Start()
    {
        // Initialize the previous position to the starting position of the platform
        previousPosition = transform.position;
    }

    void Update()
    {
        if (positions.Length == 0) return; // Return if no positions

        // Move towards the current target position
        transform.position = Vector3.MoveTowards(transform.position, positions[currentTargetIndex], speed * Time.deltaTime);

        // Check if the platform has reached the target position
        if (Vector3.Distance(transform.position, positions[currentTargetIndex]) < 0.1f)
        {
            // Move to the next position
            currentTargetIndex = (currentTargetIndex + 1) % positions.Length;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player or another movable object
        if (other.CompareTag("Player") || other.CompareTag("MovableObject"))
        {
            // Calculate the difference between the current and previous positions
            Vector3 movementDelta = transform.position - previousPosition;

            // Apply the same movement to the object standing on the platform
            other.transform.position += movementDelta;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Continue to move the object along with the platform
        if (other.CompareTag("Player") || other.CompareTag("MovableObject"))
        {
            Vector3 movementDelta = transform.position - previousPosition;
            other.transform.position += movementDelta;
        }
    }

    private void LateUpdate()
    {
        // Update the previous position after all Update calls have been made
        previousPosition = transform.position;
    }
}
