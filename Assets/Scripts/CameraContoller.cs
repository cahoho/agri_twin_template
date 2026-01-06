using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    private Vector3 lastMousePosition;
    public float RotationSpeed = 1f;
    public float MoveSpeed = 1f;
    private void Update()
    {
        Move();
    }
    private void Move()
    {

        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false;

            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * RotationSpeed, Space.World);
            transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * RotationSpeed, Space.Self);
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("U"), Input.GetAxis("Vertical") ) * MoveSpeed * Time.deltaTime);
        }
        else 
        { 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;


        }
    }


}
