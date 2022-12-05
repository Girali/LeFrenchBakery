using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
using static UnityEngine.ParticleSystem;

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

    public override bool CanInteract(PlayerInteractionController pic, PlayerObjectController poc)
    {
        if (poc.InteractableObject == null)
        {
            return true;
        }

        return false;
    }

    public virtual void Hold(GameObject g)
    {
        holder = g;
    }

    public GameObject Release()
    {
        GameObject h = holder;
        holder = null;
        return h;
    }

    public void Drop(GameObject g)
    {
        holder = null;
        rb.AddForce(g.transform.forward * 100f);
    }
}
