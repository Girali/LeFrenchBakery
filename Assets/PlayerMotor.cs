using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    public void Move(bool up, bool down, bool left, bool right)
    {
        Vector3 direction = Vector3.zero;
        if (up == true) 
        {
            direction = direction + Vector3.forward;
        }
        if (down == true)
        {
            direction = direction + Vector3.back;
        }
        if (left == true)
        {
            direction = direction + Vector3.left;
        }
        if (right == true)
        {
            direction = direction + Vector3.right;
        }
        direction = direction.normalized;
        rb.velocity = direction*speed;

    }
    

}
