using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause : MonoBehaviour
{
    public void Unpause()
    {
        AppController.Instance.Unpause();
    }

    public void Quit()
    {
        AppController.Instance.QuitApp();
    }
}
