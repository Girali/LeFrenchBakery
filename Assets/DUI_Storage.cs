using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DUI_Storage : MonoBehaviour
{
    public Color empty;
    public Color full;
    public Image icon;

    public void UpdateView(ArticleStorage a) 
    {
        icon.sprite = a.article.sprite;

        if(a.count <= 0)
        {
            icon.color = empty;
        }
        else
        {
            icon.color = full;
        }
    }
}
