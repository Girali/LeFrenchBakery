using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : Interactable
{
    private Rigidbody rb;

    public override void Interact(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        base.Interact(leftClick, leftClickDown, rightClickDown);
    }

    public override Interactable InteractFirst(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        return base.InteractFirst(leftClick, leftClickDown, rightClickDown);
    }

    public void Drop(GameObject g)
    {
        rb.AddForce(g.transform.forward * 10f);
    }
}
