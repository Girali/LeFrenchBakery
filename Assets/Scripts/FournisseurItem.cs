using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FournisseurItem : MonoBehaviour
{
public Image icon;
    public TMP_Text titre;
    public TMP_Text description;
    public TMP_Text prix;
    public TMP_Text count;
    public Button bouton;
    public Ingredient item;
    public FournisseurController monFournisseur;

    private void Awake()
    {
        MagasinController.Instance.onIngredentStockChange += UpdateView;
    }

    private void OnEnable()
    {
        icon.sprite = item.sprite;
        titre.text = item.name;
        description.text = item.description;

        UpdateView();
    }

    public void UpdateView()
    {
        StockItem stockItem = monFournisseur.FindIngredientStock(item);
        count.text = stockItem.count + "x";
        if (MagasinController.Instance.Money >= item.prix)
        {
            if (stockItem.count <= 0)
            {
                prix.text = "Empty";
                bouton.interactable = false;
            }
            else
            {
                prix.text = item.prix.ToString() + "$";
                bouton.interactable = true;
            }
        }
        else
        {
            bouton.interactable = false;
        }
    }

    public void BuyItem()
    {
        monFournisseur.BuyItemFromStock(item);
    }
}
