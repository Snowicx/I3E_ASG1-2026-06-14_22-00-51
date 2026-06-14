using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; 

public class PlayerScript : MonoBehaviour
{
    CollectibleScript currentCollectible; 
    DoorScript currentDoor; 

    int heldCheese = 0; // Tracks cheese the player has picked up but not yet deposited
    int depositedScore = 0; // Tracks cheese actually deposited into the zone

    [SerializeField]
    int targetScore = 18; 

    void Start()
    {
        print("Rat: \"Pss... gather me all the cheese and I will let you in! Becareful of the water\""); 
    }

    void Update() 
    {
        if(Keyboard.current.eKey.wasPressedThisFrame) 
        {
            Interact(); 
        }
    }

    void Interact() 
    {
        // 1. Player is near a cheese and picks it up
        if(currentCollectible != null) 
        {
            heldCheese += currentCollectible.collectibleScore; // Add to inventory

            print("Cheese collected! You are carrying " + heldCheese + " cheese."); 

            currentCollectible.Collect(); 
            currentCollectible = null; 

            return; // Exit early so one E press doesn't pick up AND deposit
        }

        // 2. Player is in the deposit zone
        if(currentDoor != null) 
        {
            // If the player has cheese to deposit
            if(heldCheese > 0)
            {
                depositedScore += heldCheese;
                print("Gave " + heldCheese + " cheese! Total given: " + depositedScore + " / " + targetScore);
                heldCheese = 0; // Empty the player's inventory after depositing
            }

            // Check if the door can open
            if(depositedScore >= targetScore) 
            {
                print("Rat: \"Okay fine. Here you go...\""); 
                currentDoor.Interact(); 
                currentDoor = null; // Clear the door so it doesn't trigger again
            }
            else 
            {
                print("Rat: \"Not enough! GET ME " + (targetScore - depositedScore) + " MORE!\""); 
            }
        }
        else 
        {
            print("Nothing to interact with."); 
        }
    }

    public void SetNearCollectible(CollectibleScript collectible) 
    {
        currentCollectible = collectible; 
    }

    public void ClearNearCollectible(CollectibleScript collectible) 
    {
        if(currentCollectible == collectible) 
        {
            currentCollectible = null; 
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Hazard")
        {
            print("You fell into the water! ");
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        // Check if the player walked into the transparent deposit box
        if(other.gameObject.tag == "DepositZone") 
        {
            currentDoor = other.GetComponentInParent<DoorScript>(); 
            print("Press [ E ] to give cheese"); 
        }
    }

    void OnTriggerExit(Collider other) 
    {
        // Check if the player walked out of the transparent deposit box
        if(other.gameObject.tag == "DepositZone") 
        {
            currentDoor = null; 
            print("Left the deposit zone."); // Tells the console the prompt is gone
        }
    }
}