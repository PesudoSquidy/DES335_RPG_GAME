using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private Animator anim;

    public GameObject arrow;

    public Transform firePoint_Left;
    public Transform firePoint_Right;
    public Transform firePoint_Up;
    public Transform firePoint_Down;

    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
            anim = gameObject.GetComponent<Animator>();
        if (playerMovement == null)
            playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("isAttacking");
            SpawnArrow();
        }
    }

    void SpawnArrow()
    {
        if (arrow != null)
        {
            Transform firePoint = null;

            if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Right)
                firePoint = firePoint_Right;
            else if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Left)
                firePoint = firePoint_Left;
            else if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Up)
                firePoint = firePoint_Up;
            else if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Down)
                firePoint = firePoint_Down;

            if (firePoint != null)
                Instantiate(arrow, firePoint.position, firePoint.rotation);
        }
    }
}
