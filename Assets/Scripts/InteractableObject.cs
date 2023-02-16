using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : Interactable
{
    [HideInInspector]
    public Rigidbody rb;
    protected GameObject holder;
    protected DUI_ReciepeFollower recipeFollower;

    public virtual void Init(Object o)
    {
        rb = GetComponent<Rigidbody>();
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
        recipeFollower = holder.GetComponent<DUI_ReciepeFollower>();
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
