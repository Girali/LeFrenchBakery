using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField]
    private DUI_Client ui;

    private ClientPath enterPath;
    private ClientPath exitPath;
    private Article article;

    private float curentTime;
    [SerializeField]
    private float pathTime;
    [SerializeField]
    private float waitLimit;

    [SerializeField]
    private float lerpSpeed = 0.1f;

    private Phase currentPhase;

    private Vector3 tragetPosition;
    private Quaternion targetRotation;

    public enum Phase
    {
        None,
        Entering,
        Awaiting,
        Exiting
    }

    public void Init(ClientPath entr, ClientPath exit, Article a)
    {
        currentPhase = Phase.Entering;
        enterPath = entr;
        exitPath = exit;
        article = a;
    }

    private void Update()
    {
        float t = 0;

        switch (currentPhase)
        {
            case Phase.None:
                break;
            case Phase.Entering:
                curentTime += Time.deltaTime;
                t = curentTime / pathTime;

                tragetPosition = Vector3.Lerp(transform.position, enterPath.GetPostion(t), lerpSpeed);
                targetRotation = Quaternion.Lerp(transform.rotation, enterPath.GetRotation(), lerpSpeed);

                if (t >= 1f)
                {
                    curentTime = 0;
                    t = 0;
                    currentPhase = Phase.Awaiting;
                    ui.ShowIcon(true, article);
                }
                break;
            case Phase.Awaiting:
                curentTime += Time.deltaTime;
                t = curentTime / waitLimit;

                ui.UpdateView(t);

                if (t >= 1f)
                {
                    curentTime = 0;
                    t = 0;
                    currentPhase = Phase.Exiting;
                    ui.ShowIcon(false, article);
                }
                break;
            case Phase.Exiting:
                curentTime += Time.deltaTime;
                t = curentTime / pathTime;

                tragetPosition = Vector3.Lerp(transform.position, exitPath.GetPostion(t), lerpSpeed);
                targetRotation = Quaternion.Lerp(transform.rotation, exitPath.GetRotation(), lerpSpeed);

                if (t >= 1f)
                {
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }

        transform.position = tragetPosition;
        transform.rotation = targetRotation;
    }
}
