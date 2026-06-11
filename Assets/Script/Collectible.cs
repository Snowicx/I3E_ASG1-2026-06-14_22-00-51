using UnityEngine;

using UnityEngine.InputSystem; 

public class Collectible : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1; // How much this item is worth
    private bool isPlayerNearby = false;       // Tracks if the player is close enough

    void Update()
    {
        // 2. Check if the player is nearby AND presses the 'E' key
        if (isPlayerNearby && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Collect();
        }
    }

    // Detect when the player walks into the collectible's trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Press 'E' to collect the ingredient!");
        }
    }

    // Detect when the player walks away from the collectible
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void Collect()
    {
        Debug.Log("Item Collected!");
        
        // Talk to your updated ScoreManager
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(1); // Adds 1 to the score
        }

        Destroy(gameObject);
    }
}
