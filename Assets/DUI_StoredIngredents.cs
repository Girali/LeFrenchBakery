using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DUI_StoredIngredents : MonoBehaviour
{
    [SerializeField]
    private Jun_TweenRuntime left;
    [SerializeField]
    private Jun_TweenRuntime right;
    [SerializeField]
    private Jun_TweenRuntime add;
    [SerializeField]
    private Image added;
    [SerializeField]
    private TMP_Text buttonText;
    [SerializeField]
    private Image buttonPanel;
    
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text titre;

    [SerializeField]
    private IngredientStorage ingredientStorage;

    private Ingredient currentArticle;
    private bool interactable = true;

    public void UpdateView()
    {
        if(currentArticle != null)
            UpdateView(currentArticle);
    }

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
            add.Play();
            added.sprite = currentArticle.sprite;
            ingredientStorage.AddIngredient(currentArticle);
            SoundController.Instance.Hover();
            UpdateView(currentArticle);
        }
    }

    public void UpdateView(Ingredient item)
    {
        currentArticle = item;

        icon.sprite = item.sprite;
        titre.text = item.name;

        IngredientStock stock = MagasinController.Instance.FindStockByIngredent(item);
        if (stock.count <= 0)
        {
            icon.CrossFadeColor(Color.gray, 0, true, true);
            buttonText.CrossFadeColor(Color.gray, 0, true, true);
            buttonPanel.CrossFadeColor(Color.gray, 0, true, true);
            buttonText.text = "Empty";
            interactable = false;
        }
        else
        {
            icon.CrossFadeColor(Color.white, 0, true, true);
            buttonText.CrossFadeColor(Color.white, 0, true, true);
            buttonPanel.CrossFadeColor(Color.white, 0, true, true);
            interactable = true;
            buttonText.text = "Take";
        }
    }
}
