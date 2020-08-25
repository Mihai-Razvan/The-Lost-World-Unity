using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Show_Item_Crafting : MonoBehaviour
{
    [SerializeField]
    private GameObject[] requiredItemSlot;     //sloturile alea mici
    private float thisItemQuantity;
    [SerializeField]
    private Color enoughtResourceColor;
    [SerializeField]
    private Color NotenoughtResourceColor;

    void Start()
    {
        transform.Find("Item_Image").gameObject.SetActive(false);
        for (int i = 1; i <= requiredItemSlot.Length; i++)
            requiredItemSlot[i].transform.Find("Item_Image").gameObject.SetActive(false);
    }

   
    void Update()
    {
        if (FindObjectOfType<Inventory>().itemCodeHovered > 0)
        {
            transform.Find("Item_Image").gameObject.SetActive(true);
            transform.Find("Item_Image").GetComponent<Image>().sprite = FindObjectOfType<List_Of_Items>().Inventory_Sprite[FindObjectOfType<Inventory>().itemCodeHovered];

            transform.Find("Item_Name").gameObject.SetActive(true);
            transform.Find("Item_Name").GetComponent<TextMeshProUGUI>().text = FindObjectOfType<List_Of_Items>().Item_Name[FindObjectOfType<Inventory>().itemCodeHovered];

            transform.Find("Item_Description").gameObject.SetActive(true);
            transform.Find("Item_Description").GetComponent<TextMeshProUGUI>().text = FindObjectOfType<List_Of_Items>().Item_Description[FindObjectOfType<Inventory>().itemCodeHovered];

            for (int i = 1; i <= FindObjectOfType<Inventory>().craftingSlotHovered.GetComponent<Crafting>().itemCode.Length; i++)
            {
                requiredItemSlot[i].transform.Find("Item_Image").GetComponent<Image>().sprite = FindObjectOfType<List_Of_Items>().Inventory_Sprite[FindObjectOfType<Inventory>().craftingSlotHovered.GetComponent<Crafting>().itemCode[i]];
                requiredItemSlot[i].transform.Find("Item_Image").gameObject.SetActive(true);

                thisItemQuantity = 0;
                for (int j = 1; j <= 24; j++)
                     if (FindObjectOfType<Inventory>().Slot_Item_Code[j] == FindObjectOfType<Inventory>().craftingSlotHovered.GetComponent<Crafting>().itemCode[i])
                            thisItemQuantity += FindObjectOfType<Inventory>().Slot_Item_Quantity[j];


                requiredItemSlot[i].transform.Find("Quantity_Required").gameObject.SetActive(true);
                requiredItemSlot[i].transform.Find("Bar").gameObject.SetActive(true);
                requiredItemSlot[i].transform.Find("Quantity_Have").gameObject.SetActive(true);

                requiredItemSlot[i].transform.Find("Quantity_Required").GetComponent<TextMeshProUGUI>().text = FindObjectOfType<Inventory>().craftingSlotHovered.GetComponent<Crafting>().itemQuantity[i].ToString();
                requiredItemSlot[i].transform.Find("Quantity_Have").GetComponent<TextMeshProUGUI>().text = thisItemQuantity.ToString();

                if (thisItemQuantity >= FindObjectOfType<Inventory>().craftingSlotHovered.GetComponent<Crafting>().itemQuantity[i])
                    requiredItemSlot[i].transform.Find("Quantity_Have").GetComponent<TextMeshProUGUI>().color = enoughtResourceColor;
                else
                    requiredItemSlot[i].transform.Find("Quantity_Have").GetComponent<TextMeshProUGUI>().color = NotenoughtResourceColor;
            }
        }
        else
        {
            transform.Find("Item_Image").gameObject.SetActive(false);
            transform.Find("Item_Name").gameObject.SetActive(false);
            transform.Find("Item_Description").gameObject.SetActive(false);

            for (int i = 1; i <= requiredItemSlot.Length; i++)
            {
                requiredItemSlot[i].transform.Find("Item_Image").gameObject.SetActive(false);

                requiredItemSlot[i].transform.Find("Quantity_Required").gameObject.SetActive(false);
                requiredItemSlot[i].transform.Find("Bar").gameObject.SetActive(false);
                requiredItemSlot[i].transform.Find("Quantity_Have").gameObject.SetActive(false);
            }
        }
    }



    void ItemQuantityInInventory()
    {

    }
}
