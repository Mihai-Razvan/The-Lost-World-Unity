using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Handing_Item : MonoBehaviour
{                                               /// itemel din mana intra aci si placementu de buildinguri ////

  
    [SerializeField]
    public int SelectedItemBarSlot;
    public int SelectedItemCode;
    public bool handing_item;
    [SerializeField]
    public GameObject Building_Spawn_Position;
    public RaycastHit hit;
  

    //pt scriptu  ColorPlacingChange 
    public Material OkMaterial;
    [SerializeField]
    public Material CollidingMaterial;



    ///// Items to handle (tools,weapons) or buildings to plac  \\\\\\

    [SerializeField]
    private GameObject Item_004;    //furnace
    [SerializeField]
    private GameObject Item_008;    //wooden floor

    void Start()
    {

    }


    void Update()
    {
        SelectItemSlot();
    }


    void SelectItemSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (handing_item == true)               //daca nu scriu asta apare un bug de incurca unityu
            {
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);  //daca ai o cladire in mana si schimbi pe alt slot sau tot pe ala pe care esti sa nu mai ai cladirea in mana
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);
            }

            SelectedItemBarSlot = 1;
            handing_item = false;

            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[16] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[16] = -1;
            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[16];

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (handing_item == true)               
            {
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject); 
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);
            }

            SelectedItemBarSlot = 2;
            handing_item = false;

            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[17] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[17] = -1;
            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[17];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (handing_item == true)
            {
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);
            }

            SelectedItemBarSlot = 3;
            handing_item = false;

            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[18] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[18] = -1;
            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[18];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (handing_item == true)
            {
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);
            }

            SelectedItemBarSlot = 4;
            handing_item = false;

            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[19] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[19] = -1;
            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[19];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (handing_item == true)
            {
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);
            }

            SelectedItemBarSlot = 5;
            handing_item = false;

            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[20] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[20] = -1;
            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[20];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (handing_item == true)
            {
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);
            }

            SelectedItemBarSlot = 6;
            handing_item = false;

            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[21] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[21] = -1;
            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[21];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (handing_item == true)
            {
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);
            }

            SelectedItemBarSlot = 7;
            handing_item = false;

            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[22] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[22] = -1;
            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[2];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (handing_item == true)
            {
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);
            }

                SelectedItemBarSlot = 8;
            handing_item = false;

            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[23] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[23] = -1;
            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[23];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (handing_item == true)
            {
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);
            }

            SelectedItemBarSlot = 9;
            handing_item = false;

            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[24] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[24] = -1;
            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[24];
        }
        else if (Input.GetKeyDown(KeyCode.X))    // daca ai ceva in mana sa nu mai ai
        {
            if (handing_item == true)
            {
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);
            }

            SelectedItemCode = -1;
            SelectedItemBarSlot = -1;
            handing_item = false;           
        }

        
        if (FindObjectOfType<Inventory>().Slot_Item_Quantity[SelectedItemBarSlot + 15] <= 0)
            FindObjectOfType<Inventory>().Slot_Item_Code[SelectedItemBarSlot + 15] = -1;
        SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[SelectedItemBarSlot + 15];
    }

}

