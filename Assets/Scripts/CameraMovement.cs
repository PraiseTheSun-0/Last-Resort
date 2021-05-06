using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //edge scroll variables
    public int camera_margin = 10;
    public float camera_speed = 10f;

    //zoom variables
    public float zoom_speed = 80f;
    private float zoom_value = 5f;

    //pan variables
    private Vector3 lastMousePosition = new Vector3();
    private bool panning = false;

    Rigidbody2D rb;
    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        if (Application.isFocused)
        {
            HandlePanning();
            if(!panning) HandleEdgeScrolling();
            HandleZooming();
        }
    }

    private void HandleEdgeScrolling()
    {
        if (Input.mousePosition.x > Screen.width - camera_margin)
        {
            rb.AddForce(new Vector2(1, 0) * camera_speed * Time.deltaTime);
        }
        if (Input.mousePosition.x < camera_margin)
        {
            rb.AddForce(new Vector2(-1, 0) * camera_speed * Time.deltaTime);
        }

        if (Input.mousePosition.y > Screen.height - camera_margin)
        {
            rb.AddForce(new Vector2(0, 1) * camera_speed * Time.deltaTime);
        }
        if (Input.mousePosition.y < camera_margin)
        {
            rb.AddForce(new Vector2(0, -1) * camera_speed * Time.deltaTime);
        }
    }


    private void HandleZooming()
    {
        Vector3 mPos_beforeZoom = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.mouseScrollDelta.y > 0)
        {
            zoom_value -= zoom_speed * Time.deltaTime;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            zoom_value += zoom_speed * Time.deltaTime;
        }
        zoom_value = Mathf.Clamp(zoom_value, 2f, 15f);
        cam.orthographicSize = zoom_value;

        Vector3 mPos_afterZoom = cam.ScreenToWorldPoint(Input.mousePosition);
        cam.transform.position += (mPos_beforeZoom - mPos_afterZoom);
    }

    private void HandlePanning()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            panning = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            panning = false;
        }

        if (panning)
        {
            Vector3 currentMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += (lastMousePosition - currentMousePosition);
        }
        
    }

}