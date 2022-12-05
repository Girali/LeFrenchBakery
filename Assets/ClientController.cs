using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    [SerializeField]
    private GameObject clientPrefab;

    [SerializeField]
    private QueuePlace[] clients;

    [SerializeField]
    private ClientPath[] enters;
    [SerializeField]
    private ClientPath[] exits;

    //private IEnumerator Start()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(4);
    //        AddClient();
    //    }
    //}

    public void AddClient()
    {
        Client c = Instantiate(clientPrefab).GetComponent<Client>();

        int ietr = Random.Range(0, enters.Length);
        int iext = Random.Range(0, exits.Length);

        Article a = MagasinController.Instance.GetPossibleArticle();

        c.Init(enters[ietr].Copy(), exits[iext].Copy(), a);
        c.onEnter += ClientEnters;
        c.onSuccess += ClientSuccess;
        c.onFail += ClientFailed;
    }

    public void ClientEnters(Client c)
    {
        for (int i = 0; i < clients.Length; i++)
        {
            if (clients[i].client == null)
            {
                clients[i].client = c;
                c.EnterShop(clients[i]);
                break;
            }
        }
    }

    public void ClientSuccess(Client c)
    {
        for (int i = 0; i < clients.Length; i++)
        {
            if (clients[i].client == c)
            {
                clients[i].client = null;
                break;
            }
        }
    }

    public void ClientFailed(Client c)
    {
        for (int i = 0; i < clients.Length; i++)
        {
            if (clients[i].client == c)
            {
                clients[i].client = null;
                break;
            }
        }
    }
}

[System.Serializable]
public class QueuePlace
{
    public Transform position;
    public Client client;
}

[System.Serializable]
public class ClientPath
{
    public Transform[] postions;
    public int currentPosition = 0;
    private float totalDistance = 0;
    private float doneDistance = 0;

    public ClientPath Copy()
    {
        ClientPath c = new ClientPath();
        c.postions = postions;
        c.currentPosition = currentPosition;
        c.totalDistance = totalDistance;
        c.doneDistance = doneDistance;
        c.InitPath();
        return c;
    }

    public void InitPath()
    {
        totalDistance = 0;

        for (int i = 0; i < postions.Length - 1; i++)
        {
            totalDistance += Vector3.Distance(postions[i].position, postions[i + 1].position);
        }
    }

    public Vector3 GetPostion(float t)
    {
        doneDistance = 0;

        for (int i = 0; i < currentPosition + 1; i++)
        {
            float currentSegment = Vector3.Distance(postions[i].position, postions[i + 1].position);
            float rest = totalDistance - currentSegment - doneDistance;
            float currentDistance = t * totalDistance;
            doneDistance += currentSegment;
            float segmentT = (currentDistance - (doneDistance - currentSegment)) / currentSegment;
            
            //Debug.LogError(currentDistance + " - " + (doneDistance - currentSegment) + " / " + currentSegment + "   " + currentPosition + "   " + t);
            //Debug.LogError(currentDistance + "  " + rest + "   " + currentSegment + "   " + doneDistance + "   " + totalDistance + "   " + segmentT + "  " + currentPosition);
            if (currentPosition == i)
            {
                if (currentDistance < doneDistance)
                {
                    return Vector3.Lerp(postions[i].position, postions[i + 1].position, segmentT);
                }
                else
                {
                    currentPosition++;
                    currentPosition = Mathf.Clamp(currentPosition, 0, postions.Length - 1);
                    return postions[i + 1].position;
                }
            }
        }

        return Vector3.zero;
    }

    public Quaternion GetRotation()
    {
        if (currentPosition == postions.Length - 1)
        {
            return postions[currentPosition].rotation;
        }
        else
        {
            return Quaternion.LookRotation(postions[currentPosition + 1].position - postions[currentPosition].position);
        }
    }
}