using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    public Transform firePoint;                 // The point from where the bullet will be fired
    public GameObject semiAutoBulletPrefab;     // Bullet prefab for the semi-automatic weapon
    public GameObject fullAutoBulletPrefab;     // Bullet prefab for the fully automatic weapon
    public GameObject shotgunBulletPrefab;      // Bullet prefab for the shotgun
    public float bulletForce = 20f;             // The speed at which the bullet will travel
    public float shotgunBulletForce = 10f;      // The speed at which the shotgun bullets will travel

    public float autoFireRate = 0.1f;           // Fire rate for fully automatic weapon
    public float semiFireRate = 0.5f;           // Cooldown time for semi-automatic weapon
    public float shotgunFireRate = 1f;          // Cooldown time for shotgun weapon
    public float shotgunBulletLifetime = 0.5f;  // Lifetime of shotgun bullets (in seconds)
    private float nextTimeToFire = 0f;

    public enum WeaponType { SemiAutomatic, FullyAutomatic, Shotgun }
    public WeaponType currentWeapon = WeaponType.SemiAutomatic;

    public Image weaponIndicator;               // UI Image to show the weapon type
    public Sprite semiAutomaticSprite;          // Sprite for semi-automatic weapon
    public Sprite fullyAutomaticSprite;         // Sprite for fully automatic weapon
    public Sprite shotgunSprite;                // Sprite for shotgun weapon

    public SpriteRenderer gunSpriteRenderer;    // Reference to the SpriteRenderer component of the gun
    public Sprite semiAutoGunSprite;            // Sprite for the semi-automatic gun
    public Sprite fullAutoGunSprite;            // Sprite for the fully automatic gun
    public Sprite shotgunGunSprite;             // Sprite for the shotgun gun

    public SpriteRenderer playerSpriteRenderer; // Reference to the player's SpriteRenderer

    public AudioSource audioSource;             // AudioSource component to play the sound
    public AudioClip semiAutoSound;             // Sound for semi-automatic weapon
    public AudioClip fullAutoSound;             // Sound for fully automatic weapon
    public AudioClip shotgunSound;              // Sound for shotgun weapon

    void Start()
    {
        UpdateWeaponIndicator();
        UpdateGunSprite();  // Update the gun sprite on start
    }

    void Update()
    {
        HandleShooting();
        HandleWeaponSwitching();
    }

    void HandleShooting()
    {
        if (currentWeapon == WeaponType.FullyAutomatic)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + autoFireRate;
                Shoot(fullAutoBulletPrefab, bulletForce);
                PlayFireSound(fullAutoSound);
            }
        }
        else if (currentWeapon == WeaponType.SemiAutomatic)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + semiFireRate;
                Shoot(semiAutoBulletPrefab, bulletForce);
                PlayFireSound(semiAutoSound);
            }
        }
        else if (currentWeapon == WeaponType.Shotgun)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + shotgunFireRate;
                ShootShotgun();
                PlayFireSound(shotgunSound);
            }
        }
    }

    void HandleWeaponSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = WeaponType.SemiAutomatic;
            UpdateWeaponIndicator();
            UpdateGunSprite();
            Debug.Log("Switched to Semi-Automatic");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = WeaponType.FullyAutomatic;
            UpdateWeaponIndicator();
            UpdateGunSprite();
            Debug.Log("Switched to Fully Automatic");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = WeaponType.Shotgun;
            UpdateWeaponIndicator();
            UpdateGunSprite();
            Debug.Log("Switched to Shotgun");
        }
    }

    void Shoot(GameObject bulletPrefab, float force)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 shootingDirection = playerSpriteRenderer.flipX ? -firePoint.right : firePoint.right;
            rb.velocity = shootingDirection * force;
        }
        else
        {
            Debug.LogError("Bullet prefab is missing Rigidbody2D component!");
        }
    }

    void ShootShotgun()
    {
        float[] spreadAngles = { -15f, 0f, 15f };

        foreach (float angle in spreadAngles)
        {
            Quaternion bulletRotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(shotgunBulletPrefab, firePoint.position, bulletRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 shootingDirection = playerSpriteRenderer.flipX ? -bullet.transform.right : bullet.transform.right;
                rb.velocity = shootingDirection * shotgunBulletForce;
            }
            else
            {
                Debug.LogError("Shotgun bullet prefab is missing Rigidbody2D component!");
            }

            Destroy(bullet, shotgunBulletLifetime);
        }
    }

    void PlayFireSound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void UpdateWeaponIndicator()
    {
        if (weaponIndicator != null)
        {
            if (currentWeapon == WeaponType.SemiAutomatic)
            {
                weaponIndicator.sprite = semiAutomaticSprite;
            }
            else if (currentWeapon == WeaponType.FullyAutomatic)
            {
                weaponIndicator.sprite = fullyAutomaticSprite;
            }
            else if (currentWeapon == WeaponType.Shotgun)
            {
                weaponIndicator.sprite = shotgunSprite;
            }
        }
    }

    void UpdateGunSprite()
    {
        if (gunSpriteRenderer != null)
        {
            if (currentWeapon == WeaponType.SemiAutomatic)
            {
                gunSpriteRenderer.sprite = semiAutoGunSprite;
            }
            else if (currentWeapon == WeaponType.FullyAutomatic)
            {
                gunSpriteRenderer.sprite = fullAutoGunSprite;
            }
            else if (currentWeapon == WeaponType.Shotgun)
            {
                gunSpriteRenderer.sprite = shotgunGunSprite;
            }
        }
    }
}