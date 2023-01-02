using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float speed;

    [SerializeField]
    private Animator animator;

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

        if(direction == Vector3.zero)
        {
            animator.SetBool("Wait", true);
        }
        else
        {
            animator.SetBool("Wait", false);
            Vector2Int v = GetOneVector(direction);
            animator.SetFloat("X", v.x);
            animator.SetFloat("Y", v.y);
        }

        direction = direction.normalized;
        rb.velocity = direction*speed;
        transform.rotation = Quaternion.LookRotation(frd);
    }

    public Vector2Int GetOneVector(Vector3 v)
    {
        if (Mathf.Abs(v.x) < Mathf.Abs(v.z))
        {
            if (v.z > 0)
                return new Vector2Int(1, 0);
            else
                return new Vector2Int(-1, 0);
        }
        else
        {
            if (v.x > 0)
                return new Vector2Int(0, 1);
            else
                return new Vector2Int(0, -1);
        }
    }
}
