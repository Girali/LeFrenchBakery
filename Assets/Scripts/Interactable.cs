using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool interactable = true;

    public virtual Interactable InteractFirst(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        return this;
    }

    public virtual void Interact(bool leftClick, bool leftClickDown, bool rightClickDown, bool escape, bool left, bool right)
    {

    }

    public virtual bool CanInteract(PlayerInteractionController pic, PlayerObjectController poc)
    {
        return true;
    }
}
