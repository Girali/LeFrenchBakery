using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticleStorage : Machine
{
    public Article article;
    public int count;
    public bool wasUsed;
    public DUI_Storage dui;

    private void Start()
    {
        dui.UpdateView(this);
    }

    public void AddArticle( ArticleObject a)
    {
        if (article.name == a.name)
        {
            count++;
            wasUsed = true;
            dui.UpdateView(this);
        }
    }

    public void SubArticle()
    {
        count--;
        dui.UpdateView(this);
    }
}
