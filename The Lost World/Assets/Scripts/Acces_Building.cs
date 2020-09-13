using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField]
    bool aaaa;

    void Start()
    {

    }

    void Update()
    {
        if (FindObjectOfType<Place_Building>().Building_In_Hand == false && FindObjectOfType<Place_Prefab>().Prefab_In_Hand == false)
        {
            Collider[] colliders = Physics.OverlapCapsule(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * DetectionCapsuleLength, DetectionCapsuleRadius, DetectionSphereMask);
            if (colliders.Length > 0)
            {
                Buttons(colliders);

                if (Input.GetKeyDown(KeyCode.E))
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
                    else if(colliders[0].tag == "Sap extractor")
                    {
                        if(colliders[0].transform.parent.GetComponent<Item_026>().time_On_This_Round >= colliders[0].transform.parent.GetComponent<Item_026>().production_Time)  //cu parent ca scriptu e pe punct da colliders[0] nu e pct ci gameobjectu de pe punct
                        {
                            FindObjectOfType<Inventory>().quantityToAdd = 1;
                            FindObjectOfType<Inventory>().itemCodeToAdd = 27;
                            colliders[0].transform.parent.GetComponent<Item_026>().time_On_This_Round = 0;

                            FindObjectOfType<Pickup_Item>().BeeAttract();      //cand ieisapu din extractor atragi albinele ca cand culegi merele 
                        }

                    }
                    else if (colliders[0].gameObject.tag == "Storage box")
                    {
                        if (colliders[0].GetComponent<Item_030>().BuildingAccessed == false)
                        {
                            AccesedBuilding = colliders[0].gameObject;

                            colliders[0].GetComponent<Item_030>().BuildingAccessed = true;
                            FindObjectOfType<Inventory>().Item_030_Inventory_Panel.SetActive(true);
                            FindObjectOfType<Inventory>().inventory_craftingIsActive = true;
                            Building_Inventory_Opened = true;
                        }
                        else           // e deja in meniu la furnace si acu iese
                        {
                            AccesedBuilding = null;

                            colliders[0].GetComponent<Item_030>().BuildingAccessed = false;
                            Building_Inventory_Opened = false;
                            FindObjectOfType<Inventory>().inventory_craftingIsActive = false;
                            FindObjectOfType<Inventory>().Inventory_Crafting_Panel.SetActive(false);
                            FindObjectOfType<Inventory>().Item_030_Inventory_Panel.SetActive(false);
                            FindObjectOfType<PlayerMovement>().MovementFrozen = false;
                        }
                    }

                }

            }
            else
                FindObjectOfType<Buttons>().AccesBuildingButton.SetActive(false);
        }
        else
            FindObjectOfType<Buttons>().AccesBuildingButton.SetActive(false);


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



    void Buttons(Collider[] colliders)
    {
        FindObjectOfType<Buttons>().AccesBuildingButton.SetActive(true);

        if (colliders[0].gameObject.tag == "Furnace")
        {
            if (colliders[0].GetComponent<Item_004>().BuildingAccessed == false)
                FindObjectOfType<Buttons>().AccesBuildingButton.transform.Find("Building_Name").GetComponent<TextMeshProUGUI>().text = "Acces 'Furnace'";
            else
                FindObjectOfType<Buttons>().AccesBuildingButton.transform.Find("Building_Name").GetComponent<TextMeshProUGUI>().text = "Close 'Furnace'";
        }
        else if (colliders[0].gameObject.tag == "Sap extractor")
        {
            if (colliders[0].transform.parent.GetComponent<Item_026>().time_On_This_Round >= colliders[0].transform.parent.GetComponent<Item_026>().production_Time)
                FindObjectOfType<Buttons>().AccesBuildingButton.transform.Find("Building_Name").GetComponent<TextMeshProUGUI>().text = "Collect 'Cactus sap'";               
            else
                FindObjectOfType<Buttons>().AccesBuildingButton.transform.Find("Building_Name").GetComponent<TextMeshProUGUI>().text = "Time remained: " + ((int)(colliders[0].transform.parent.GetComponent<Item_026>().production_Time - colliders[0].transform.parent.GetComponent<Item_026>().time_On_This_Round) / 60).ToString() + ":" + ((int)(colliders[0].transform.parent.GetComponent<Item_026>().production_Time - colliders[0].transform.parent.GetComponent<Item_026>().time_On_This_Round) % 60).ToString();
        }
        else if (colliders[0].gameObject.tag == "Storage box")
        {
            if (colliders[0].GetComponent<Item_030>().BuildingAccessed == false)
                FindObjectOfType<Buttons>().AccesBuildingButton.transform.Find("Building_Name").GetComponent<TextMeshProUGUI>().text = "Acces 'Storage box'";
            else
                FindObjectOfType<Buttons>().AccesBuildingButton.transform.Find("Building_Name").GetComponent<TextMeshProUGUI>().text = "Close 'Storage box'";
        }

    }

}
