using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectController : MonoBehaviour
{
    private PlayerInteractionController playerInteractionController;
    private InteractableObject interactableObject;

    private void Start()
    {
        playerInteractionController = GetComponent<PlayerInteractionController>();
    }


    public void AddInteractableObject(InteractableObject io)
    {
        interactableObject = io;
    }

    public void DropInteractableObject()
    {
        interactableObject.transform.parent = null;
        interactableObject.Drop(gameObject);
        interactableObject = null;
    }

    public void DestroyInteractableObject()
    {
    }
}
