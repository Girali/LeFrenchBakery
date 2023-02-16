using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FournisseurController : MonoBehaviour
{
    public List <StockItem> stockItems= new List <StockItem> ();

    public StockItem FindIngredientStock(Ingredient i)
    {
        return stockItems.Where((ingr) => ingr.ingredient.ingredient == i.ingredient).ToArray()[0];
    }

    public void BuyItemFromStock(Ingredient item)
    {
        for (int i = 0; i < stockItems.Count; i++)
        {
            StockItem stockItem = stockItems[i];
            if (stockItem.ingredient.ingredient == item.ingredient)
            {
                SoundController.Instance.BuyItem();
                stockItem.count--;
                MagasinController.Instance.SubMoney(item.prix);
                MagasinController.Instance.AddIngredient(item);
                stockItems[i] = stockItem;
            }
        }
    }

    public void ResetStock()
    {
        for (int i = 0; i < stockItems.Count; i++)
        {
            stockItems[i].count = 20;
        }
    }
}
[System.Serializable]
public class StockItem
{
    public Ingredient ingredient;
    public int count;
}