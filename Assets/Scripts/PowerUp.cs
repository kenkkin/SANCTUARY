using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float speedIncreaseAmount = 2f; // The amount to increase the player's speed by
    [SerializeField] float powerUpDuration = 5f; // The duration of the power-up effect

    private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    private float originalSpeed; // Original player speed

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement = other.GetComponent<PlayerMovement>(); // Get a reference to the PlayerMovement script
            if (playerMovement != null)
            {
                // Store the original player speed
                originalSpeed = playerMovement.agent.speed;

                // Increase the player's speed
                playerMovement.IncreaseSpeed(speedIncreaseAmount);

                // Start the power-up timer
                StartCoroutine(PowerUpTimer());
                
                // Disable the power-up object
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator PowerUpTimer()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(powerUpDuration);

        // Reset the player's speed back to normal
        if (playerMovement != null)
        {
            playerMovement.ResetSpeed();
        }

        // Destroy the power-up object
        Destroy(gameObject);
    }
}
