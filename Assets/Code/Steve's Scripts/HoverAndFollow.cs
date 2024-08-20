using UnityEngine;

public class HoverAndFollow : MonoBehaviour
{
    public Transform player;         // Reference to the player's transform
    public float hoverHeight = 0.5f; // How high the object hovers
    public float hoverSpeed = 2f;    // Speed of hovering
    public float followSpeed = 2f;   // Speed at which the object follows the player

    private Vector3 offset;          // Offset from the player's position
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        offset = transform.position - player.position; // Initial offset from the player
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Levitating effect
        float newY = player.position.y + offset.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Follow the player
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, newY, player.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Flip the sprite based on player's flip
        if (playerSpriteRenderer != null)
        {
            spriteRenderer.flipX = playerSpriteRenderer.flipX;
        }
    }
}
