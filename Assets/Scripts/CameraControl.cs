using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Camera cam;

    float MoveVelosity = 1.0f;
    float ZoomVelosity = 1.0f;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += new Vector3(0, MoveVelosity, 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-MoveVelosity, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += new Vector3(0, -MoveVelosity, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(MoveVelosity, 0, 0);
        }



        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0)
        {
            cam.orthographicSize -= ZoomVelosity;
        }
        
        if (scroll < 0)
        {
            cam.orthographicSize += ZoomVelosity;
        }
    }
}
