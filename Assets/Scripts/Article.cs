using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Article : ScriptableObject

{

    public float prix;
    public int peremption;
    public int creation;
    public int dureeConsommation;
    public Sprite sprite;
    [TextArea]
    public string description;
    public Recipe recette;
    public Qualite qualite;
    public GameObject prefab;

    public enum Qualite 
    {
        Mediocre,
        PasBon,
        Moyen,
        Bon,
        Exceptionnel,
    }

}