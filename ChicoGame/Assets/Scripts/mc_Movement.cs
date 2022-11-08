 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mc_Movement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    private Rigidbody2D m_Rigidbody;

    public float runSpeed = 40f;
    public float sprintBoost = 1.5f;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * sprintBoost;
            verticalMove = Input.GetAxisRaw("Vertical") * runSpeed * sprintBoost;
        }
        else
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;
        }

        animator.SetFloat("HSpeed", Mathf.Abs(horizontalMove));
        animator.SetFloat("VSpeed", verticalMove);                  // no abs value because of differen up, down walk animations


    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);

    }
}
