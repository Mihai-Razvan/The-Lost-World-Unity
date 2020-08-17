using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Item_004_Inventory_Slots : MonoBehaviour, IPointerDownHandler, IDragHandler, IDropHandler
{
    public int itemCode;               //codu itemului din  slot
    [SerializeField]
    public int Slot_Number;          //numaru slotului in inventar
    public int itemQuantity;
    void Start()
    {
        transform.Find("Item_Image").gameObject.SetActive(false);
    }

    
    void Update()
    {
        itemQuantity = FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Quantity[Slot_Number];
        itemCode = FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Code[Slot_Number];

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
        
        if (FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().BuildingAccessed == true)
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

        if (FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().BuildingAccessed == true)
        {
            if (FindObjectOfType<Inventory>().Initial_Slot_Gameobject.tag == "Furnace_Inventory_Slot")
            {
                FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Code[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = itemCode;
                FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = itemQuantity;
            }
            else if (FindObjectOfType<Inventory>().Initial_Slot_Gameobject.tag == "Inventory_Slot")
            {
                FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = itemCode;
                FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = itemQuantity;
            }

            FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Code[Slot_Number] = FindObjectOfType<Inventory>().InitialSlotItemCodeDrag;
            FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Quantity[Slot_Number] = FindObjectOfType<Inventory>().InitialSlotQuantityDrag;

            FindObjectOfType<Inventory>().InitialSlotNumberDrag = 0;
            FindObjectOfType<Inventory>().InitialSlotItemCodeDrag = 0;
            FindObjectOfType<Inventory>().InitialSlotQuantityDrag = 0;
        }
    }

}
