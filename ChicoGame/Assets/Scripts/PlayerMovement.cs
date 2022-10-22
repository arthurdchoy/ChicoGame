using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5.5f, sprintSpeed = 7.5f;
    private bool sprinting;
    // Start is called before the first frame update
    void Start()
    {
        sprinting = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        
        // Check if sprinting
        if (Input.GetKey(KeyCode.LeftShift)) sprinting = true;
        else sprinting = false;

        // Basic Movement
        if(Input.GetKey(KeyCode.W))
        {
            if (sprinting) pos.y += sprintSpeed * Time.deltaTime;
            else pos.y += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (sprinting) pos.x -= sprintSpeed * Time.deltaTime;
            else pos.x -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (sprinting) pos.y -= sprintSpeed * Time.deltaTime;
            else pos.y -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (sprinting) pos.x += sprintSpeed * Time.deltaTime;
            else pos.x += moveSpeed * Time.deltaTime;
        }

        this.transform.position = pos;
    }
}
