using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DUI_Fournisseur : MonoBehaviour
{
    [SerializeField]
    private Jun_TweenRuntime left;
    [SerializeField]
    private Jun_TweenRuntime right;
    [SerializeField]
    private Jun_TweenRuntime buy;
    [SerializeField]
    private TMP_Text price;
    
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text titre;

    [SerializeField]
    private DUI_StoredIngredents storedIngredents;

    [SerializeField]
    private FournisseurController fournisseur;

    private Ingredient currentArticle;
    private bool interactable = true;

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
        if (interactable)
        {
            fournisseur.BuyItemFromStock(currentArticle);
            SoundController.Instance.Hover();
            UpdateView(currentArticle);
            storedIngredents.UpdateView();
        }
    }

    public void UpdateView(Ingredient item)
    {
        currentArticle = item;

        icon.sprite = item.sprite;
        titre.text = item.name;

        StockItem stockItem = fournisseur.FindIngredientStock(item);
        if (MagasinController.Instance.Money >= item.prix)
        {
            
            if (stockItem.count <= 0)
            {
                price.color = Color.gray;
                price.text = "Empty";
                interactable = false;
            }
            else
            {
                price.color = Color.white;
                price.text = item.prix.ToString() + "$";
                interactable = true;
            }
        }
        else
        {
            price.color = Color.gray;
            price.text = item.prix.ToString() + "$";
            interactable = false;
        }
    }
}
