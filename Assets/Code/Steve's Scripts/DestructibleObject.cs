using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public float health = 50f;               // Total health of the object
    public Color hitColor = Color.white;     // Color to flash when hit
    public float flashDuration = 0.1f;       // Duration of the flash
    private SpriteRenderer spriteRenderer;   // Reference to the SpriteRenderer component
    private Color originalColor;             // The original color of the sprite

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color; // Store the original color
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (spriteRenderer != null)
        {
            StartCoroutine(FlashWhite());
        }

        if (health <= 0f)
        {
            Die();
        }
    }

    private System.Collections.IEnumerator FlashWhite()
    {
        spriteRenderer.color = hitColor;  // Change color to white
        yield return new WaitForSeconds(flashDuration);  // Wait for the flash duration
        spriteRenderer.color = originalColor;  // Revert to the original color
    }

    void Die()
    {
        Destroy(gameObject);  // Destroy the object
    }
}