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

    private void OnEnable()
    {
        title.text = item.name;
        icon.sprite = item.sprite;
        description.text = item.description;

        IngredientStock stock = MagasinController.Instance.FindStockByIngredent(item);
        countText.text = stock.count + " X";
    }

    public void TakeIngredient(Ingredient i)
    {
        MagasinController.Instance.SubIngredient(item);
    }
}
