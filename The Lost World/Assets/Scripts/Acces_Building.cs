using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acces_Building : MonoBehaviour
{

    public GameObject AccesedBuilding;
    [SerializeField]
    private float DetectionCapsuleLength;
    [SerializeField]
    private float DetectionCapsuleRadius;
    [SerializeField]
    private LayerMask DetectionSphereMask;
    public bool Building_Inventory_Opened;
    [SerializeField]
    private GameObject IC_Backup_Position;
    [SerializeField]
    private GameObject IC_Normal_Position;


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            Collider[] colliders = Physics.OverlapCapsule(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * DetectionCapsuleLength, DetectionCapsuleRadius, DetectionSphereMask);

            if (colliders.Length > 0)
            {
                if (colliders[0].gameObject.tag == "Furnace")
                {
                    if (colliders[0].GetComponent<Item_004>().BuildingAccessed == false)
                    {
                        AccesedBuilding = colliders[0].gameObject;

                        colliders[0].GetComponent<Item_004>().BuildingAccessed = true;
                        FindObjectOfType<Inventory>().Item_004_Inventory_Panel.SetActive(true);
                        FindObjectOfType<Inventory>().inventory_craftingIsActive = true;
                        Building_Inventory_Opened = true;
                    }
                    else           // e deja in meniu la furnace si acu iese
                    {
                        AccesedBuilding = null;

                        colliders[0].GetComponent<Item_004>().BuildingAccessed = false;
                        Building_Inventory_Opened = false;
                        FindObjectOfType<Inventory>().inventory_craftingIsActive = false;
                        FindObjectOfType<Inventory>().Inventory_Crafting_Panel.SetActive(false);
                        FindObjectOfType<Inventory>().Item_004_Inventory_Panel.SetActive(false); 
                        FindObjectOfType<PlayerMovement>().MovementFrozen = false;
                    }
                }

            }

        }
        ////////////////////////////////ceva separare

        if (Building_Inventory_Opened == true)
        {
            FindObjectOfType<Inventory>().Inventory_Crafting_Panel.transform.position = IC_Backup_Position.transform.position;
            FindObjectOfType<Inventory>().Inventory_Crafting_Panel.SetActive(true);
            FindObjectOfType<Inventory>().inventory_craftingIsActive = true;
            FindObjectOfType<PlayerMovement>().MovementFrozen = true;
        }
        else
        {
            FindObjectOfType<Inventory>().Inventory_Crafting_Panel.transform.position = IC_Normal_Position.transform.position;

        }
    }    

}
