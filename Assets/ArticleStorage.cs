using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticleStorage : Machine
{
    public Article article;
    [SerializeField]
    private int count;
    public bool wasUsed;

    public void AddArticle()
    {
        count++;
        wasUsed = true;
    }
}
