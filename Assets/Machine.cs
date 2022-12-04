using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : Interactable
{
    [SerializeField]
    private string machineName;
    [SerializeField]
    private Type type;
    protected PlayerController user = null;
    protected RecipeObject recipeObject;

    public Type GetMachineType { get => type; }

    public virtual void OnEnter(RecipeObject r, GameObject p)
    {
        recipeObject = r;
        user = p.GetComponent<PlayerController>();
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
        CookBook,
        Trash,
        Ingredients,
        Four,
        Mixer
    }
}
