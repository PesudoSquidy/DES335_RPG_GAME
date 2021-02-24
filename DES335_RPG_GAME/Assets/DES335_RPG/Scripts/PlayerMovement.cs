using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Transform trans;

    public int moveSpeed;

    private Animator anim;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Transform>() != null)
            trans = gameObject.GetComponent<Transform>();

        if (anim == null)
            anim = gameObject.GetComponent<Animator>();

        if (cam == null)
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Run the Player's Animation
        anim.SetFloat("horizontalSpeed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        anim.SetFloat("verticalSpeed", Input.GetAxisRaw("Vertical"));
        
        // Calculating Mouse position in world space with respect to the player's pos from Camera
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        if (Input.GetButton("Horizontal"))
        {
            // Face Right
            if(Input.GetAxisRaw("Horizontal") > 0)
            //if(direction.x > 0)
                trans.localScale = new Vector3(10, 10, 1);

            // Face Left
            //else if(direction.x < 0)
            else if(Input.GetAxisRaw("Horizontal") < 0)
                trans.localScale = new Vector3(-10, 10, 1);


            trans.position += Vector3.right * moveSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
        }

        if(Input.GetButton("Vertical"))
        {
            trans.position += Vector3.up * moveSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        }
    }
}
