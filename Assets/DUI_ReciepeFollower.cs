using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DUI_ReciepeFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;
    [SerializeField]
    private Image currentStep;
    [SerializeField]
    private GameObject working;
    [SerializeField]
    private GameObject failed;
    [SerializeField]
    private GameObject success;
    public void Show(bool b)
    {
        parent.SetActive(b);
    }

    public void UpdateView(ArticleObject r)
    {
        currentStep.sprite = r.Article.sprite;
        
        failed.SetActive(false);
        working.SetActive(false);
        success.SetActive(true);
    }

    public void UpdateView(RecipeObject r)
    {
        if (r.Failed)
        {
            failed.SetActive(true);
            working.SetActive(false);
            success.SetActive(false);
        }
        else
        {
            success.SetActive(false);
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
            else if (r.CurrentStep == r.Recipe.steps.Length)
            {
                working.SetActive(false);
                success.SetActive(true);
            }
        }
    }
}
