using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Fournisseur : Machine
{
    [SerializeField]
    private GameObject ui;

    public override bool CanInteract(PlayerInteractionController pic, PlayerObjectController poc)
    {
        return true;
    }

    public override void OnEnter(InteractableObject r, GameObject p)
    {
        base.OnEnter(r, p);
        user.StartStopMove(false, this);
        ui.SetActive(true);
    }

    public override InteractableObject OnExit()
    {
        ui.SetActive(false);
        user.StartStopMove(true, this);
        return base.OnExit();
    }

    public void Quit()
    {
        OnExit();
    }
}
