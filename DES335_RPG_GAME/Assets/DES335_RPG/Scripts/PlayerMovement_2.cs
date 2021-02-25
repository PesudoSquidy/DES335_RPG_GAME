using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_2 : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;
    
    private Rigidbody2D rb;
    private Animator anim;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
            rb = gameObject.GetComponent<Rigidbody2D>();

        if (anim == null)
            anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("horizontalSpeed", movement.x);
        anim.SetFloat("verticalSpeed", movement.y);
        anim.SetFloat("speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
