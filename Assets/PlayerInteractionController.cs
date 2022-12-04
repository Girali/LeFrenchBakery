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
    private RecipeObject recipeObject;
    private PlayerObjectController playerObjectController;

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

                if (i != null)// hit interactable
                {
                    Vector3 v = hit.transform.position - transform.position;
                    v.y = 0;
                    float dist = v.magnitude;

                    if (currentInteractable == null && dist < minDistance)// current is null and in range
                    {
                        currentInteractable = i;
                        DGUI_Controller.Insatance.ShowInidicator(currentInteractable.transform, true);
                    }
                    else if (currentInteractable != i && dist < minDistance)// current is not current and in range
                    {
                        currentInteractable = i;
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
                            machineInUse = currentInteractable.InteractFirst(interactLeft, interactDownLeft, interactDownRight).GetComponent<Machine>();
                            DGUI_Controller.Insatance.ShowInidicator(null, false);
                            machineInUse.OnEnter(recipeObject, gameObject);
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

        if (interactDownLeft && currentInteractable != null)
        {
            currentInteractable.InteractFirst(interactLeft, interactDownLeft, interactDownRight);
        }

        if (interacting && currentInteractable != null)
        {
            currentInteractable.Interact(interactLeft, interactDownLeft, interactDownRight);
        }
        
    }
}
