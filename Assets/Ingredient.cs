using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "nouvel ingredient", menuName = "Ingredient")]
public class Ingredient : ScriptableObject
{
    public enum NomIngredient
    {
        Farine,
        Eau,
        Levure,
        Levain,
        Cereales,
        Olives,
        Noix,
        Mais,
        Beurre,
        Sucre,
        Chocolat,
        None
    }
    public NomIngredient ingredient;
    public float prix;
    public int peremption;
    public int creation;
    public int dureeUtilisation;
    public Sprite sprite;
    [TextArea]
    public string description;

}
