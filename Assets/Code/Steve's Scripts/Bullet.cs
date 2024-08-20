using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;           // Set this value in the Inspector
    public AudioClip collisionSound;     // Sound to play on collision

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Play the collision sound at the point of collision
        if (collisionSound != null)
        {
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);
        }

        // Check if the collided object has the DestructibleObject script
        DestructibleObject destructible = collision.gameObject.GetComponent<DestructibleObject>();
        if (destructible != null)
        {
            destructible.TakeDamage(damage);  // Apply damage to the destructible object
        }

        // Destroy the bullet instantly after it hits something
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}