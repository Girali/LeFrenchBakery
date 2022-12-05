using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientStorage : Machine
{
    private FournisseurController fournisseurController;

    [SerializeField]
    private GameObject ui;

    public override void OnEnter(RecipeObject r, GameObject p)
    {
        base.OnEnter(r, p);
        user.StartStopMove(false, this);
        ui.SetActive(true);
    }

    public void Quit()
    {
        OnExit();
    }

    public override RecipeObject OnExit()
    {
        ui.SetActive(false);
        user.StartStopMove(true, this);
        return base.OnExit();
    }

    public void AddIngredient(Ingredient i)
    {
        recipeObject.UpdateStep(i);
        MagasinController.Instance.SubIngredient(i);
        OnExit();
    }
}
