using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookBook : Machine
{
    [SerializeField]
    private RecipeController recipeController;

    public override void OnEnter(RecipeObject r, GameObject p)
    {
        base.OnEnter(r,p);
        user.StartStopMove(false, this);
        StartRecipe(0);
    }

    public override RecipeObject OnExit()
    {
        user.StartStopMove(true, this);
        return base.OnExit();
    }

    public void StartRecipe(int i)
    {
        recipeObject = recipeController.StartReciepe(i);
        OnExit();
    }
}
