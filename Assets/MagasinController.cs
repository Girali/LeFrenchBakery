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
    private ArticleStorage[] storages;

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

    }

    public void SubIngredient(Ingredient o)
    {

    }

    public void AddArticle(Article o)
    {
        for (int i = 0; i < storages.Length; i++)
        {
            if (storages[i].article.name == o.name)
            {
                storages[i].AddArticle();
            }
        }
    }

    public Article GetPossibleArticle()
    {
        ArticleStorage a = null;

        do
        {
            a = storages[Random.Range(0, storages.Length)];
        }
        while (a == null || !a.wasUsed);

        return a.article;
    }

    public void SubArticle(Article o)
    {

    }
}
