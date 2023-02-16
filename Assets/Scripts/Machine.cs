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
    protected InteractableObject interactableObject;
    protected bool inUse = false;

    public Type GetMachineType { get => type; }

    public virtual void OnEnter(InteractableObject r, GameObject p)
    {
        interactableObject = r;
        user = p.GetComponent<PlayerController>();
    }

    public virtual InteractableObject OnExit()
    {
        user = null;
        InteractableObject r = interactableObject;
        interactableObject = null;
        return r;
    }

    public enum Type
    {
        None,
        CookBook,
        Trash,
        Ingredients,
        Four,
        Mixer,
        Storage,
        Fournisseur
    }
}
