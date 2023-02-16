using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Fournisseur : Machine
{
    [SerializeField]
    private FournisseurController fournisseur;

    [SerializeField]
    private DUI_Fournisseur ui;

    private int currentRecipe = 0;
    private bool firstFrame;

    public override bool CanInteract(PlayerInteractionController pic, PlayerObjectController poc)
    {
        if (inUse)
                return false; 
        
        return true;
    }

    public override void OnEnter(InteractableObject r, GameObject p)
    {
        base.OnEnter(r, p);
        user.StartStopMove(false, this);
        ui.gameObject.SetActive(true);
        ui.UpdateView(fournisseur.stockItems[currentRecipe].ingredient);
        inUse = true;
    }

    public override Interactable InteractFirst(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        firstFrame = true;
        return base.InteractFirst(leftClick, leftClickDown, rightClickDown);
    }

    public override void Interact(bool leftClick, bool leftClickDown, bool rightClickDown, bool escape, bool left, bool right)
    {
        base.Interact(leftClick, leftClickDown, rightClickDown, escape, left, right);
        if (firstFrame)
        {
            firstFrame = false;
        }
        else
        {
            if (rightClickDown)
                Quit();

            if (left)
            {
                ui.Prev();
                currentRecipe--;
                currentRecipe = Mathf.Clamp(currentRecipe, 0, fournisseur.stockItems.Count - 1);
                ui.UpdateView(fournisseur.stockItems[currentRecipe].ingredient);
            }

            if (right)
            {
                ui.Next();
                currentRecipe++;
                currentRecipe = Mathf.Clamp(currentRecipe, 0, fournisseur.stockItems.Count - 1);
                ui.UpdateView(fournisseur.stockItems[currentRecipe].ingredient);
            }

            if (leftClickDown)
            {
                ui.Buy();
            }
        }
    }

    public override InteractableObject OnExit()
    {
        inUse = false;
        ui.gameObject.SetActive(false);
        user.StartStopMove(true, this);
        return base.OnExit();
    }

    public void Quit()
    {
        SoundController.Instance.CloseUI();
        OnExit();
    }
}
