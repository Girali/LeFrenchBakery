using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Client : Interactable
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

    public UnityAction<Client> onEnter;
    public UnityAction<Client> onSuccess;
    public UnityAction<Client> onFail;

    private Transform placeInQueue;
    private Vector3 tempPlacePosition;
    private Quaternion tempPlaceRotation;
    [SerializeField]
    private float joinQueueTime = 3;
    private bool exitFromQueue = false;

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

    public void EnterShop(QueuePlace place)
    {
        tempPlacePosition = transform.position;
        tempPlaceRotation = transform.rotation;
        placeInQueue = place.position;
        curentTime = 0;
    }

    private void Update()
    {
        if(placeInQueue != null)
        {
            curentTime += Time.deltaTime;
            float t = curentTime / joinQueueTime;

            transform.position = Vector3.Lerp(tempPlacePosition, placeInQueue.position, t); 
            transform.rotation = Quaternion.Lerp(tempPlaceRotation, placeInQueue.rotation,t);

            if (t >= 1f)
            {
                curentTime = 0;
                t = 0;
                tragetPosition = placeInQueue.position;
                targetRotation = placeInQueue.rotation;
                placeInQueue = null;
            }
        }
        else
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

                        if (onEnter != null)
                            onEnter(this);
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

                        tempPlacePosition = transform.position;
                        tempPlaceRotation = transform.rotation;
                        exitFromQueue = true;

                        if (onFail != null)
                            onFail(this);
                    }
                    break;
                case Phase.Exiting:
                    if (exitFromQueue)
                    {
                        curentTime += Time.deltaTime;
                        t = curentTime / joinQueueTime;

                        tragetPosition = Vector3.Lerp(tempPlacePosition, exitPath.postions[0].position, t);
                        targetRotation = Quaternion.Lerp(tempPlaceRotation, exitPath.postions[0].rotation, t);

                        if (t >= 1f)
                        {
                            curentTime = 0;
                            t = 0;
                            tragetPosition = transform.position;
                            targetRotation = transform.rotation;
                            exitFromQueue = false;
                        }
                    }
                    else
                    {
                        curentTime += Time.deltaTime;
                        t = curentTime / pathTime;

                        tragetPosition = Vector3.Lerp(transform.position, exitPath.GetPostion(t), lerpSpeed);
                        targetRotation = Quaternion.Lerp(transform.rotation, exitPath.GetRotation(), lerpSpeed);

                        if (t >= 1f)
                        {
                            Destroy(gameObject);
                        }
                    }
                    break;
                default:
                    break;
            }


            transform.position = tragetPosition;
            transform.rotation = targetRotation;
        }
    }
}