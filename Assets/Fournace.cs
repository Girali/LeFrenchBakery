using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fournace : Machine
{
    private RecipeObject recipeObject;

    public override void Interact(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        base.Interact(leftClick, leftClickDown, rightClickDown);
    }

    public override Interactable InteractFirst(bool leftClick, bool leftClickDown, bool rightClickDown)
    {
        base.InteractFirst(leftClick, leftClickDown, rightClickDown);
        DGUI_Controller.Insatance.StartTimer(transform, 10, 10);

        return this;
    }
}
