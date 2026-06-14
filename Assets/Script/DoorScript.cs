using System;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] // This exposes the variable to the Unity Inspector
    Animator myAnimator; 

    bool isOpen = false; 


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