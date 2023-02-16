using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;

    private int count = 0;  

    private void OnTriggerExit(Collider other)
    {
        PlayerMotor pm = other.GetComponent<PlayerMotor>();
        if (pm)
        {
            float f = Vector3.Dot(transform.right, (pm.transform.position - transform.position).normalized);

            if (f > 0)
            {
                count--;
                
                if(count < 0)
                    count = 0;
                
                if (count == 0)
                    cameraController.FromEtranceToKichen();
            }
            else
            {
                count++;
                
                if (count > 2)
                    count = 2;
                
                cameraController.FromKichenToEtrance();
            }
        }
    }
}
