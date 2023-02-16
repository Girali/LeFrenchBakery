using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientStorage : Machine
{
    [SerializeField]
    private DUI_StoredIngredents ui;
    private bool firstFrame;
    private int currentRecipe = 0;

    public override bool CanInteract(PlayerInteractionController pic, PlayerObjectController poc)
    {
        if (inUse)
            return false;

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
        ui.gameObject.SetActive(true);
        ui.UpdateView(MagasinController.Instance.ingredientStocks[currentRecipe].ingredient);
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
                currentRecipe = Mathf.Clamp(currentRecipe, 0, MagasinController.Instance.ingredientStocks.Length - 1);
                ui.UpdateView(MagasinController.Instance.ingredientStocks[currentRecipe].ingredient);
            }

            if (right)
            {
                ui.Next();
                currentRecipe++;
                currentRecipe = Mathf.Clamp(currentRecipe, 0, MagasinController.Instance.ingredientStocks.Length - 1);
                ui.UpdateView(MagasinController.Instance.ingredientStocks[currentRecipe].ingredient);
            }

            if (leftClickDown)
            {
                ui.Buy();
            }
        }
    }

    public void Quit()
    {
        SoundController.Instance.CloseUI();
        OnExit();
    }

    public override InteractableObject OnExit()
    {
        ui.gameObject.SetActive(false);
        user.StartStopMove(true, this);
        inUse = false;
        return base.OnExit();
    }

    public void AddIngredient(Ingredient i)
    {
        SoundController.Instance.PutIngredient();
        ((RecipeObject)interactableObject).UpdateStep(i);
        MagasinController.Instance.SubIngredient(i);
    }
}
