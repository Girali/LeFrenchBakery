using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientStorage : Machine
{
    private FournisseurController fournisseurController;

    public override void OnEnter(RecipeObject r, GameObject p)
    {
        base.OnEnter(r, p);
    }

    public override RecipeObject OnExit()
    {
        return base.OnExit();
    }

    public void AddIngredient(Ingredient i)
    {

    }
}
