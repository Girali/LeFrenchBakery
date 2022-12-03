using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagasinController : MonoBehaviour
{
    private static MagasinController _instance;
    public static MagasinController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MagasinController>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private Jun_TweenRuntime blackFadeOut;
    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private IngredientStock[] ingredientStocks;
    [SerializeField]
    private ArticleStorage[] articleStocks;

    private void Start()
    {
        blackFadeOut.Play();
        cameraController.FromStartToKichen();
    }

    private float money = 100;

    public float Money
    {
        get
        {
            return money;
        }
    }

    public void AddMoney(float f)
    {
        if(f >= 0)
            money += f;
    }

    public void SubMoney(float f)
    {
        if (f >= 0)
            money -= f;
    }

    public void AddIngredient(Ingredient o)
    {
        for (int i = 0; i < ingredientStocks.Length; i++)
        {
            if (ingredientStocks[i].ingredient.ingredient == o.ingredient)
            {
                ingredientStocks[i].count++;
            }
        }
    }

    public void SubIngredient(Ingredient o)
    {
        for (int i = 0; i < ingredientStocks.Length; i++)
        {
            if (ingredientStocks[i].ingredient.ingredient == o.ingredient)
            {
                ingredientStocks[i].count--;
            }
        }
    }

    public void AddArticle(Article o)
    {
        for (int i = 0; i < articleStocks.Length; i++)
        {
            if (articleStocks[i].article.name == o.name)
            {
                articleStocks[i].AddArticle();
            }
        }
    }

    public Article GetPossibleArticle()
    {
        ArticleStorage a = null;

        do
        {
            a = articleStocks[Random.Range(0, articleStocks.Length)];
        }
        while (a == null || !a.wasUsed);

        return a.article;
    }

    public void SubArticle(Article o)
    {
        for (int i = 0; i < articleStocks.Length; i++)
        {
            if (articleStocks[i].article.name == o.name)
            {
                articleStocks[i].SubArticle();
            }
        }
    }
}

[System.Serializable]
public class IngredientStock
{
    public Ingredient ingredient;
    public int count;
}
