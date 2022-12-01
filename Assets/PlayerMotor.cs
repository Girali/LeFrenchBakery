using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float speed;

    public void Move(bool up, bool down, bool left, bool right, Vector3 mousePos)
    {
        Vector3 frd = mousePos - transform.position;
        frd.y = 0;
        Vector3 direction = Vector3.zero;
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

        direction = direction.normalized;
        rb.velocity = direction*speed;
        transform.rotation = Quaternion.LookRotation(frd);
    }
}
