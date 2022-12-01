using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : Interactable
{
    public string machineName;
    public Type type;
    protected PlayerController user = null;
    protected RecipeObject recipeObject;

    public virtual void OnEnter(RecipeObject r, PlayerController p)
    {
        recipeObject = r;
        user = p;
    }

    public virtual RecipeObject OnExit()
    {
        user = null;
        RecipeObject r = recipeObject;
        recipeObject = null;
        return r;
    }

    public enum Type
    {
        None,
        Four,
        Mixer
    }
}
