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

    private EquipmentManager equipmentManager;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
            anim = gameObject.GetComponent<Animator>();
        if (playerMovement == null)
            playerMovement = gameObject.GetComponent<PlayerMovement>();

        if (equipmentManager == null)
            equipmentManager = EquipmentManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Left Mouse Click
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (equipmentManager.mainEquipment().name == "Bow")
            {
                anim.SetTrigger("isAttacking");
                SpawnArrow();
            }
            else if(equipmentManager.mainEquipment().name == "Bomb")
            {
                GameObject bomb = equipmentManager.mainEquipment().prefab;

                Instantiate(bomb, gameObject.transform.position, Quaternion.identity);
            }
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
