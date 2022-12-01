using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeObject : Interactable
{
    public Gradient colorOverReciepe;
    private Recipe recipe;

    public void Init(Recipe r)
    {
        recipe = r;
    }
}
