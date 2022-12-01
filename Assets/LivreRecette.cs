using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LivreRecette : MonoBehaviour
{
public Image icon;
    public TMP_Text titre;
    public TMP_Text description;
    public TMP_Text ingredients;
    public Button bouton;
    public Article item;

    public RecetteController monLivre;
    private void Start()
    {
        icon.sprite = item.sprite;
        titre.text = item.name;
        description.text = item.description;
    }
    public void AddArticle()
    {
        monLivre.AddArticleToStock(item);
    }
}
