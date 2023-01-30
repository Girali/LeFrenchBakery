using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class CookBook : Machine
{
    [SerializeField]
    private RecipeController recipeController;

    [SerializeField]
    private GameObject ui;

    public override bool CanInteract(PlayerInteractionController pic, PlayerObjectController poc)
    {
        if (poc.InteractableObject != null)
        {
            return false;
        }

        return true;
    }

    public override void OnEnter(InteractableObject r, GameObject p)
    {
        base.OnEnter(r,p);
        user.StartStopMove(false, this);
        ui.SetActive(true);
        AppController.Instance.Pausable = false;
    }

    public void Quit()
    {
        OnExit();
    }

    public override InteractableObject OnExit()
    {
        AppController.Instance.Pausable = true;
        ui.SetActive(false);
        user.StartStopMove(true, this);
        return base.OnExit();
    }

    public void StartRecipe(Article i)
    {
        interactableObject = recipeController.StartReciepe(i);
        user.GetComponent<PlayerObjectController>().AddInteractableObject(interactableObject);
        OnExit();
    }
}
