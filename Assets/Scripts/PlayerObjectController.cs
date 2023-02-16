using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerObjectController : MonoBehaviour
{
    private PlayerInteractionController playerInteractionController;
    private InteractableObject interactableObject;
    private DUI_ReciepeFollower recipeFollower;
    
    [SerializeField]
    private Transform position;

    public InteractableObject InteractableObject { get => interactableObject; }

    private void Start()
    {
        recipeFollower = GetComponent<DUI_ReciepeFollower>();
        playerInteractionController = GetComponent<PlayerInteractionController>();
    }

    public void Motor(bool interactRightDown)
    {
        if (!playerInteractionController.Interacting)
        {
            if (interactRightDown)
            {
                DropInteractableObject();
            }
        }
    }

    public void AddInteractableObject(InteractableObject io)
    {
        interactableObject = io;
        interactableObject.Hold(gameObject);
        Destroy(interactableObject.rb);
        interactableObject.transform.parent = position;
        interactableObject.transform.localPosition = Vector3.zero;
        recipeFollower.Show(true);
    }

    public void PauseInteractableObject()
    {
        if (interactableObject != null)
        {
            interactableObject.transform.parent = null;
            interactableObject.transform.position = transform.position + new Vector3(0, 100, 0);
            interactableObject.Release();
            interactableObject = null;
            recipeFollower.Show(false);
        }
    }

    public void UnPauseInteractableObject(InteractableObject io)
    {
        interactableObject = io;
        interactableObject.Hold(gameObject);
        interactableObject.transform.parent = position;
        interactableObject.transform.localPosition = Vector3.zero;
        recipeFollower.Show(true);
    }

    public void DropInteractableObject()
    {
        if (interactableObject != null)
        {
            interactableObject.rb = interactableObject.AddComponent<Rigidbody>();
            interactableObject.rb.freezeRotation = true;
            interactableObject.transform.parent = null;
            interactableObject.Drop(gameObject);
            interactableObject.Release();
            interactableObject = null;
            recipeFollower.Show(false);
        }
    }

    public GameObject DestroyInteractableObject()
    {
        if (interactableObject != null)
        {
            interactableObject.transform.parent = null;
            recipeFollower.Show(false);
            Destroy(interactableObject.gameObject);
            GameObject h = interactableObject.Release();
            interactableObject = null;
            return h;
        }
        return null;
    }
}
