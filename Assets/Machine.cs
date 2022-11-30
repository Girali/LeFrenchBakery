using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : Interactable
{
    public string machineName;
    public Type type;
    protected bool inUse = false;

    public virtual void OnEnter(RecipeObject r)
    {

    }

    public virtual RecipeObject OnExit()
    {
        return null;
    }

    public enum Type
    {
        None,
        Four,
        Mixer
    }
}
