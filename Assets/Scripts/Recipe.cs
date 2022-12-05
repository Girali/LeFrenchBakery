using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New recipe", menuName = "Recipes")]
public class Recipe : ScriptableObject
{
    public string reciepeName;
    public Sprite icon;
    public string description;

    public RecipeStep[] steps;
    public Article result;
}

[System.Serializable]
public class RecipeStep
{
    public enum ActionType
    {
        UseIngredient,
        UseMachine
    }

    public ActionType actionType;
    public Ingredient.NomIngredient ingredientToUse = Ingredient.NomIngredient.None;
    public Machine.Type machineToUse = Machine.Type.None;
}