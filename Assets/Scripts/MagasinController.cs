using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private Jun_TweenRuntime blackFadeIn;
    [SerializeField]
    private CameraController cameraController;

    public MachinePairIcon[] machinePairIcons;
    public IngredientStock[] ingredientStocks;
    [SerializeField]
    private ArticleStorage[] articleStocks;

    [SerializeField]
    private ClientController clientController;

    [SerializeField]
    private LightingManager lightingManager;

    public UnityAction onMoneyChange;
    public UnityAction onIngredentStockChange;

    [SerializeField]
    private float money = 35;
    [SerializeField]
    private int day = 1;

    public void StartDay()
    {
        GUI_Controller.Insatance.Notify("Jour #" + day);
    }

    public void EndDay()
    {
        day++;
        SaveGame();
        blackFadeIn.Play();
        Debug.LogError("ups"); 
    }

    public void LoadGame()
    {
        day = PlayerPrefs.GetInt("Day", 1);
        money = PlayerPrefs.GetFloat("Money", 35);

        for (int i = 0; i < articleStocks.Length; i++)
        {
            int count = PlayerPrefs.GetInt("Article_Count_" + articleStocks[i].Article.name, 0);
            bool used = (PlayerPrefs.GetInt("Article_Usage_" + articleStocks[i].Article.name, 0) == 1);
            articleStocks[i].SetCount(count);
            articleStocks[i].SetUse(used);
        }

        for (int i = 0; i < ingredientStocks.Length; i++)
        {
            int count = PlayerPrefs.GetInt("Ingredient_Count_" + ingredientStocks[i].ingredient.name, 0);
            ingredientStocks[i].count = count;
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Day", day);
        PlayerPrefs.SetFloat("Money", money);

        for (int i = 0; i < articleStocks.Length; i++)
        {
            PlayerPrefs.SetInt("Article_Count_" + articleStocks[i].Article.name, articleStocks[i].Count);
            PlayerPrefs.SetInt("Article_Usage_" + articleStocks[i].Article.name, articleStocks[i].WasUsed ? 1 : 0);
        }

        for (int i = 0; i < ingredientStocks.Length; i++)
        {
            PlayerPrefs.SetInt("Ingredient_Count_" + ingredientStocks[i].ingredient.name, ingredientStocks[i].count);
        }
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Time.fixedDeltaTime = 1f / 60f;
        blackFadeOut.Play();
        cameraController.FromStartToKichen();
        lightingManager.onTimePeriodChange += OnPeriodUpdate;

        LoadGame();
    }

    private void OnPeriodUpdate(LightingManager.PeriodOfDay p)
    {
        switch (p)
        {
            case LightingManager.PeriodOfDay.PetitMatin:
                StartDay();
                break;
            case LightingManager.PeriodOfDay.Matin:
                    GUI_Controller.Insatance.Notify("Matin");
                break;
            case LightingManager.PeriodOfDay.Midi:
                    GUI_Controller.Insatance.Notify("Midi");
                break;
            case LightingManager.PeriodOfDay.Aprem:
                    GUI_Controller.Insatance.Notify("Apres-Midi ");
                break;
            case LightingManager.PeriodOfDay.Soir:
                    GUI_Controller.Insatance.Notify("Soir");
                break;
            case LightingManager.PeriodOfDay.Nuit:
                    GUI_Controller.Insatance.Notify("Nuit");
                EndDay();
                break;
            default:
                break;
        }
    }


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
        if(onMoneyChange != null)
            onMoneyChange();
    }

    public void SubMoney(float f)
    {
        if (f >= 0)
            money -= f;
        if(onMoneyChange != null)
            onMoneyChange();
    }

    public IngredientStock FindStockByIngredent(Ingredient ing)
    {
        for (int i = 0; i < ingredientStocks.Length; i++)
        {
            if (ingredientStocks[i].ingredient.ingredient == ing.ingredient)
                return ingredientStocks[i];
        }

        return null;
    }

    public ArticleStorage FindStorageByArticle(Article a)
    {
        for (int i = 0; i < articleStocks.Length; i++)
        {
            if (articleStocks[i].Article.name == a.name)
                return articleStocks[i];
        }

        return null;
    }

    public void AddIngredient(Ingredient o)
    {
        for (int i = 0; i < ingredientStocks.Length; i++)
        {
            if (ingredientStocks[i].ingredient.ingredient == o.ingredient)
            {
                ingredientStocks[i].count++;
                if(onIngredentStockChange != null)
                    onIngredentStockChange();
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
                if(onIngredentStockChange != null)
                    onIngredentStockChange();
            }
        }
    }

    public Article GetPossibleArticle()
    {
        ArticleStorage a = null;

        int i = 0;
        int maxRounds = 100;

        do
        {
            a = articleStocks[Random.Range(0, articleStocks.Length)];
        }
        while ((a == null || !a.WasUsed) && i < maxRounds);

        a = articleStocks[0];

        return a.Article;
    }
}

[System.Serializable]
public class IngredientStock
{
    public Ingredient ingredient;
    public int count;
}

[System.Serializable]
public class MachinePairIcon
{
    public Machine.Type type;
    public Sprite sprite;
}