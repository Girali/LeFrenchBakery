using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform kichen;
    [SerializeField]
    private Transform entrance;

    private Coroutine currentCRT;

    [SerializeField]
    private AnimationCurve animationCurvePosition;
    [SerializeField]
    private AnimationCurve animationCurveRotation;

    IEnumerator AnimationCRT(Transform b,float time)
    {
        float t = 0;
        Transform a = new GameObject("temp").transform;
        a.position = transform.position;
        a.rotation = transform.rotation;

        while (t < time)
        {
            t += Time.deltaTime;
            transform.position = Vector3.LerpUnclamped(a.position, b.position, animationCurvePosition.Evaluate(t / time));
            transform.rotation = Quaternion.LerpUnclamped(a.rotation, b.rotation, animationCurvePosition.Evaluate(t / time));
            yield return null;
        }

        transform.position = b.position;
        transform.rotation = b.rotation;
        Destroy(a.gameObject);
    }

    public void FromStartToKichen()
    {
        transform.position = start.position;
        transform.rotation = start.rotation;
        if (currentCRT != null)
            StopCoroutine(currentCRT);
        currentCRT = StartCoroutine( AnimationCRT(kichen, 5));
    }

    public void FromKichenToEtrance()
    {
        if (currentCRT != null)
            StopCoroutine(currentCRT);
        currentCRT = StartCoroutine(AnimationCRT(entrance, 1));
    }

    public void FromEtranceToKichen()
    {
        if (currentCRT != null)
            StopCoroutine(currentCRT);

        currentCRT = StartCoroutine(AnimationCRT(kichen, 1));
    }
}
