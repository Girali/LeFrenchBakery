using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class LivreRecette : MonoBehaviour
{
    public Image icon;
    public TMP_Text titre;
    public TMP_Text description;
    public Article item;

    public CookBook cookBook;

    public Image[] ingredientsRecette;
    private void Start()
    {
        icon.sprite = item.sprite;
        titre.text = item.name;
        description.text = item.description;

        for (int i = 0; i < ingredientsRecette.Length; i++)
        {
            if (item.recette.steps.Length <= i)
            {
                ingredientsRecette[i].gameObject.SetActive(false);
            }
            else
            {
                if (item.recette.steps[i].actionType == RecipeStep.ActionType.UseIngredient)
                {
                    Ingredient ingredient = MagasinController.Instance.ingredientStocks.Where((ingr) => ingr.ingredient.ingredient == item.recette.steps[i].ingredientToUse).ToArray()[0].ingredient;
                    ingredientsRecette[i].sprite = ingredient.sprite;
                }
                else
                {
                    MachinePairIcon machine = MagasinController.Instance.machinePairIcons.Where((m) => m.type == item.recette.steps[i].machineToUse).ToArray()[0];
                    ingredientsRecette[i].sprite = machine.sprite;
                }
            }
        }
    }
    
    public void StartRecipe()
    {
        cookBook.StartRecipe(item);
    }
}
