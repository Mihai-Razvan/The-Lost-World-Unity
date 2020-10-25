using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Acces_Building : MonoBehaviour
{                                           //CA SA INTERACTIONEZI CU CLADIRILE NU NUMAI CA SA ACCESEZI INVENTARU LOR (GEN SI CA SA PUI LEMNE SI INCREDIENTE IN COOKING POT

    public GameObject AccesedBuilding;
    [SerializeField]
    private float DetectionCapsuleLength;
    [SerializeField]
    private float DetectionCapsuleRadius;
    [SerializeField]
    private LayerMask DetectionSphereMask;
    public bool Building_Inventory_Opened;
    public bool Building_Menu_Opened;      //un menu al unei cladiri care se poate deschide si fara sa fie inventaru deschis gen la cooking pot ca acolo inventaru nu se deschide
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
                            FindObjectOfType<Inventory>().inventoryIsActive = true;
                            FindObjectOfType<Inventory>().Inventory_Panel.SetActive(true);
                            FindObjectOfType<Inventory>().Crafting_Panel.SetActive(false);
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
                    else if (colliders[0].tag == "Sap extractor")
                    {
                        if (colliders[0].transform.parent.GetComponent<Item_026>().time_On_This_Round >= colliders[0].transform.parent.GetComponent<Item_026>().production_Time)  //cu parent ca scriptu e pe punct da colliders[0] nu e pct ci gameobjectu de pe punct
                        {
                            FindObjectOfType<Inventory>().quantityToAdd = 1;
                            FindObjectOfType<Inventory>().itemCodeToAdd = 27;
                            colliders[0].transform.parent.GetComponent<Item_026>().time_On_This_Round = 0;
                            FindObjectOfType<Sounds_Player>().collect_item_sound.Play();

                            FindObjectOfType<Pickup_Item>().BeeAttract();      //cand ieisapu din extractor atragi albinele ca cand culegi merele 
                        }

                    }
                    else if (colliders[0].gameObject.tag == "Chest")
                    {
                        if (colliders[0].GetComponent<Item_030>().BuildingAccessed == false)
                        {
                            AccesedBuilding = colliders[0].gameObject;

                            colliders[0].GetComponent<Item_030>().BuildingAccessed = true;
                            FindObjectOfType<Inventory>().Item_030_Inventory_Panel.SetActive(true);
                            FindObjectOfType<Inventory>().inventory_craftingIsActive = true;
                            FindObjectOfType<Inventory>().inventoryIsActive = true;
                            FindObjectOfType<Inventory>().Inventory_Panel.SetActive(true);
                            FindObjectOfType<Inventory>().Crafting_Panel.SetActive(false);
                            Building_Inventory_Opened = true;

                            FindObjectOfType<Sounds_Player>().chest_open_sound.Play();
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

                            FindObjectOfType<Sounds_Player>().chest_open_sound.Play();
                        }
                    }
                    else if (colliders[0].gameObject.tag == "Cooking pot")
                    {
                        if (colliders[0].GetComponent<Item_035>().BuildingAccessed == false)
                        {
                            AccesedBuilding = colliders[0].gameObject;
                            FindObjectOfType<Inventory>().Item_035_Crafting_Panel.SetActive(true);
                            colliders[0].GetComponent<Item_035>().BuildingAccessed = true;
                            Building_Menu_Opened = true;                //nu e inventory da e tot building ceva
                        }
                        else           // e deja in meniu la furnace si acu iese
                        {
                            AccesedBuilding = null;
                            FindObjectOfType<Inventory>().Item_035_Crafting_Panel.SetActive(false);
                            colliders[0].GetComponent<Item_035>().BuildingAccessed = false;
                            Building_Menu_Opened = false;
                            FindObjectOfType<PlayerMovement>().MovementFrozen = false;

                        }
                    }


                }

                if (colliders[0].gameObject.tag == "Cooking pot")
                    CookingPotAdd(colliders);

            }
            else
            {
                FindObjectOfType<Buttons>().AccesBuildingButton.SetActive(false);
                FindObjectOfType<Buttons>().AddButton.SetActive(false);
            }
        }
        else
        {
            FindObjectOfType<Buttons>().AccesBuildingButton.SetActive(false);
            FindObjectOfType<Buttons>().AddButton.SetActive(false);
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
            {   if((int)(colliders[0].transform.parent.GetComponent<Item_026>().production_Time - colliders[0].transform.parent.GetComponent<Item_026>().time_On_This_Round) % 60 >= 10)
                    FindObjectOfType<Buttons>().AccesBuildingButton.transform.Find("Building_Name").GetComponent<TextMeshProUGUI>().text = "Time remained: " + ((int)(colliders[0].transform.parent.GetComponent<Item_026>().production_Time - colliders[0].transform.parent.GetComponent<Item_026>().time_On_This_Round) / 60).ToString() + ":" + ((int)(colliders[0].transform.parent.GetComponent<Item_026>().production_Time - colliders[0].transform.parent.GetComponent<Item_026>().time_On_This_Round) % 60).ToString();
                else
                    FindObjectOfType<Buttons>().AccesBuildingButton.transform.Find("Building_Name").GetComponent<TextMeshProUGUI>().text = "Time remained: " + ((int)(colliders[0].transform.parent.GetComponent<Item_026>().production_Time - colliders[0].transform.parent.GetComponent<Item_026>().time_On_This_Round) / 60).ToString() + ": 0" + ((int)(colliders[0].transform.parent.GetComponent<Item_026>().production_Time - colliders[0].transform.parent.GetComponent<Item_026>().time_On_This_Round) % 60).ToString();
            }
        }
        else if (colliders[0].gameObject.tag == "Chest")
        {
            if (colliders[0].GetComponent<Item_030>().BuildingAccessed == false)
                FindObjectOfType<Buttons>().AccesBuildingButton.transform.Find("Building_Name").GetComponent<TextMeshProUGUI>().text = "Acces 'Chest'";
            else
                FindObjectOfType<Buttons>().AccesBuildingButton.transform.Find("Building_Name").GetComponent<TextMeshProUGUI>().text = "Close 'Chest'";
        }
        else if (colliders[0].gameObject.tag == "Cooking pot")
        {
            if (colliders[0].GetComponent<Item_035>().BuildingAccessed == false)
                FindObjectOfType<Buttons>().AccesBuildingButton.transform.Find("Building_Name").GetComponent<TextMeshProUGUI>().text = "Acces 'Cooking pot'";
            else
                FindObjectOfType<Buttons>().AccesBuildingButton.transform.Find("Building_Name").GetComponent<TextMeshProUGUI>().text = "Close 'Cooking pot'";
        }

    }




    void CookingPotAdd(Collider[] colliders)            //adaugi lemne si incrediente la COOKING POT
    {     
        if (FindObjectOfType<Handing_Item>().SelectedItemCode == 1)       //stick
        {
            if (Input.GetKey(KeyCode.F))
            {
                colliders[0].GetComponent<Item_035>().energy_left += colliders[0].GetComponent<Item_035>().stick_energy;

                FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15]--;
                if (FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] == 0)
                {
                    FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] = 0;
                    FindObjectOfType<Handing_Item>().SelectedItemCode = 0;
                }
            }

            FindObjectOfType<Buttons>().AddButton.SetActive(true);
            FindObjectOfType<Buttons>().AddButton.transform.Find("Item_To_Add_Name").GetComponent<TextMeshProUGUI>().text = "Add 'Stick'";
        }
        else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 18)       //log
        {
            if (Input.GetKey(KeyCode.F))
            {
                colliders[0].GetComponent<Item_035>().energy_left += colliders[0].GetComponent<Item_035>().log_energy;

                FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15]--;
                if (FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] == 0)
                {
                    FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] = 0;
                    FindObjectOfType<Handing_Item>().SelectedItemCode = 0;
                }
            }

            FindObjectOfType<Buttons>().AddButton.SetActive(true);
            FindObjectOfType<Buttons>().AddButton.transform.Find("Item_To_Add_Name").GetComponent<TextMeshProUGUI>().text = "Add 'Log'";
        }
        else if(FindObjectOfType<Handing_Item>().SelectedItemCode == 13)       //honeycomb
        {
            if(Input.GetKey(KeyCode.F))
            {
                colliders[0].GetComponent<Item_035>().quantityToAdd = 1;
                colliders[0].GetComponent<Item_035>().itemCodeToAdd = 13;

                FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15]--;
                if (FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] == 0)
                {
                    FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] = 0;
                    FindObjectOfType<Handing_Item>().SelectedItemCode = 0;
                }
            }

            FindObjectOfType<Buttons>().AddButton.SetActive(true);
            FindObjectOfType<Buttons>().AddButton.transform.Find("Item_To_Add_Name").GetComponent<TextMeshProUGUI>().text = "Add 'Honeycomb'";
        }
        else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 34)       //pumpkin
        {
            if (Input.GetKey(KeyCode.F))
            {
                colliders[0].GetComponent<Item_035>().quantityToAdd = 1;
                colliders[0].GetComponent<Item_035>().itemCodeToAdd = 34;

                FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15]--;
                if (FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] == 0)
                {
                    FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] = 0;
                    FindObjectOfType<Handing_Item>().SelectedItemCode = 0;
                }
            }

            FindObjectOfType<Buttons>().AddButton.SetActive(true);
            FindObjectOfType<Buttons>().AddButton.transform.Find("Item_To_Add_Name").GetComponent<TextMeshProUGUI>().text = "Add 'Pumpkin'";
        }

        
    }

}
