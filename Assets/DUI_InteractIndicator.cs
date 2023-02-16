using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DUI_InteractIndicator : MonoBehaviour
{
    [SerializeField]
    private GameObject interactIndicator;
    [SerializeField]
    private GameObject textOnly;
    [SerializeField]
    private GameObject textIcon;
    [SerializeField]
    private TMP_Text text1;
    [SerializeField]
    private TMP_Text text2;
    [SerializeField]
    private Image icon;
    private Transform toFollowObject = null;
    private Vector3 offsetToFollow = Vector3.zero;

    private void Update()
    {
        if (toFollowObject != null)
        {
            interactIndicator.transform.position = toFollowObject.transform.position + offsetToFollow;
        }
    }

    public void ShowInidicator(Transform pos, bool b, string t, Sprite s, Vector3 offset = new Vector3())
    {
        interactIndicator.SetActive(b);
        textOnly.SetActive(false);
        textIcon.SetActive(true);

        text1.text = t;
        text2.text = t;

        icon.sprite = s;

        if (pos != null)
        {
            Collider c = pos.GetComponent<Collider>();
            Vector3 p = c.bounds.center + Vector3.up;

            offsetToFollow = (p - pos.position) + offset;
            toFollowObject = pos;

            interactIndicator.transform.position = pos.position + offsetToFollow;
        }
    }

    public void ShowInidicator(Transform pos, bool b, string t, Vector3 offset = new Vector3())
    {
        interactIndicator.SetActive(b);
        textOnly.SetActive(true);
        textIcon.SetActive(false);

        text1.text = t;
        text2.text = t;

        if (pos != null)
        {
            Collider c = pos.GetComponent<Collider>();
            Vector3 p = c.bounds.center + Vector3.up;

            offsetToFollow = (p - pos.position) + offset;
            toFollowObject = pos;

            interactIndicator.transform.position = pos.position + offsetToFollow;
        }
    }

    public void ShowInidicator(bool b)
    {
        interactIndicator.SetActive(b);
        textOnly.SetActive(b);
        textIcon.SetActive(b);
    }
}
