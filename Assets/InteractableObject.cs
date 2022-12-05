using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class InteractableObject : Interactable
{
    [HideInInspector]
    public Rigidbody rb;
    protected GameObject holder;

    public virtual void Init(Object o)
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Interact(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        base.Interact(leftClick, leftClickDown, rightClickDown);
    }

    public override Interactable InteractFirst(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        return base.InteractFirst(leftClick, leftClickDown, rightClickDown);
    }

    public void Hold(GameObject g)
    {
        holder = g;
        Debug.LogError(holder);
    }

    public GameObject Release()
    {
        GameObject h = holder;
        holder = null;
        Debug.LogError(holder);
        return h;
    }

    public void Drop(GameObject g)
    {
        holder = null;
        Debug.LogError(holder);
        rb.AddForce(g.transform.forward * 100f);
    }
}
