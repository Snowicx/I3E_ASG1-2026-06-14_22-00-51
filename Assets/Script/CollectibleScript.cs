using System; // Import standard .NET system types (not strictly needed here but common in C# files)
using UnityEngine; // Import Unity-specific classes like MonoBehaviour and AudioClip

public class CollectibleScript : MonoBehaviour
{
    public int collectibleScore = 1; // Store the score value of this collectible, editable from the Unity Inspector

    [SerializeField]
    float rotationSpeed = 100f; // How fast the cheese spins in degrees per second, adjustable from the Inspector

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); // Spin the cheese around its Y-axis every frame at a smooth frame-rate-independent speed
    }

    void OnTriggerEnter(Collider other) // Fires when the player enters this cheese's collider
    {
        if(other.gameObject.tag == "Player") // Only react if the object entering is tagged as the player
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>(); // Try to find PlayerScript directly on the colliding object

            if(player == null) // If not found directly try searching upward through the parent objects
            {
                player = other.gameObject.GetComponentInParent<PlayerScript>(); // Search up the hierarchy in case PlayerScript is on a parent of the capsule
            }

            if(player != null) // Only proceed if a PlayerScript was found
            {
                player.SetNearCollectible(this); // Tell PlayerScript the player is now near this cheese so E can collect it

                print("Press [E] to collect the cheese!"); // Log the prompt to the Unity Console
            }
        }
    }

    void OnTriggerExit(Collider other) // Fires when the player leaves this cheese's collider
    {
        if(other.gameObject.tag == "Player") // Only react if the object leaving is tagged as the player
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>(); // Try to find PlayerScript directly on the colliding object

            if(player == null) // If not found directly try searching upward through the parent objects
            {
                player = other.gameObject.GetComponentInParent<PlayerScript>(); // Search up the hierarchy in case PlayerScript is on a parent of the capsule
            }

            if(player != null) // Only clear if a PlayerScript was found
            {
                player.ClearNearCollectible(this); // Tell PlayerScript the player has moved away from this cheese
            }
        }
    }

    public void Collect() // Called by PlayerScript when the player presses E while near this cheese
    {
        Destroy(gameObject); // Remove this cheese from the scene
    }
}