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
    public Button bouton;
    public Ingredient item;

    public FournisseurController monFournisseur;
    private void Start()
    {
        icon.sprite = item.sprite;
        titre.text = item.name;
        description.text = item.description;
        prix.text = item.prix.ToString()+"€";
    }
    public void BuyItem()
    {
        monFournisseur.BuyItemFromStock(item);
    }
}
