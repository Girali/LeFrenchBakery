using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClientController : MonoBehaviour
{

    private List<Client> clients = new List<Client>();
    [SerializeField]
    private GameObject clientPrefab;

    [SerializeField]
    private ClientPath[] enters;
    [SerializeField]
    private ClientPath[] exits;

    public void AddClient()
    {
        Client c = Instantiate(clientPrefab).GetComponent<Client>();

        int ietr = Random.Range(0, enters.Length);
        int iext = Random.Range(0, exits.Length);

        Article a = MagasinController.Instance.GetPossibleArticle();

        c.Init(enters[ietr].Copy(), exits[iext].Copy(), a);
    }
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
            float currentSegment = Vector3.Distance(postions[currentPosition].position, postions[currentPosition + 1].position);
            float rest = totalDistance - currentSegment - doneDistance;
            doneDistance += currentSegment;
            if(t * totalDistance < doneDistance)
            {
                float segmentT = ((t * totalDistance) - doneDistance - rest) / currentSegment;
                return Vector3.Lerp(postions[currentPosition].position, postions[currentPosition + 1].position, segmentT);
            }
        }

        return Vector3.zero;
    }

    public Quaternion GetRotation()
    {
        return Quaternion.LookRotation(postions[currentPosition + 1].position - postions[currentPosition].position);
    }
}