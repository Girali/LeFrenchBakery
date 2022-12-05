using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class CookBook : Machine
{
    [SerializeField]
    private RecipeController recipeController;

    [SerializeField]
    private GameObject ui;

    public override void OnEnter(RecipeObject r, GameObject p)
    {
        base.OnEnter(r,p);
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

    public void StartRecipe(Article i)
    {
        recipeObject = recipeController.StartReciepe(i);
        user.GetComponent<PlayerObjectController>().AddInteractableObject(recipeObject);
        OnExit();
    }
}
