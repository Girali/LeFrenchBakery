using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_RecipeFollower : MonoBehaviour
{
    public Image icon;
    public TMP_Text title;
    public Image[] ingredients;
    public Image currentStep;
    public GameObject working;
    public GameObject failed;

    public void Show(bool b)
    {
        gameObject.SetActive(b);
    }

    public void UpdateView(RecipeObject r)
    {
        icon.sprite = r.Recipe.result.sprite;
        title.text = r.Recipe.result.name;

        if (r.Failed)
        {
            failed.SetActive(true);
            working.SetActive(false);
        }
        else
        {
            failed.SetActive(false);
            working.SetActive(true);
            if (r.CurrentStep < r.Recipe.steps.Length)
            {
                if (r.Recipe.steps[r.CurrentStep].actionType == RecipeStep.ActionType.UseIngredient)
                {
                    currentStep.sprite = MagasinController.Instance.ingredientStocks.Where((ingr) => ingr.ingredient.ingredient == r.Recipe.steps[r.CurrentStep].ingredientToUse).ToArray()[0].ingredient.sprite;
                }
                else
                {
                    currentStep.sprite = MagasinController.Instance.machinePairIcons.Where((m) => m.type == r.Recipe.steps[r.CurrentStep].machineToUse).ToArray()[0].sprite;
                }
            }

            for (int i = 0; i < ingredients.Length; i++)
            {
                if (r.Recipe.steps.Length <= i)
                {
                    ingredients[i].gameObject.SetActive(false);
                }
                else
                {
                    if (r.Recipe.steps[i].actionType == RecipeStep.ActionType.UseIngredient)
                    {
                        Ingredient ingredient = MagasinController.Instance.ingredientStocks.Where((ingr) => ingr.ingredient.ingredient == r.Recipe.steps[i].ingredientToUse).ToArray()[0].ingredient;
                        ingredients[i].sprite = ingredient.sprite;
                    }
                    else
                    {
                        MachinePairIcon machine = MagasinController.Instance.machinePairIcons.Where((m) => m.type == r.Recipe.steps[i].machineToUse).ToArray()[0];
                        ingredients[i].sprite = machine.sprite;
                    }
                }
            }
        }
    }
}
