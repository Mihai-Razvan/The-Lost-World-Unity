using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Show_Item_Inventory : MonoBehaviour
{
    
    void Start()
    {
        transform.Find("Item_Image").gameObject.SetActive(false);
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
        }
        else
        {
            transform.Find("Item_Image").gameObject.SetActive(false);
            transform.Find("Item_Name").gameObject.SetActive(false);
            transform.Find("Item_Description").gameObject.SetActive(false);
        }
    }
}
