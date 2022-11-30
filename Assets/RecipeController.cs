using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeController : MonoBehaviour
{
    [SerializeField]
    private Recipe[] recipes;
    [SerializeField]
    private GameObject reciepPrefab;
    private List<RecipeObject> recipeObjects;

    private void Start()
    {
        recipeObjects = new List<RecipeObject>();
    }

    public void StartReciepe(int i)
    {
        GameObject g = Instantiate(reciepPrefab);
        RecipeObject o = g.GetComponent<RecipeObject>();
        o.Init(recipes[i]);
        recipeObjects.Add(o);
    }
}
