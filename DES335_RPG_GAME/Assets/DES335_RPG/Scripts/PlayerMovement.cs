using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Transform trans;

    public int moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Transform>() != null)
            trans = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            //trans.position.x += 1.0f;
            //trans.position.x

            trans.position += Vector3.right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        }

        if(Input.GetButton("Vertical"))
        {
            trans.position += Vector3.up * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        }
    }
}
