using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeController : MonoBehaviour
{
    [SerializeField]
    private GameObject reciepPrefab;
    private List<RecipeObject> recipeObjects;

    private void Start()
    {
        recipeObjects = new List<RecipeObject>();
    }

    public RecipeObject StartReciepe(Article i)
    {
        GameObject g = Instantiate(reciepPrefab);
        RecipeObject o = g.GetComponent<RecipeObject>();
        o.Init(i.recette);
        recipeObjects.Add(o);
        return o;
    }
}
