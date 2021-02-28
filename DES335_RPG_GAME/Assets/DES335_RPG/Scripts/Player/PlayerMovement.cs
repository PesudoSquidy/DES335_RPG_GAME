using System.Collections;
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

        anim.SetFloat("horizontalSpeed", movement.x);
        anim.SetFloat("verticalSpeed", movement.y);
        anim.SetFloat("speed", movement.sqrMagnitude);

        if (movement.x > 0)
        {
            SetDirectionFacing("Right");
            playerFaceDir = faceDir.Right;
        }
        else if (movement.x < 0)
        {
            SetDirectionFacing("Left");
            playerFaceDir = faceDir.Left;
        }
        if (movement.y > 0)
        {
            SetDirectionFacing("Up");
            playerFaceDir = faceDir.Up;
        }
        else if (movement.y < 0)
        {
            SetDirectionFacing("Down");
            playerFaceDir = faceDir.Down;
        }
        if (Input.GetButtonDown("Horizontal"))
        {
            //anim.SetBool("faceHorizontal", true);
        }
        else if (Input.GetButtonDown("Vertical"))
        {
            //anim.SetBool("faceHorizontal", false);
        }

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
            anim.SetInteger("faceDir", 1);
        else if (str == "Right")
            anim.SetInteger("faceDir", 2);
        else if (str == "Up")
            anim.SetInteger("faceDir", 3);
        else if (str == "Down")
            anim.SetInteger("faceDir", 4);
    }
}
