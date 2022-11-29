using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private PlayerMotor pm;
    [SerializeField]
    private PlayerInteractionController pic;
    private bool up;
    private bool down;
    private bool left;
    private bool right;
    private bool interact;
    private bool interactDown;
    private Vector3 mousePosition;
    private GameObject hitObject;

    private int layerMaskFloor;
    private int layerMaskInteractable;

    void Start()
    {
        layerMaskFloor = LayerMask.GetMask("Floor");
        layerMaskInteractable = LayerMask.GetMask("Interactable");
    }

    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 20f, layerMaskFloor);
        mousePosition = hit.point;

        Debug.DrawRay(transform.position + (Vector3.down * 0.5f), transform.forward, Color.red);

        Physics.Raycast(transform.position + (Vector3.down * 0.5f), transform.forward, out hit, 20f, layerMaskInteractable);
        if(hit.transform)
            hitObject = hit.transform.gameObject;

        up = Input.GetKey(KeyCode.UpArrow);
        down = Input.GetKey(KeyCode.DownArrow);
        left = Input.GetKey(KeyCode.LeftArrow);
        right = Input.GetKey(KeyCode.RightArrow);
        interact = Input.GetKey(KeyCode.Space);
        interactDown = Input.GetKeyDown(KeyCode.Space);

        pm.Move(up, down, left, right, mousePosition);
        pic.Interact(interact, interactDown, hitObject);
    }

}
