using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientCell : MonoBehaviour
{
    public Image icon;
    public TMP_Text title;
    public TMP_Text countText;
    public TMP_Text description;
    public Button button;
    public TMP_Text buttonText;
    public Ingredient item;
    public IngredientStorage ingredientStorage;

    private void OnEnable()
    {
        title.text = item.name;
        icon.sprite = item.sprite;
        description.text = item.description;

        IngredientStock stock = MagasinController.Instance.FindStockByIngredent(item);
        countText.text = stock.count + " X";

        if(stock.count <= 0)
        {
            buttonText.text = "Empty";
            button.interactable = false;
        }
        else
        {
            button.interactable= true;
            buttonText.text = "Take";
        }
    }

    public void TakeIngredient()
    {
        ingredientStorage.AddIngredient(item);
    }
}
