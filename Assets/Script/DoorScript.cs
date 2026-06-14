using System;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] // This exposes the variable to the Unity Inspector
    Animator myAnimator; 

    bool isOpen = false; 

    // We no longer need the Start() method trying to find the component automatically!

    public void Interact() 
    {
        if(isOpen) 
        {
            myAnimator.SetTrigger("CloseDoor"); 
        }
        else 
        {
            myAnimator.SetTrigger("OpenDoor"); 
        }

        isOpen = !isOpen; 
    }
}