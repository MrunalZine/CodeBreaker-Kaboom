using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteraction : MonoBehaviour
{
    public Sprite turnedSprite; // Assign sprite 5 here
    private Sprite originalSprite; // Store the original sprite
    private SpriteRenderer spriteRenderer;
    private bool isTurned = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite; // Store the original sprite at the start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TurnBox();
        }
    }

    private void TurnBox()
    {
        if (!isTurned)
        {
            spriteRenderer.sprite = turnedSprite;
            isTurned = true;
        }
    }

    public bool IsTurned()
    {
        return isTurned;
    }

    public Sprite GetCurrentSprite()
    {
        return spriteRenderer.sprite;
    }

    public void ResetBox()
    {
        spriteRenderer.sprite = originalSprite; // Reset to the original sprite
        isTurned = false; // Mark as not turned
    }
}
