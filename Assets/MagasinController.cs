using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagasinController : MonoBehaviour
{
    private static MagasinController _instance;
    public static MagasinController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MagasinController>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private Jun_TweenRuntime blackFadeOut;
    [SerializeField]
    private CameraController cameraController;

    private void Start()
    {
        blackFadeOut.Play();
        cameraController.FromStartToKichen();
    }


    private float money = 100;

    public float Money
    {
        get
        {
            return money;
        }
    }

    public void AddMoney(float f)
    {
        if(f >= 0)
            money += f;
    }

    public void SubMoney(float f)
    {
        if (f >= 0)
            money -= f;
    }

    public void AddIngredient(object o)
    {

    }

    public void SubIngredient(object o)
    {

    }

    public void AddArticle(object o)
    {

    }

    public void SubArticle(object o)
    {

    }
}
