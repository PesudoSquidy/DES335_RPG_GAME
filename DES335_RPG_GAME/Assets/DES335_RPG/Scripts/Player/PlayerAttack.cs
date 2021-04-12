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

    [SerializeField] private PlayerSkill playerSkill;
    [SerializeField] private GeneralGameManager gameManager;


    public EquipmentManager equipmentManager;

    //Weapon - Cooldown
    public float equipmentCooldown;
    public bool isAttacking;

    GameObject weaponDirection;

    // Weapon_1
    public string eq_Name = "Null";
    GameObject eq_GO = null;
    public float eq_CD = 0.0f;

    // Weapon_2
    public string eq_Name_2 = "Null";
    GameObject eq_GO_2 = null;
    public float eq_CD_2 = 0.0f;

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

        isAttacking = false;
        equipmentCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (eq_CD > 0)
            eq_CD -= Time.deltaTime;

        if (eq_CD_2 > 0)
            eq_CD_2 -= Time.deltaTime;

        // Reset value at start - can be changed later
        isAttacking = false;

        if (playerSkill.isDigging == false && !gameManager.ui_active)
        {
            // Update the equipment data
            if(Input.GetButtonDown("Attack"))
            {
                eq_Name = equipmentManager.MainEquipment().name;
                eq_GO = equipmentManager.MainEquipment().prefab;

                //Debug.Log("Set 1st Weapon: " + eq_Name);
            }
            else if(Input.GetButtonDown("Attack_2"))
            {
                eq_Name_2 = equipmentManager.SideEquipment().name;
                eq_GO_2 = equipmentManager.SideEquipment().prefab;

                //Debug.Log("Set 2nd Weapon: " + eq_Name_2);
            }

            // Set the cooldown of the weapon
            //if (equipmentCooldown <= 0)
            {
                if (Input.GetButtonDown("Attack") && eq_CD <= 0)
                {
                    UtiliseWeapon(eq_Name, eq_GO);
                    eq_CD = equipmentManager.MainEquipment().coolDown;

                    //Debug.Log("Set 1st CD");
                }
                else if (Input.GetButtonDown("Attack_2") && eq_CD_2 <= 0)
                {
                    UtiliseWeapon(eq_Name_2, eq_GO_2);
                    eq_CD_2 = equipmentManager.SideEquipment().coolDown;

                    //Debug.Log("Set 2nd CD");
                }
            }

            // Set the continuous weapon attack
            if(Input.GetButton("Attack"))
            {
                if (eq_Name == "Flamethrower")
                    UtiliseWeapon(eq_Name, eq_GO);
            }
            else if(Input.GetButtonUp("Attack"))
            {
                if (eq_Name == "Flamethrower")
                {
                    Debug.Log("Turn off Flamethrower_1");

                    playerMovement.lockFaceDir = false;

                    for (int i = 0; i < weaponDirection.transform.childCount; ++i)
                        weaponDirection.transform.GetChild(i).gameObject.SetActive(true);

                    weaponDirection.GetComponentInChildren<Animator>().SetBool("isAlive", false);
                }
            }

            if (Input.GetButton("Attack_2"))
            {
                if (eq_Name_2 == "Flamethrower")
                    UtiliseWeapon(eq_Name_2, eq_GO_2);
            }
            else if (Input.GetButtonUp("Attack_2"))
            {
                if (eq_Name_2 == "Flamethrower")
                {
                    playerMovement.lockFaceDir = false;

                    for (int i = 0; i < weaponDirection.transform.childCount; ++i)
                        weaponDirection.transform.GetChild(i).gameObject.SetActive(true);

                    weaponDirection.GetComponentInChildren<Animator>().SetBool("isAlive", false);
                }
            }

            //if (Input.GetButton("Attack"))
            //{
            //    if (eq_Name == "Flamethrower")
            //        UtiliseWeapon(eq_Name, eq_GO);
            //}
            //else if (Input.GetButton("Attack_2"))
            //{
            //    if (eq_Name_2 == "Flamethrower")
            //    {
            //        if (eq_Name_2 == "Flamethrower")
            //        {
            //            isAttacking = true;
            //            playerMovement.lockFaceDir = true;

            //            if (weaponDirection == null)
            //            {
            //                Debug.Log("weaponDir: " + weaponDirection);
            //                weaponDirection = Instantiate(flamethrower);
            //            }
            //            else
            //            {
            //                for (int i = 0; i < weaponDirection.transform.childCount; ++i)
            //                    weaponDirection.transform.GetChild(i).gameObject.SetActive(true);

            //                weaponDirection.GetComponentInChildren<Animator>().SetBool("isAlive", true);
            //            }
            //        }
            //    }
            //    //UtiliseWeapon(eq_Name_2, eq_GO_2);
            //}

            //else if (Input.GetButtonUp("Attack") || Input.GetButtonUp("Attack_2"))
            //{
            //    isAttacking = false;

            //    if (eq_Name == "Flamethrower" || eq_Name_2 == "Flamethrower")
            //    {
            //        playerMovement.lockFaceDir = false;

            //        for (int i = 0; i < weaponDirection.transform.childCount; ++i)
            //            weaponDirection.transform.GetChild(i).gameObject.SetActive(true);

            //        weaponDirection.GetComponentInChildren<Animator>().SetBool("isAlive", false);
            //    }
            //}

            if (weaponDirection && Input.GetButton("Attack") || weaponDirection && Input.GetButton("Attack_2"))
                UpdateWeaponDirection();
        }
        else if (playerSkill.isDigging && isAttacking)
        {
            // Turn off any variable related to attacking - Diggging takes priority
            //isAttacking = false;

            if (eq_Name == "Flamethrower" || eq_Name_2 == "Flamethrower")
            {
                playerMovement.lockFaceDir = false;

                for (int i = 0; i < weaponDirection.transform.childCount; ++i)
                    weaponDirection.transform.GetChild(i).gameObject.SetActive(true);

                weaponDirection.GetComponentInChildren<Animator>().SetBool("isAlive", false);
            }

            if (weaponDirection)
                UpdateWeaponDirection();
        }
    }

    void UtiliseWeapon(string eqName, GameObject eqGO)
    {
        if (eqName == "Bow")
        {
            anim.SetTrigger("isAttacking");
            SpawnRangedProjectile(arrow);
        }
        else if (eqName == "Boomerang")
            SpawnRangedProjectile(eqGO);
        else if (eqName == "Bomb")
            Instantiate(eqGO, gameObject.transform.position, Quaternion.identity);

        if (eqName == "Flamethrower")
        {
            isAttacking = true;
            playerMovement.lockFaceDir = true;

            if (weaponDirection == null)
                weaponDirection = Instantiate(flamethrower);
            else
            {
                for (int i = 0; i < weaponDirection.transform.childCount; ++i)
                    weaponDirection.transform.GetChild(i).gameObject.SetActive(true);

                weaponDirection.GetComponentInChildren<Animator>().SetBool("isAlive", true);
            }
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
            Instantiate(rangedProjectile, firePoint.position, firePoint.rotation);

            //if (rangedProjectile.name == arrow.name)
            //    Instantiate(rangedProjectile, firePoint.position, firePoint.rotation);

            //else if (rangedProjectile.name == flamethrower.name)
            //{
            //    if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Left)
            //        Instantiate(rangedProjectile, firePoint.position, firePoint.rotation).GetComponent<Transform>().localScale = new Vector2(-Mathf.Abs(rangedProjectile.transform.localScale.x), rangedProjectile.transform.localScale.y);
            //    else
            //        Instantiate(rangedProjectile, firePoint.position, firePoint.rotation);
            //}
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
