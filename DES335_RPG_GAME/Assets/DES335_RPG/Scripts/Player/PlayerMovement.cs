﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera cam;

    [SerializeField]
    private float moveSpeed = 5;

    private Rigidbody2D rb;
    private Animator anim;

    Vector2 movement;

    public enum faceDir { Left = 0, Right, Up, Down};

    public faceDir playerFaceDir;

    public bool lockFaceDir;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
            anim = gameObject.GetComponent<Animator>();

        if (cam == null)
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (rb == null)
            rb = gameObject.GetComponent<Rigidbody2D>();

        playerFaceDir = faceDir.Right;
        lockFaceDir = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculating Mouse position in world space with respect to the player's pos from Camera
        //Vector3 mousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        //Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (lockFaceDir == false)
        {
            anim.SetFloat("horizontalSpeed", movement.x);
            anim.SetFloat("verticalSpeed", movement.y);

            if (movement.x > 0)
                SetDirectionFacing("Right");
            else if (movement.x < 0)
                SetDirectionFacing("Left");
            if (movement.y > 0)
                SetDirectionFacing("Up");
            else if (movement.y < 0)
                SetDirectionFacing("Down");
        }
        else
        {
            if(playerFaceDir == faceDir.Left)
                anim.SetFloat("horizontalSpeed", -1f);
            else if (playerFaceDir == faceDir.Right)
                anim.SetFloat("horizontalSpeed", 1f);
            else if (playerFaceDir == faceDir.Up)
                anim.SetFloat("verticalSpeed", 1f);
            else if (playerFaceDir == faceDir.Right)
                anim.SetFloat("vericalSpeed", -1f);
        }

        anim.SetFloat("speed", movement.sqrMagnitude);


        //if (Input.GetButtonDown("Horizontal"))
        //{
        //    //anim.SetBool("faceHorizontal", true);
        //}
        //else if (Input.GetButtonDown("Vertical"))
        //{
        //    //anim.SetBool("faceHorizontal", false);
        //}
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void SetDirectionFacing(string str)
    {
        /*
         * 1 - Left
         * 2 - Right
         * 3 - Up
         * 4 - Down
        */
        if (str == "Left")
        {
            anim.SetInteger("faceDir", 1);
            playerFaceDir = faceDir.Left;
        }
        else if (str == "Right")
        {
            anim.SetInteger("faceDir", 2);
            playerFaceDir = faceDir.Right;
        }
        else if (str == "Up")
        {
            anim.SetInteger("faceDir", 3);
            playerFaceDir = faceDir.Up;
        }
        else if (str == "Down")
        {
            anim.SetInteger("faceDir", 4);
            playerFaceDir = faceDir.Down;
        }
    }
}
