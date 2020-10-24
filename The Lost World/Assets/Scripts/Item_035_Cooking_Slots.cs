﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Item_035_Cooking_Slots : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Sprite Object_Sprite;
    [SerializeField]
    public int[] itemCode;     //codu la itemele de trebuie olosite ca sa cratezi pe asta
    [SerializeField]
    public int[] itemQuantity;           //cantitatea la itemele de trebuie olosite ca sa cratezi pe asta
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
        if (Has_To_Craft == true)
        {
            Has_To_Craft = false;
            Frame_To_Craft = true;
        }
        else if (Frame_To_Craft == true)
        {
            Frame_To_Craft = false;

            FindObjectOfType<Inventory>().quantityToAdd = 1;
            FindObjectOfType<Inventory>().itemCodeToAdd = Item_Code_Has_To_Craft;
        }
    }


    void ItemsToCraft()
    {
        allItemsRequired = true;             //pp ca ai toate itemele

        for (int i = 1; i < itemCode.Length; i++)
        {
            thisItemQuantity = 0;
            for (int j = 1; j <= 5; j++)
                if (FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_code[j] == itemCode[i])
                    thisItemQuantity += FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j];

            if (thisItemQuantity < itemQuantity[i])
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

                for (int j = 1; j <= 5 && quantityAlreadyTaken < itemQuantity[i]; j++)
                    if (FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_code[j] == itemCode[i] && FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j] < FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode[i]])
                    {
                        if (FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j] > itemQuantity[i] - quantityAlreadyTaken)
                        {
                            FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j] -= itemQuantity[i] - quantityAlreadyTaken;
                            quantityAlreadyTaken = itemQuantity[i];
                        }
                        else
                        {
                            quantityAlreadyTaken += FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j];
                            FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j] = 0;
                        }
                    }

                for (int j = 1; j <= 5 && quantityAlreadyTaken < itemQuantity[i]; j++)
                    if (FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_code[j] == itemCode[i] && FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j] == FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCode[i]])
                    {
                        if (FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j] > itemQuantity[i] - quantityAlreadyTaken)
                        {
                            FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j] -= itemQuantity[i] - quantityAlreadyTaken;
                            quantityAlreadyTaken = itemQuantity[i];
                        }
                        else
                        {
                            quantityAlreadyTaken += FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j];
                            FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j] = 0;
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
        FindObjectOfType<Inventory>().craftingSlotHovered = this.gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<Inventory>().itemCodeHovered = 0;
        FindObjectOfType<Inventory>().craftingSlotHovered = null;
    }
}
