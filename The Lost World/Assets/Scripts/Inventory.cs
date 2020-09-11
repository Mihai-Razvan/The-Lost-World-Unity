using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    public GameObject player;
    public GameObject Inventory_Crafting_Panel;                       //ala de are si inventoriu si craftingu
    public GameObject Inventory_Panel;                                  // numa inventoryu
    public GameObject Crafting_Panel;                                   //numa craftingu
    public GameObject Switch_To_Inventory_Buttton;
    public GameObject Switch_To_Crafting_Buttton;
    public bool inventory_craftingIsActive;                            //e activ panelu cu ambele
    public bool inventoryIsActive = true;                                   //numa inventaru
    [SerializeField]
    private GameObject Center_Dot;

    public GameObject Initial_Slot_Gameobject;
    public int InitialSlotNumberDrag;
    public int InitialSlotItemCodeDrag;
    public int InitialSlotQuantityDrag;


    public int[] Slot_Item_Code;             //codu itemului din fiecare slot din inventar
    public int[] Slot_Item_Quantity;          //cantitatea
     
    [SerializeField]
    public int quantityToAdd;
    public int itemCodeToAdd;

    [SerializeField]
    public int SelectedItemBarSlot;             // slotu de ai in mana
    [SerializeField]
    public int SelectedItemCode;
    [SerializeField]
    private Image ImageOnMouse;                //imaginea de e la poz mouseului cand muti un item dintr un slot in altu
    [SerializeField]
    public int itemCodeHovered;
    public GameObject craftingSlotHovered;
    // items panels //
    [SerializeField]
    public GameObject Item_004_Inventory_Panel;
    /// 



    public bool ok;  // pt teste
    void Start()
    {
        Item_004_Inventory_Panel.SetActive(false);
        Inventory_Crafting_Panel.SetActive(false);
        Crafting_Panel.SetActive(false);

        ImageOnMouse.gameObject.SetActive(false);
    }

    
    void Update()
    {
        ToggleInventory();

        AddToInventory();

        if (inventory_craftingIsActive == true)
        {
            Cursor.visible = true;
            for (int i = 0; i <= 15; i++)
                if (Slot_Item_Quantity[i] == 0)
                    Slot_Item_Code[i] = 0;

            Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);
            Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);
            FindObjectOfType<Handing_Item>().handing_placeable = false;
            FindObjectOfType<Handing_Item>().SelectedItemBarSlot = -1;
            FindObjectOfType<Handing_Item>().SelectedItemCode = -1;

            if (InitialSlotItemCodeDrag > 0)
            {
                ImageOnMouse.gameObject.SetActive(true);
                ImageOnMouse.transform.position = Input.mousePosition;
                ImageOnMouse.GetComponent<Image>().sprite = FindObjectOfType<List_Of_Items>().Inventory_Sprite[InitialSlotItemCodeDrag];
            }
            else
                ImageOnMouse.gameObject.SetActive(false);

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (Initial_Slot_Gameobject.tag == "Inventory_Slot")            //drop la iteme din inventory
                {
                    Slot_Item_Code[InitialSlotNumberDrag] = 0;
                    Slot_Item_Quantity[InitialSlotNumberDrag] = 0;
                }
                else if (Initial_Slot_Gameobject.tag == "Furnace_Inventory_Slot")
                {
                    FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Code[InitialSlotNumberDrag] = 0;
                    FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Quantity[InitialSlotNumberDrag] = 0;
                }

                InitialSlotNumberDrag = -1;
                InitialSlotItemCodeDrag = -1;
                InitialSlotQuantityDrag = -1;
                Initial_Slot_Gameobject = null;

            }
        }
        else
            Cursor.visible = false;
        

       /* for (int i = 16; i <= 24; i++)
            if (Slot_Item_Quantity[i] == 0)
                Slot_Item_Code[i] = 0;
                */
        

        Center_Dot.SetActive(!inventory_craftingIsActive);
       // Cursor.visible = inventory_craftingIsActive;
    }
    

    void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && FindObjectOfType<Acces_Building>().Building_Inventory_Opened == false)
        {
            if (inventory_craftingIsActive == false)
            {
                Inventory_Crafting_Panel.SetActive(true);
                inventory_craftingIsActive = true;

                Inventory_Panel.SetActive(true);
                Crafting_Panel.SetActive(false);

                FindObjectOfType<PlayerMovement>().MovementFrozen = true;
            }
            else
            {
                Inventory_Crafting_Panel.SetActive(false);
                inventory_craftingIsActive = false;

                InitialSlotNumberDrag = -1;
                InitialSlotItemCodeDrag = -1;
                InitialSlotQuantityDrag = -1;

                FindObjectOfType<PlayerMovement>().MovementFrozen = false;

            }
        }
    }

   

    public void AddToInventory()
    {
        if (quantityToAdd == 0)
            itemCodeToAdd = 0;

        for (int i = 1; i <= 24 && quantityToAdd != 0; i++)
            if (Slot_Item_Code[i] == itemCodeToAdd)
            {
                if (Slot_Item_Quantity[i] + quantityToAdd <= FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCodeToAdd])
                {
                    Slot_Item_Quantity[i] += quantityToAdd;
                    quantityToAdd = 0;
                }
                else
                {
                    quantityToAdd -= FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCodeToAdd] - Slot_Item_Quantity[i];
                    Slot_Item_Quantity[i] = FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCodeToAdd];
                }

            }


        if (quantityToAdd != 0)                                                                                      //s-a adaugat  in  sloturile unde eradeja  acum cauta un slot   gol
            for (int i = 1; i <= 24 && quantityToAdd != 0; i++)
            {
                if (Slot_Item_Code[i] == 0)                                                                          //SLOTU E GOL
                {
                    Slot_Item_Code[i] = itemCodeToAdd;

                    if (quantityToAdd <= FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCodeToAdd])
                    {
                        Slot_Item_Quantity[i] = quantityToAdd;
                        quantityToAdd = 0;
                    }
                    else
                    {
                        Slot_Item_Quantity[i] = FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCodeToAdd];
                        quantityToAdd -= Slot_Item_Quantity[i];
                    }
                }
            }


    }

    public void ChangeToCraftingButton()
    {
        if (inventoryIsActive)
        {
            Inventory_Panel.SetActive(false);
            Crafting_Panel.SetActive(true);
            inventoryIsActive = false;
        }
        
    }

    public void ChangeToInventoryButton()
    {
        if (inventoryIsActive == false)   //inseamna ca e active craftingu
        {
            Crafting_Panel.SetActive(false);
            Inventory_Panel.SetActive(true);
            inventoryIsActive = true;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
       
        Debug.Log("OnPointerDown");
        InitialSlotNumberDrag = eventData.pointerPress.GetComponent<Inventory_slots>().Slot_Number;
        InitialSlotItemCodeDrag = eventData.pointerPress.GetComponent<Inventory_slots>().itemCode;
        InitialSlotQuantityDrag = eventData.pointerPress.GetComponent<Inventory_slots>().itemQuantity;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        Debug.Log("OnBeginDrag");
        InitialSlotNumberDrag = eventData.pointerPress.GetComponent<Inventory_slots>().Slot_Number;
        InitialSlotItemCodeDrag = eventData.pointerPress.GetComponent<Inventory_slots>().itemCode;
        InitialSlotQuantityDrag = eventData.pointerPress.GetComponent<Inventory_slots>().itemQuantity;
    }

    public void OnDrag(PointerEventData eventData)
    {
        

        Debug.Log("OnDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        Debug.Log("OnEndDrag");
        Slot_Item_Code[InitialSlotNumberDrag] = eventData.pointerPress.GetComponent<Inventory_slots>().itemCode;
        Slot_Item_Quantity[InitialSlotNumberDrag] = eventData.pointerPress.GetComponent<Inventory_slots>().itemQuantity;

        eventData.pointerPress.GetComponent<Inventory_slots>().itemCode = InitialSlotItemCodeDrag;
        eventData.pointerPress.GetComponent<Inventory_slots>().itemQuantity = InitialSlotQuantityDrag;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    

}
