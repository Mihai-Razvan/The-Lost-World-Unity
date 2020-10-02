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

              //numa pt special slot ca astea sunt numa pt jetpack si momentan jetpacku poate fi numai in special slot
    public int batteryCode;
    [SerializeField]
    public float maxcharge;       //cat ar avea max bateria asta e pt fill
    private float battery_Charge_31 = 30;
    private float battery_Charge_32 = 90;
    private float battery_Charge_33 = 300;
    public float batteryCharge;   //cat mai are
    

    void Start()
    {
        transform.Find("Item_Image").gameObject.SetActive(false);

        if (Slot_Number > 15 && Slot_Number < 25)
            transform.Find("Highlight").gameObject.SetActive(false);

        if (Slot_Number == 25)
        {
            transform.Find("Battery_Image").gameObject.SetActive(false);
            transform.Find("Battery_Backround").gameObject.SetActive(false);
        }
    }


    void Update()
    {
        
        if((FindObjectOfType<Handing_Item>().SelectedItemBarSlot != 0 && FindObjectOfType<Handing_Item>().SelectedItemBarSlot == Slot_Number - 15) && Slot_Number > 15 && Slot_Number < 25)
            transform.Find("Highlight").gameObject.SetActive(true);
        else if(Slot_Number > 15 && Slot_Number < 25)
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

        if (Slot_Number == 25)
            Battery();

    }


    private void Battery()
    {
        if(batteryCharge > 0)
        {
            transform.Find("Battery_Image").gameObject.SetActive(true);
            transform.Find("Battery_Image").GetComponent<Image>().sprite = FindObjectOfType<List_Of_Items>().Inventory_Sprite[batteryCode];
            transform.Find("Battery_Backround").gameObject.SetActive(true);
            transform.Find("Battery_Backround").GetComponent<Image>().sprite = FindObjectOfType<List_Of_Items>().Inventory_Sprite[batteryCode];
            transform.Find("Battery_Image").GetComponent<Image>().fillAmount = batteryCharge / maxcharge; 
        }
        else
        {
            transform.Find("Battery_Image").gameObject.SetActive(false);
            transform.Find("Battery_Backround").gameObject.SetActive(false);
        }
    }

    private void BatteryCharge()
    {
        if (batteryCode == 31)
            batteryCharge = battery_Charge_31;
        else if (batteryCode == 32)
            batteryCharge = battery_Charge_32;
        else if (batteryCode == 33)
            batteryCharge = battery_Charge_33;

        maxcharge = batteryCharge;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");

        if (FindObjectOfType<Inventory>().inventory_craftingIsActive == true && Slot_Number != 25)
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

        if (Slot_Number != 25 || (Slot_Number == 25 && FindObjectOfType<Inventory>().InitialSlotItemCodeDrag == 7))   //nu poti dropa iteme pe special slot

        {
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
                else if (FindObjectOfType<Inventory>().Initial_Slot_Gameobject.tag == "Chest_Inventory_Slot")
                {


                    if (FindObjectOfType<Inventory>().InitialSlotItemCodeDrag != itemCode || (FindObjectOfType<Inventory>().InitialSlotItemCodeDrag == itemCode && (FindObjectOfType<Inventory>().InitialSlotQuantityDrag == FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode] || itemQuantity == FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode])))  //swapu normal
                    {
                        FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_030>().Slot_Item_Code[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = itemCode;
                        FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_030>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = itemQuantity;

                        FindObjectOfType<Inventory>().Slot_Item_Code[Slot_Number] = FindObjectOfType<Inventory>().InitialSlotItemCodeDrag;
                        FindObjectOfType<Inventory>().Slot_Item_Quantity[Slot_Number] = FindObjectOfType<Inventory>().InitialSlotQuantityDrag;
                    }
                    else     //daca sunt aceleasi iteme aduna la slotu pe care lasi ce pui si daca depaseste restu ramana in al vechi
                    {
                        if (itemQuantity + FindObjectOfType<Inventory>().InitialSlotQuantityDrag <= FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode])
                        {
                            FindObjectOfType<Inventory>().Slot_Item_Quantity[Slot_Number] += FindObjectOfType<Inventory>().InitialSlotQuantityDrag;
                            FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_030>().Slot_Item_Code[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = 0;
                            FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_030>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = 0;
                        }
                        else
                        {
                            FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_030>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] -= FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode] - itemQuantity; //scade cat se pune in asta de ai dat drop
                            FindObjectOfType<Inventory>().Slot_Item_Quantity[Slot_Number] = FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode];
                        }

                    }

                }




                FindObjectOfType<Inventory>().InitialSlotNumberDrag = 0;
                FindObjectOfType<Inventory>().InitialSlotItemCodeDrag = 0;
                FindObjectOfType<Inventory>().InitialSlotQuantityDrag = 0;
                FindObjectOfType<Inventory>().Initial_Slot_Gameobject = null;
            }
        }
        else if (Slot_Number == 25 && (FindObjectOfType<Inventory>().InitialSlotItemCodeDrag == 31 || FindObjectOfType<Inventory>().InitialSlotItemCodeDrag == 32 || FindObjectOfType<Inventory>().InitialSlotItemCodeDrag == 33)) //baterii
        {
            if (FindObjectOfType<Inventory>().inventory_craftingIsActive == true)
            {
                if (FindObjectOfType<Inventory>().Initial_Slot_Gameobject.tag == "Inventory_Slot")
                {
                    batteryCode = FindObjectOfType<Inventory>().InitialSlotItemCodeDrag;
                    BatteryCharge();
                    FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag]--;                 
                }
                else if (FindObjectOfType<Inventory>().Initial_Slot_Gameobject.tag == "Furnace_Inventory_Slot")
                {
                    batteryCode = FindObjectOfType<Inventory>().InitialSlotItemCodeDrag;
                    BatteryCharge();
                    FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag]--;
                }
                else if (FindObjectOfType<Inventory>().Initial_Slot_Gameobject.tag == "Chest_Inventory_Slot")
                {
                    batteryCode = FindObjectOfType<Inventory>().InitialSlotItemCodeDrag;
                    BatteryCharge();
                    FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_030>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag]--;
                }




                FindObjectOfType<Inventory>().InitialSlotNumberDrag = 0;
                FindObjectOfType<Inventory>().InitialSlotItemCodeDrag = 0;
                FindObjectOfType<Inventory>().InitialSlotQuantityDrag = 0;
                FindObjectOfType<Inventory>().Initial_Slot_Gameobject = null;
            }
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
