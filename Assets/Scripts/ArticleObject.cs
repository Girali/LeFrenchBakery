using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArticleObject : InteractableObject
{
    private Article article;

    public Image icon;

    public Article Article { get => article; }

    public override void Init(Object o)
    {
        base.Init(o);
        article = (Article)o;
        icon.sprite = article.sprite;
    }

    public override void Hold(GameObject g)
    {
        base.Hold(g);
        UpdateView();
    }

    public void UpdateView()
    {
        recipeFollower.UpdateView(this);
    }
}
