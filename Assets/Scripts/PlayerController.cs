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
    private bool leftDown;
    private bool rightDown;
    private bool interactLeft;
    private bool interactDownLeft;
    private bool interactDownRight;
    private bool escapeDown;
    private Vector3 mousePosition;
    private GameObject hitObject;

    private int layerMaskFloor;
    private int layerMaskInteractable;

    private bool stopMove = false;

    [SerializeField]
    private PlayerIndex currentPlayer = PlayerIndex.P1;

    public PlayerIndex CurrentPlayer { get => currentPlayer; }

    public enum PlayerIndex
    {
        P1,
        P2
    }

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
        Vector3 direction = transform.forward;
        switch (currentPlayer)
        {
            case PlayerIndex.P1:
                leftDown = Input.GetKeyDown(KeyCode.LeftArrow);
                rightDown = Input.GetKeyDown(KeyCode.RightArrow);
                escapeDown = Input.GetKeyDown(KeyCode.Backspace);
                
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

                Physics.Raycast(transform.position + (Vector3.down * 0.5f), transform.forward, out hit, 20f, layerMaskInteractable);
                if (hit.transform)
                    hitObject = hit.transform.gameObject;

                interactLeft = Input.GetKey(KeyCode.Return);
                interactDownLeft = Input.GetKeyDown(KeyCode.Return);
                interactDownRight = Input.GetKeyDown(KeyCode.RightControl);
                break;
            case PlayerIndex.P2:

                leftDown = Input.GetKeyDown(KeyCode.A);
                rightDown = Input.GetKeyDown(KeyCode.D);
                escapeDown = Input.GetKeyDown(KeyCode.Escape);

                if (!stopMove)
                {
                    up = Input.GetKey(KeyCode.W);
                    down = Input.GetKey(KeyCode.S);
                    left = Input.GetKey(KeyCode.A);
                    right = Input.GetKey(KeyCode.D);
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
                
                Physics.Raycast(transform.position + (Vector3.down * 0.5f), transform.forward, out hit, 20f, layerMaskInteractable);
                if (hit.transform)
                    hitObject = hit.transform.gameObject;

                interactLeft = Input.GetKey(KeyCode.Space);
                interactDownLeft = Input.GetKeyDown(KeyCode.Space);
                interactDownRight = Input.GetKeyDown(KeyCode.LeftControl);
                break;
            default:
                break;
        }

        if (up == true)
        {
            direction = direction + Vector3.right;
        }
        if (down == true)
        {
            direction = direction + Vector3.left;
        }
        if (left == true)
        {
            direction = direction + Vector3.forward;
        }
        if (right == true)
        {
            direction = direction + Vector3.back;
        }

        mousePosition = transform.position;
        mousePosition.y = 0;
        mousePosition += direction;

        pm.Move(up, down, left, right, mousePosition);
        poc.Motor(interactDownRight);
        pic.Interact(interactLeft, interactDownLeft, interactDownRight, hitObject, escapeDown, leftDown , rightDown);
    }
}
