using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticleStorage : Machine
{
    [SerializeField]
    private Article article;
    [SerializeField]
    private int count;
    [SerializeField]
    private bool wasUsed;
    [SerializeField]
    private DUI_Storage dui;
    [SerializeField]
    private GameObject articlePrefab;

    public Article Article
    {
        get { return article; }
    }

    public int Count
    {
        get { return count; }
    }

    public bool WasUsed
    {
        get { return wasUsed; }
    }

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

    public void SetCount(int i)
    {
        count = i;
        dui.UpdateView(this);
    }

    public void SetUse(bool b)
    {
        wasUsed = b;
        dui.UpdateView(this);
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
