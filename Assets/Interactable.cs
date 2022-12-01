using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual Interactable InteractFirst(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        return this;
    }

    public virtual void Interact(bool leftClick, bool leftClickDown, bool rightClickDown)
    {

    }
}
