using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField]
    private float minDistance;
    private Interactable currentInteractable;
    private bool interacting = false;
    private Machine machineInUse;
    private PlayerObjectController playerObjectController;

    public bool Interacting { get => interacting; }

    private void Start()
    {
        playerObjectController = GetComponent<PlayerObjectController>();
    }

    public void StartStopInteract(bool start, Machine machine)
    {
        if (start)
        {
            interacting = false;
            machineInUse = machine;
        }
        else
        {
            interacting = true;
            machineInUse = null;
        }
    }

    public void Interact(bool interactLeft, bool interactDownLeft, bool interactDownRight, GameObject hit)
    {

        if (interacting == false)
        {
            if (hit != null)
            {
                Interactable i = hit.GetComponent<Interactable>();

                if (i != null && i.interactable && i.CanInteract(this, playerObjectController))// hit interactable
                {
                    Vector3 v = hit.transform.position - transform.position;
                    v.y = 0;
                    float dist = v.magnitude;

                    if (currentInteractable == null && dist < minDistance)// current is null and in range
                    {
                        currentInteractable = i;
                        if(currentInteractable.GetComponent<InteractableObject>() != null)
                            DGUI_Controller.Insatance.ShowInidicator(currentInteractable.transform, true, Vector3.down);
                        else
                            DGUI_Controller.Insatance.ShowInidicator(currentInteractable.transform, true);
                    }
                    else if (currentInteractable != i && dist < minDistance)// current is not current and in range
                    {
                        currentInteractable = i;
                        if (currentInteractable.GetComponent<InteractableObject>() != null)
                            DGUI_Controller.Insatance.ShowInidicator(currentInteractable.transform, true, Vector3.down);
                        else
                            DGUI_Controller.Insatance.ShowInidicator(currentInteractable.transform, true);
                    }
                    else if (currentInteractable != i && dist > minDistance)// current is ok and out range
                    {
                        currentInteractable = null;
                        DGUI_Controller.Insatance.ShowInidicator(null, false);
                    }
                    else // current is ok and in range
                    {
                        if (interactDownLeft)
                        {
                            Interactable interactable = currentInteractable.InteractFirst(interactLeft, interactDownLeft, interactDownRight);

                            machineInUse = interactable.GetComponent<Machine>();

                            if (machineInUse == null)
                            {
                                InteractableObject io = interactable.GetComponent<InteractableObject>();
                                if (io == null)
                                {
                                    Client c = interactable.GetComponent<Client>();
                                    if (c != null)
                                    {
                                        c.SellItem(playerObjectController);
                                    }
                                }
                                else
                                {
                                    playerObjectController.AddInteractableObject(io);
                                }
                            }
                            else
                            {
                                ArticleObject articleObject;

                                if (playerObjectController.InteractableObject != null && playerObjectController.InteractableObject.TryGetComponent<ArticleObject>(out articleObject))
                                {   
                                    if (machineInUse.GetMachineType == Machine.Type.Storage)
                                    {
                                        machineInUse.OnEnter(playerObjectController.InteractableObject, gameObject);
                                    }
                                }
                                else
                                {
                                    machineInUse.OnEnter(playerObjectController.InteractableObject, gameObject);
                                }
                            }

                            SoundController.Instance.Interact();
                            DGUI_Controller.Insatance.ShowInidicator(null, false);
                        }
                    }
                }
                else// no hit
                {
                    if (currentInteractable != null)//current is ok
                    {
                        currentInteractable = null;
                        DGUI_Controller.Insatance.ShowInidicator(null, false);
                    }
                    else// current is null
                    {

                    }
                }
            }
        }

        //if (interactDownLeft && currentInteractable != null)
        //{
        //    currentInteractable.InteractFirst(interactLeft, interactDownLeft, interactDownRight);
        //}

        if (interacting && currentInteractable != null)
        {
            currentInteractable.Interact(interactLeft, interactDownLeft, interactDownRight);
        }

    }
}
