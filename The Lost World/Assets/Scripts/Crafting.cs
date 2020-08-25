using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Crafting : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Sprite Object_Sprite;
    [SerializeField]
    private int[] itemCode;     //codu la itemele de trebuie olosite ca sa cratezi pe asta
    [SerializeField]
    private int[] itemQuantity;           //cantitatea la itemele de trebuie olosite ca sa cratezi pe asta
    private float timeBetweenClicks;           //ca da crat la double click
    private bool alreadyFirstClick;
    private int thisItemQuantity;
    [SerializeField]
    private bool allItemsRequired;
    private int quantityAlreadyTaken;           //cat ti a luat deja din inventar 

    ///  pt ca cand dai sa craftezi daca ai materialele pe slotu 1 ce a craftat pune pe 2 si mutam cratatu la urm frame sa se execute si scriptu Inventory/////

    private bool Has_To_Craft;
    private bool Frame_To_Craft;         // asta pt ca update e dupa onpointerdown si nu rezolva nmk asa ca tre facut in updateu din frameu urmator
    [SerializeField]
    private int Item_Code_Has_To_Craft;
    
    ///////////// astea pt asta sunt folosite ////////


    void Start()
    {
        transform.Find("Item_Image").GetComponent<Image>().sprite = Object_Sprite;
    }

   
    void Update()
    {
        if(Has_To_Craft == true)
        {
            Has_To_Craft = false;
            Frame_To_Craft = true;
        }
        else if(Frame_To_Craft ==  true)
        {
            Frame_To_Craft = false;
      
            FindObjectOfType<Inventory>().quantityToAdd = 1;
            FindObjectOfType<Inventory>().itemCodeToAdd = Item_Code_Has_To_Craft;      
        }
    }


    void ItemsToCraft()
    {
        allItemsRequired = true;             //pp ca ai toate itemele

        for (int i = 1; i < itemCode.Length; i ++)
        {
            thisItemQuantity = 0;
            for (int j = 1; j <= 24; j++)
                if (FindObjectOfType<Inventory>().Slot_Item_Code[j] == itemCode[i])
                    thisItemQuantity += FindObjectOfType<Inventory>().Slot_Item_Quantity[j];

            if(thisItemQuantity < itemQuantity[i])
            {
                allItemsRequired = false;
                break;
            }
        }

        if (allItemsRequired == true)
        {        
            for (int i = 1; i < itemCode.Length; i++)
            {
                quantityAlreadyTaken = 0;

                for (int j = 1; j <= 24 && quantityAlreadyTaken < itemQuantity[i]; j++)
                    if (FindObjectOfType<Inventory>().Slot_Item_Code[j] == itemCode[i] && FindObjectOfType<Inventory>().Slot_Item_Quantity[j] < FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode[i]])
                    {
                        if (FindObjectOfType<Inventory>().Slot_Item_Quantity[j] > itemQuantity[i] - quantityAlreadyTaken)
                        {
                            FindObjectOfType<Inventory>().Slot_Item_Quantity[j] -= itemQuantity[i] - quantityAlreadyTaken;
                            quantityAlreadyTaken = itemQuantity[i];
                        }
                        else
                        {
                            quantityAlreadyTaken += FindObjectOfType<Inventory>().Slot_Item_Quantity[j];
                            FindObjectOfType<Inventory>().Slot_Item_Quantity[j] = 0;
                        }
                    }

                for (int j = 1; j <= 24 && quantityAlreadyTaken < itemQuantity[i]; j++)
                    if (FindObjectOfType<Inventory>().Slot_Item_Code[j] == itemCode[i] && FindObjectOfType<Inventory>().Slot_Item_Quantity[j] == FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode[i]])
                    {
                        if (FindObjectOfType<Inventory>().Slot_Item_Quantity[j] > itemQuantity[i] - quantityAlreadyTaken)
                        {
                            FindObjectOfType<Inventory>().Slot_Item_Quantity[j] -= itemQuantity[i] - quantityAlreadyTaken;
                            quantityAlreadyTaken = itemQuantity[i];
                        }
                        else
                        {
                            quantityAlreadyTaken += FindObjectOfType<Inventory>().Slot_Item_Quantity[j];
                            FindObjectOfType<Inventory>().Slot_Item_Quantity[j] = 0;
                        }
                    }
            }

            Has_To_Craft = true;
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ItemsToCraft();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<Inventory>().itemCodeHovered = Item_Code_Has_To_Craft;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<Inventory>().itemCodeHovered = 0;
    }
}
