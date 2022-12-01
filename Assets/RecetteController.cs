using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecetteController : MonoBehaviour
{
    public List <ArticleStock> articles = new List<ArticleStock> ();
    public void AddArticleToStock(Article item)
    {
        for (int i = 0; i < articles.Count; i++)
        {
            ArticleStock article = articles[i];
            if (article.typearticle.name == item.name)
            {
                article.count++;
                MagasinController.Instance.SubIngredient(item.count);
                MagasinController.Instance.AddArticle(item);
                articles[i] = article;
            }
        }
    }
}
[System.Serializable]
public class ArticleStock
{
    public int count;
    public Article typearticle;
}
