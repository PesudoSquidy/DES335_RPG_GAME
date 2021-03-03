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

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private PlayerSkill playerSkill;

    public EquipmentManager equipmentManager;


    //Weapon - Cooldown
    public float bombCooldown;

    GameObject weaponDirection;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();

        if (equipmentManager == null)
            equipmentManager = EquipmentManager.instance;

        if (playerSkill == null)
            playerSkill = GetComponent<PlayerSkill>();

        bombCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (bombCooldown > 0)
            bombCooldown -= Time.deltaTime;

        // Left Mouse Click
        if (playerSkill.isDigging == false)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (equipmentManager.mainEquipment().name == "Bow")
                {
                    anim.SetTrigger("isAttacking");
                    SpawnRangedProjectile(arrow);
                }
                else if (equipmentManager.mainEquipment().name == "Bomb" && bombCooldown <= 0)
                {
                    GameObject bomb = equipmentManager.mainEquipment().prefab;
                    bombCooldown = equipmentManager.mainEquipment().coolDown;
                    Instantiate(bomb, gameObject.transform.position, Quaternion.identity);
                }
            }
            //else if(Input.GetKeyUp(KeyCode.J))
            //{
            //    if (equipmentManager.mainEquipment().name == "Flamethrower")
            //    {
            //        //Debug.Log("Player can change face dir");
            //        playerMovement.lockFaceDir = false;
            //    }
            //}

            if (Input.GetKey(KeyCode.J))
            {
                if (equipmentManager.mainEquipment().name == "Flamethrower" && !weaponDirection)
                {
                    //Debug.Log("Player cannot change face dir");
                    //playerMovement.lockFaceDir = true;
                    //SpawnRangedProjectile(flamethrower);

                    weaponDirection = Instantiate(flamethrower);
                }
            }
            else
                weaponDirection = null;

            if(weaponDirection && Input.GetKey(KeyCode.J))
                UpdateWeaponDirection();
        }
    }

    void SpawnRangedProjectile(GameObject rangedProjectile)
    {
        Transform firePoint = null;

        if (playerSkill.tunnel != null && playerSkill.tunnel.GetComponent<Tunnel>().otherEnd != null)
            firePoint = playerSkill.tunnel.GetComponent<Tunnel>().otherEnd.transform;

        if (firePoint == null)
        {
            if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Right)
                firePoint = firePoint_Right;
            else if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Left)
                firePoint = firePoint_Left;
            else if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Up)
                firePoint = firePoint_Up;
            else if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Down)
                firePoint = firePoint_Down;
        }

        if (firePoint != null && rangedProjectile != null)
        {
            if (rangedProjectile.name == arrow.name)
                Instantiate(rangedProjectile, firePoint.position, firePoint.rotation);
            else if (rangedProjectile.name == flamethrower.name)
            {
                if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Left)
                    Instantiate(rangedProjectile, firePoint.position, firePoint.rotation).GetComponent<Transform>().localScale = new Vector2(-Mathf.Abs(rangedProjectile.transform.localScale.x), rangedProjectile.transform.localScale.y);
                else
                    Instantiate(rangedProjectile, firePoint.position, firePoint.rotation);
            }
        }
    }

    void UpdateWeaponDirection()
    {
        Transform firePoint = null;

        if (playerSkill.tunnel != null && playerSkill.tunnel.GetComponent<Tunnel>().otherEnd != null)
            firePoint = playerSkill.tunnel.GetComponent<Tunnel>().otherEnd.transform;

        if (firePoint == null)
        {
            if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Right)
                firePoint = firePoint_Right;
            else if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Left)
                firePoint = firePoint_Left;
            else if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Up)
                firePoint = firePoint_Up;
            else if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Down)
                firePoint = firePoint_Down;
        }

        if (firePoint != null)
        {
            weaponDirection.GetComponent<Transform>().position = firePoint.position;
            weaponDirection.GetComponent<Transform>().rotation = firePoint.rotation;

            if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Left)
                weaponDirection.GetComponent<Transform>().localScale = new Vector2(-Mathf.Abs(weaponDirection.transform.localScale.x), weaponDirection.transform.localScale.y);
            else
                weaponDirection.GetComponent<Transform>().localScale = new Vector2(Mathf.Abs(weaponDirection.transform.localScale.x), weaponDirection.transform.localScale.y);
        }
    }
}
