using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Money : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        MagasinController.Instance.onMoneyChange += OnMoneyChange;
        text = GetComponent<TMP_Text>();
    }

    public void OnMoneyChange()
    {
        text.text = MagasinController.Instance.Money + "$";
    }
}
