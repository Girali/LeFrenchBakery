using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Machine
{
    public override bool CanInteract(PlayerInteractionController pic, PlayerObjectController poc)
    {
        if (poc.InteractableObject == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override void OnEnter(InteractableObject r, GameObject p)
    {
        base.OnEnter(r, p);
        PlayerObjectController poc = user.GetComponent<PlayerObjectController>();
        if (poc.InteractableObject != null)
        {
            poc.DestroyInteractableObject();
        }
    }
}
