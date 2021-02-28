using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;

    public GameObject arrow;
    public GameObject flamethrower;

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
                SpawnRangedProjectile(arrow);
            }
            else if(equipmentManager.mainEquipment().name == "Bomb")
            {
                GameObject bomb = equipmentManager.mainEquipment().prefab;

                Instantiate(bomb, gameObject.transform.position, Quaternion.identity);
            }
            else if (equipmentManager.mainEquipment().name == "Flamethrower")
            {
                //GameObject bomb = equipmentManager.mainEquipment().prefab;

                Debug.Log("Shoot Flamethrower");
                SpawnRangedProjectile(flamethrower);
            }
        }
    }

    void SpawnRangedProjectile(GameObject rangedProjectile)
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


        if (firePoint != null && rangedProjectile != null)
        {
            if (rangedProjectile.name == arrow.name)
                Instantiate(rangedProjectile, firePoint.position, firePoint.rotation);
            else if (rangedProjectile.name == flamethrower.name)
            {
                if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Left)
                    Instantiate(rangedProjectile, firePoint.position, firePoint.rotation).GetComponent<Transform>().localScale = new Vector2(-1, 1);
                else
                    Instantiate(rangedProjectile, firePoint.position, firePoint.rotation);
            }
        }
    }
}
