using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticleObject : InteractableObject
{
    private Article article;

    public override void Init(Object o)
    {
        base.Init(o);
        article = (Article)o;
    }
}
