using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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

    [SerializeField]
    public GameObject drop_box;

    // items panels //
    [SerializeField]
    public GameObject Item_004_Inventory_Panel;
    [SerializeField]
    public GameObject Item_030_Inventory_Panel;
    [SerializeField]
    public GameObject Item_035_Crafting_Panel;         //il pun aici si nu pe scriptu itemului 35 pt ca nu pot atribui la preffab panelu din canvas  
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
            for (int i = 1; i <= 15; i++)
                if (Slot_Item_Quantity[i] == 0)
                    Slot_Item_Code[i] = 0;

            if (FindObjectOfType<Place_Building>().Has_Building_In_Hand == true)
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);
            else if (FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand == true)
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);

            FindObjectOfType<Handing_Item>().handing_placeable = false;
            FindObjectOfType<Handing_Item>().SelectedItemBarSlot = 0;
            FindObjectOfType<Handing_Item>().SelectedItemCode = 0;

            if (InitialSlotItemCodeDrag != 0)
            {
                ImageOnMouse.gameObject.SetActive(true);
                ImageOnMouse.transform.position = Input.mousePosition;
                ImageOnMouse.GetComponent<Image>().sprite = FindObjectOfType<List_Of_Items>().Inventory_Sprite[InitialSlotItemCodeDrag];
            }
            else
                ImageOnMouse.gameObject.SetActive(false);
        }



        if (inventory_craftingIsActive == true || FindObjectOfType<Game_Menu>().game_menu_isactive == true || FindObjectOfType<Acces_Building>().Building_Menu_Opened == true)
        {
            Cursor.visible = true;
            FindObjectOfType<PlayerMovement>().MovementFrozen = true;
        }
        else
        {
            Cursor.visible = false;
            FindObjectOfType<PlayerMovement>().MovementFrozen = false;
        }

            Center_Dot.SetActive(!inventory_craftingIsActive);
    }
    

    void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && FindObjectOfType<Acces_Building>().Building_Inventory_Opened == false && FindObjectOfType<Acces_Building>().Building_Menu_Opened == false && FindObjectOfType<Game_Menu>().game_menu_isactive == false)
        {
            if (inventory_craftingIsActive == false)
            {
                Inventory_Crafting_Panel.SetActive(true);
                inventory_craftingIsActive = true;

                inventoryIsActive = true;

                Inventory_Panel.SetActive(true);
                Crafting_Panel.SetActive(false);             
            }
            else
            {
                Inventory_Crafting_Panel.SetActive(false);
                inventory_craftingIsActive = false;

                InitialSlotNumberDrag = 0;
                InitialSlotItemCodeDrag = 0;
                InitialSlotQuantityDrag = 0;
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

        if(quantityToAdd != 0)       //inventaru e full sau nu chiar full da trebuie facut slot nou pt a adauga ce trebuie si nu mai sunt sloturi libere
        {
            GameObject spawnedBox = Instantiate(drop_box, player.transform.position, Quaternion.identity);
            spawnedBox.GetComponent<Dropped_Box>().itemCode = itemCodeToAdd;
            spawnedBox.GetComponent<Dropped_Box>().itemQuantity = quantityToAdd;

            itemCodeToAdd = 0;
            quantityToAdd = 0;
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
