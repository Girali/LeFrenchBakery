using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FournisseurController : MonoBehaviour
{
    public List <StockItem> stockItems= new List <StockItem> ();
    public void BuyItemFromStock(Ingredient item)
    {
        for (int i = 0; i < stockItems.Count; i++)
        {
            StockItem stockItem = stockItems[i];
            if (stockItem.ingredient.ingredient==item.ingredient)
            {
                stockItem.count--;
                MagasinController.Instance.SubMoney(item.prix);
                MagasinController.Instance.AddIngredient(item);
                stockItems[i] = stockItem;
            }
        }
    }
}
[System.Serializable]
public class StockItem
{
    public Ingredient ingredient;
    public int count;
}