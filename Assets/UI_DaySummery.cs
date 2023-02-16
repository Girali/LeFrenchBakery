using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_DaySummery : MonoBehaviour
{
    [SerializeField]
    private TMP_Text dayText;

    [SerializeField]
    private TMP_Text resultText;

    [SerializeField]
    private TMP_Text gainText;
    [SerializeField]
    private TMP_Text lostText;

    [SerializeField]
    private TMP_Text happyText;
    [SerializeField]
    private TMP_Text sadText;

    public void Show(bool b)
    {
        gameObject.SetActive(b);
    }

    public void Init(int day, float gains, float loses, int happy, int sad)
    {
        dayText.text = "Day #" + day + "\nFinished";
        int i = (int)(gains - loses);
        if(i < 0)
            resultText.text = "Today you lost " + (-i) + "$.";
        else
            resultText.text = "Today you made " + i + "$.";

        gainText.text = "+" + gains + " $";
        lostText.text = "-" + loses + " $";

        happyText.text = ": " + happy;
        sadText.text = ": " + sad;
    }

    public void Quit()
    {
        MagasinController.Instance.Day++;
        MagasinController.Instance.SaveGame();
        Application.Quit();
    }

    public void Next() 
    {
        MagasinController.Instance.Day++;
        MagasinController.Instance.SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AppController.Instance.Pausable = true;
    }
}
