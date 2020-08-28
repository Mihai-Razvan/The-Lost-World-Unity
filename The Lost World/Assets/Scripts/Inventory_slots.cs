using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class Inventory_slots : MonoBehaviour, IPointerDownHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int itemCode;               //codu itemului din  slot
    [SerializeField]
    public int Slot_Number;          //numaru slotului in inventar
    public int itemQuantity;

    void Start()
    {
        transform.Find("Item_Image").gameObject.SetActive(false);

        if (Slot_Number > 15)
            transform.Find("Highlight").gameObject.SetActive(false);
    }


    void Update()
    {
        
        if(FindObjectOfType<Handing_Item>().SelectedItemBarSlot == Slot_Number - 15 && Slot_Number > 15)
            transform.Find("Highlight").gameObject.SetActive(true);
        else
            transform.Find("Highlight").gameObject.SetActive(false);
        
        itemQuantity = FindObjectOfType<Inventory>().Slot_Item_Quantity[Slot_Number];
        itemCode = FindObjectOfType<Inventory>().Slot_Item_Code[Slot_Number];
        


        if (itemCode != 0 && itemQuantity != 0)
        {
            transform.Find("Item_Image").gameObject.SetActive(true);
            transform.Find("Item_Image").GetComponent<Image>().sprite = FindObjectOfType<List_Of_Items>().Inventory_Sprite[itemCode];

            transform.Find("Quantity").gameObject.SetActive(true);
            transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = itemQuantity.ToString();

        }
        else
        {
            transform.Find("Item_Image").gameObject.SetActive(false);
            transform.Find("Quantity").gameObject.SetActive(false);
        }



    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");

        if (FindObjectOfType<Inventory>().inventory_craftingIsActive == true)
        {
            FindObjectOfType<Inventory>().Initial_Slot_Gameobject = this.gameObject;

            FindObjectOfType<Inventory>().InitialSlotNumberDrag = Slot_Number;
            FindObjectOfType<Inventory>().InitialSlotItemCodeDrag = itemCode;
            FindObjectOfType<Inventory>().InitialSlotQuantityDrag = itemQuantity;
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
    }


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        if (Slot_Number != 0 || (Slot_Number == 0 && FindObjectOfType<Inventory>().InitialSlotItemCodeDrag == 7))
        if (FindObjectOfType<Inventory>().inventory_craftingIsActive == true)
        {
            if (FindObjectOfType<Inventory>().Initial_Slot_Gameobject.tag == "Inventory_Slot")
            {
                    if (FindObjectOfType<Inventory>().InitialSlotItemCodeDrag != itemCode || (FindObjectOfType<Inventory>().InitialSlotItemCodeDrag == itemCode && (FindObjectOfType<Inventory>().InitialSlotQuantityDrag == FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode] || itemQuantity == FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode])))  //swapu normal
                    {
                        FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = itemCode;
                        FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = itemQuantity;

                        FindObjectOfType<Inventory>().Slot_Item_Code[Slot_Number] = FindObjectOfType<Inventory>().InitialSlotItemCodeDrag;
                        FindObjectOfType<Inventory>().Slot_Item_Quantity[Slot_Number] = FindObjectOfType<Inventory>().InitialSlotQuantityDrag;
                    }
                    else     //daca sunt aceleasi iteme aduna la slotu pe care lasi ce pui si daca depaseste restu ramana in al vechi
                    {
                        if (itemQuantity + FindObjectOfType<Inventory>().InitialSlotQuantityDrag <= FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode])
                        {
                            FindObjectOfType<Inventory>().Slot_Item_Quantity[Slot_Number] += FindObjectOfType<Inventory>().InitialSlotQuantityDrag;
                            FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = 0;
                            FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = 0;
                        }
                        else
                        {
                            FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] -= FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode] - itemQuantity; //scade cat se pune in asta de ai dat drop
                            FindObjectOfType<Inventory>().Slot_Item_Quantity[Slot_Number] = FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode];
                        }

                    }
            }
            else if (FindObjectOfType<Inventory>().Initial_Slot_Gameobject.tag == "Furnace_Inventory_Slot")
            {
              

                    if (FindObjectOfType<Inventory>().InitialSlotItemCodeDrag != itemCode || (FindObjectOfType<Inventory>().InitialSlotItemCodeDrag == itemCode && (FindObjectOfType<Inventory>().InitialSlotQuantityDrag == FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode] || itemQuantity == FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode])))  //swapu normal
                    {
                        FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Code[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = itemCode;
                        FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = itemQuantity;

                        FindObjectOfType<Inventory>().Slot_Item_Code[Slot_Number] = FindObjectOfType<Inventory>().InitialSlotItemCodeDrag;
                        FindObjectOfType<Inventory>().Slot_Item_Quantity[Slot_Number] = FindObjectOfType<Inventory>().InitialSlotQuantityDrag;
                    }
                    else     //daca sunt aceleasi iteme aduna la slotu pe care lasi ce pui si daca depaseste restu ramana in al vechi
                    {
                        if (itemQuantity + FindObjectOfType<Inventory>().InitialSlotQuantityDrag <= FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode])
                        {
                            FindObjectOfType<Inventory>().Slot_Item_Quantity[Slot_Number] += FindObjectOfType<Inventory>().InitialSlotQuantityDrag;
                            FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Code[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = 0;
                            FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = 0;
                        }
                        else
                        {
                            FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] -= FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode] - itemQuantity; //scade cat se pune in asta de ai dat drop
                            FindObjectOfType<Inventory>().Slot_Item_Quantity[Slot_Number] = FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode];
                        }

                    }

                }

            

            FindObjectOfType<Inventory>().InitialSlotNumberDrag = -1;
            FindObjectOfType<Inventory>().InitialSlotItemCodeDrag = -1;
            FindObjectOfType<Inventory>().InitialSlotQuantityDrag = -1;
            FindObjectOfType<Inventory>().Initial_Slot_Gameobject = null;
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<Inventory>().itemCodeHovered = itemCode;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<Inventory>().itemCodeHovered = 0;
    }

}
