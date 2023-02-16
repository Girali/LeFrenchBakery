using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Money : MonoBehaviour
{
    private TMP_Text text;

    [SerializeField]
    private GameObject add;
    [SerializeField]
    private GameObject sub;

    private float lastMoney = 0;


    private void Start()
    {
        MagasinController.Instance.onMoneyChange += OnMoneyChange;
        text = GetComponent<TMP_Text>();
        text.text = MagasinController.Instance.Money + "$";
    }

    public void OnMoneyChange()
    {
        if(lastMoney == 0)
            lastMoney = MagasinController.Instance.Money;

        if (MagasinController.Instance.Money > lastMoney)
        {
            GameObject g = Instantiate(add, transform.parent);
            g.GetComponent<TMP_Text>().text = "+ " + (MagasinController.Instance.Money - lastMoney);
            g.SetActive(true);
        }
        else
        {
            if (lastMoney - MagasinController.Instance.Money != 0)
            {
                GameObject g = Instantiate(sub, transform.parent);
                g.GetComponent<TMP_Text>().text = "- " + (lastMoney - MagasinController.Instance.Money);
                g.SetActive(true);
            }
        }

        lastMoney = MagasinController.Instance.Money;

        text.text = MagasinController.Instance.Money + "$";
    }
}
