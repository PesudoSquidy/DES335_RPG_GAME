using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] private int damage;

    public float distance;
    public float toSpeed;
    private float returnSpeed;

    private Vector3 destination;
    private GameObject player;

    bool toReturn = false;

    private float approximateThreshold = 0.1f;

    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    private Augment equipmentAugment;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerMovement = player.GetComponent<PlayerMovement>();

        if (playerAttack == null)
            playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

        if (playerAttack.eq_Name == "Boomerang")
            equipmentAugment = EquipmentManager.instance.MainEquipment().augment;
        else if (playerAttack.eq_Name_2 == "Boomerang")
            equipmentAugment = EquipmentManager.instance.SideEquipment().augment;

        //Up
        //if (transform.localRotation.z <= 0)
        if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Up)
            destination = new Vector2(transform.localPosition.x, transform.localPosition.y + distance);
        //Left
        //else if (transform.localRotation.z > 0 && transform.localRotation.z <= 90)
        else if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Left)
            destination = new Vector2(transform.localPosition.x - distance, transform.localPosition.y);

        //Down 
        //else if (transform.localRotation.z > 90 && transform.localRotation.z <= 180)
        else if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Down)
            destination = new Vector2(transform.localPosition.x, transform.localPosition.y - distance);

        //Right
        //else if (transform.localRotation.z > 180 && transform.localRotation.z <= 270)
        else if (playerMovement.playerFaceDir == PlayerMovement.faceDir.Right)
            destination = new Vector2(transform.localPosition.x + distance, transform.localPosition.y);

    }


    // Update is called once per frame
    void Update()
    {
        Vector3 vectorPath = destination - transform.position;

        if (!toReturn)
        {
            transform.position = Vector2.Lerp(transform.position, destination, toSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, destination) <= approximateThreshold)
            {
                toReturn = true;
                returnSpeed = toSpeed;
            }
        }
        else if(toReturn)
        {
            destination = player.transform.position;

            transform.position = Vector2.Lerp(transform.position, destination, returnSpeed * Time.deltaTime);
            returnSpeed *= 1.02f;

            if (Vector2.Distance(transform.position, destination) <= approximateThreshold)
                Destroy(gameObject);
        }
    }

  
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            toReturn = true;
            returnSpeed = toSpeed;
        }
        else if(col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("FlyingEnemy") || col.gameObject.CompareTag("Boss"))
        {
            if (playerAttack.eq_Name == "Boomerang")
                equipmentAugment = EquipmentManager.instance.MainEquipment().augment;
            else if (playerAttack.eq_Name_2 == "Boomerang")
                equipmentAugment = EquipmentManager.instance.SideEquipment().augment;

            EnemyHealth enemyHP_Script = col.GetComponent<EnemyHealth>();

            enemyHP_Script.TakeDamage(damage);

            if (equipmentAugment != null)
            {
                if (equipmentAugment.augmentStatus == Augment.AugmentStatus.Burn)
                {
                    if (enemyHP_Script != null && enemyHP_Script.currStatusCondition != EnemyHealth.StatusCondition.Burn)
                        enemyHP_Script.TakeDamage(0, EnemyHealth.StatusCondition.Burn);
                }
                else if (equipmentAugment.augmentStatus == Augment.AugmentStatus.Freeze)
                {
                    if (enemyHP_Script != null && enemyHP_Script.currStatusCondition != EnemyHealth.StatusCondition.Freeze)
                        enemyHP_Script.TakeDamage(0, EnemyHealth.StatusCondition.Freeze);
                }
            }

        }
    }
}
