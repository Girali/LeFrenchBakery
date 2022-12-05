using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientStorage : Machine
{
    private FournisseurController fournisseurController;

    [SerializeField]
    private GameObject ui;

    public override bool CanInteract(PlayerInteractionController pic, PlayerObjectController poc)
    {
        if (poc.InteractableObject == null)
        {
            return false;
        }
        else
        {
            RecipeObject recipeObject = poc.InteractableObject.GetComponent<RecipeObject>();
            if (recipeObject != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public override void OnEnter(InteractableObject r, GameObject p)
    {
        base.OnEnter(r, p);
        user.StartStopMove(false, this);
        ui.SetActive(true);
    }

    public void Quit()
    {
        OnExit();
    }

    public override InteractableObject OnExit()
    {
        ui.SetActive(false);
        user.StartStopMove(true, this);
        return base.OnExit();
    }

    public void AddIngredient(Ingredient i)
    {
        ((RecipeObject)interactableObject).UpdateStep(i);
        MagasinController.Instance.SubIngredient(i);
        OnExit();
    }
}
