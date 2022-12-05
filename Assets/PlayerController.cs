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
    [SerializeField]
    private PlayerObjectController poc;
    private bool up;
    private bool down;
    private bool left;
    private bool right;
    private bool interactLeft;
    private bool interactDownLeft;
    private bool interactDownRight;
    private Vector3 mousePosition;
    private GameObject hitObject;

    private int layerMaskFloor;
    private int layerMaskInteractable;

    private bool stopMove = false;

    public void StartStopMove(bool start, Machine machine)
    {
        stopMove = !start;
        pic.StartStopInteract(start, machine);
    }

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

        Physics.Raycast(transform.position + (Vector3.down * 0.5f), transform.forward, out hit, 20f, layerMaskInteractable);
        if(hit.transform)
            hitObject = hit.transform.gameObject;

        if (!stopMove)
        {
            up = Input.GetKey(KeyCode.UpArrow);
            down = Input.GetKey(KeyCode.DownArrow);
            left = Input.GetKey(KeyCode.LeftArrow);
            right = Input.GetKey(KeyCode.RightArrow);
        }
        else
        {
            up = false;
            down = false;
            left = false;
            right = false;
            mousePosition = Vector3.zero;
            hitObject = null;
        }    

        interactLeft = Input.GetMouseButton(0);
        interactDownLeft = Input.GetMouseButtonDown(0);
        interactDownRight = Input.GetMouseButtonDown(1);

        pm.Move(up, down, left, right, mousePosition);
        pic.Interact(interactLeft, interactDownLeft, interactDownRight, hitObject);
        poc.Motor(interactDownRight);
    }

}
