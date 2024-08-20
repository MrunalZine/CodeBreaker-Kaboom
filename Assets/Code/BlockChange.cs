using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChange : MonoBehaviour
{
    public Sprite[] initialBlockSprites;  // Array to hold the initial block sprites (0, 1, 2, 3)
    public Sprite changedBlockSprite;     // The sprite to change to when touched
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")  // Make sure the player has the "Player" tag
        {
            ChangeBlockSprite();
        }
    }

    private void ChangeBlockSprite()
    {
        // If the current sprite is one of the initial block sprites, change it to the left-out block sprite
        foreach (Sprite initialSprite in initialBlockSprites)
        {
            if (spriteRenderer.sprite == initialSprite)
            {
                spriteRenderer.sprite = changedBlockSprite;
                break;
            }
        }
    }
}
