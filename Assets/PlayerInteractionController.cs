using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField]
    private float minDistance;
    private Interactable currentInteractable;
    private bool interacting = false;
    public void Interact(bool interact, bool interactDown, GameObject hit)
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
                    if (interact)
                    {
                        currentInteractable.InteractFirst(interact, interactDown);
                        interacting = true;
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

        if(interacting && currentInteractable != null)
        {
            currentInteractable.Interact(interact, interactDown);
        }
        
    }
}
