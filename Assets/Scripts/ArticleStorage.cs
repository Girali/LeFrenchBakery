using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class ArticleStorage : Machine
{
    public Article article;
    public int count;
    public bool wasUsed;
    public DUI_Storage dui;
    public GameObject articlePrefab;

    public override bool CanInteract(PlayerInteractionController pic, PlayerObjectController poc)
    {
        if (poc.InteractableObject != null)
        {
            ArticleObject articleObject = poc.InteractableObject.GetComponent<ArticleObject>();
            if (articleObject != null)
            {
                return article.name == articleObject.Article.name;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void Start()
    {
        dui.UpdateView(this);
    }

    public override void OnEnter(InteractableObject r, GameObject p)
    {
        base.OnEnter(r, p);
        PlayerObjectController poc = user.GetComponent<PlayerObjectController>();
        if(poc.InteractableObject != null)
        {
            AddArticle((ArticleObject)r);
            poc.DestroyInteractableObject();
        }
        else
        {
            SubArticle();
            InteractableObject g = Instantiate(articlePrefab).GetComponent<InteractableObject>();
            g.Init(article);
            poc.AddInteractableObject(g);
        }
    }

    public override InteractableObject OnExit()
    {
        return base.OnExit();
    }

    public void AddArticle(ArticleObject a)
    {
        if (article.name == a.Article.name)
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
