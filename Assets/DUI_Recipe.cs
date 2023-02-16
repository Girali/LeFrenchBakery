using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DUI_Recipe : MonoBehaviour
{
    [SerializeField]
    private Jun_TweenRuntime left;
    [SerializeField]
    private Jun_TweenRuntime right;
    [SerializeField]
    private Jun_TweenRuntime buy;

    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text titre;
    [SerializeField]
    private Image iconStarted;
    
    [SerializeField]
    private Image[] steps;
    [SerializeField]
    private CookBook cookBook;
    
    private Article currentArticle;


    public void Next()
    {
        right.Play();
        SoundController.Instance.Hover();
    }

    public void Prev()
    {
        left.Play();
        SoundController.Instance.Hover();
    }

    public void Buy()
    {
        buy.Play();
        SoundController.Instance.Hover();
    }

    public void OnConfirm()
    {
        cookBook.StartRecipe(currentArticle);
    }

    public void UpdateView(Article item)
    {
        currentArticle = item;

        iconStarted.sprite = item.sprite;
        icon.sprite = item.sprite;
        titre.text = item.name;

        for (int i = 0; i < steps.Length; i++)
        {
            if (item.recette.steps.Length <= i)
            {
                steps[i].gameObject.SetActive(false);
            }
            else
            {
                steps[i].gameObject.SetActive(true);
                
                if (item.recette.steps[i].actionType == RecipeStep.ActionType.UseIngredient)
                {
                    Ingredient ingredient = MagasinController.Instance.ingredientStocks.Where((ingr) => ingr.ingredient.ingredient == item.recette.steps[i].ingredientToUse).ToArray()[0].ingredient;
                    steps[i].sprite = ingredient.sprite;
                }
                else
                {
                    MachinePairIcon machine = MagasinController.Instance.machinePairIcons.Where((m) => m.type == item.recette.steps[i].machineToUse).ToArray()[0];
                    steps[i].sprite = machine.sprite;
                }
            }
        }
    }
}
