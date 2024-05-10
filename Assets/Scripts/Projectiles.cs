using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        // Instantiate a projectile at the player's position and rotation
        GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, transform.rotation);

        // Get the Rigidbody component of the projectile
        Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();

        // If the projectile has a Rigidbody component, apply velocity in the direction the player is facing
        if (projectileRb != null)
        {
            // Calculate the direction the player is facing
            Vector3 direction = transform.forward;

            // Set the velocity of the projectile
            projectileRb.velocity = direction * projectileSpeed;
        }
    }
}