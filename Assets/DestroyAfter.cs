using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float time = 1f;

    private void Start()
    {
        Destroy(gameObject,time);
    }
}
