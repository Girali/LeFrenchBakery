using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticleObject : InteractableObject
{
    private Article article;

    public Article Article { get => article; }

    public override void Init(Object o)
    {
        base.Init(o);
        article = (Article)o; 
        UpdateView();
    }

    public override void Hold(GameObject g)
    {
        base.Hold(g);
        UpdateView();
    }

    public void UpdateView()
    {
        GUI_Controller.Insatance.recipeFollower.UpdateView(this);
    }
}
