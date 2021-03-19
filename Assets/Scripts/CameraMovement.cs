using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int camera_margin = 10;
    public float camera_speed = 10f;
    Rigidbody2D rb;
    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() 
    {
        if(Input.mousePosition.x > Screen.width - camera_margin) 
        {
            rb.AddForce(new Vector2(1, 0) * camera_speed);
        }
        if(Input.mousePosition.x < camera_margin) 
        {
            rb.AddForce(new Vector2(-1, 0) * camera_speed);
        }

        if (Input.mousePosition.y > Screen.height - camera_margin) 
        {
            rb.AddForce(new Vector2(0, 1) * camera_speed);
        }
        if (Input.mousePosition.y < camera_margin) 
        {
            rb.AddForce(new Vector2(0, -1) * camera_speed);
        }

    }
}
