using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Drop_Items : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        if (FindObjectOfType<Inventory>().inventory_craftingIsActive == true)
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            
            m_Raycaster.Raycast(m_PointerEventData, results);

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (results.Count == 0)
                {
                    Drop();
                }
                else
                {
                    FindObjectOfType<Inventory>().InitialSlotNumberDrag = 0;
                    FindObjectOfType<Inventory>().InitialSlotItemCodeDrag = 0;
                    FindObjectOfType<Inventory>().InitialSlotQuantityDrag = 0;
                    FindObjectOfType<Inventory>().Initial_Slot_Gameobject = null;
                }
            }
        }
    }


    void Drop()
    {
        if (FindObjectOfType<Inventory>().Initial_Slot_Gameobject != null && FindObjectOfType<Inventory>().InitialSlotQuantityDrag > 0)
        {
            if (FindObjectOfType<Inventory>().Initial_Slot_Gameobject.tag == "Inventory_Slot")            //drop la iteme din inventory
            {
                FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = 0;
                FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = 0;
            }
            else if (FindObjectOfType<Inventory>().Initial_Slot_Gameobject.tag == "Furnace_Inventory_Slot")
            {
                FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Code[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = 0;
                FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = 0;
            }
            else if (FindObjectOfType<Inventory>().Initial_Slot_Gameobject.tag == "Chest_Inventory_Slot")
            {
                FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_030>().Slot_Item_Code[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = 0;
                FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_030>().Slot_Item_Quantity[FindObjectOfType<Inventory>().InitialSlotNumberDrag] = 0;
            }

            GameObject spawnedBox = Instantiate(FindObjectOfType<Inventory>().drop_box, FindObjectOfType<Inventory>().player.transform.position + FindObjectOfType<Inventory>().player.transform.forward.normalized * 2, Quaternion.identity);
            spawnedBox.GetComponent<Dropped_Box>().itemCode = FindObjectOfType<Inventory>().InitialSlotItemCodeDrag;
            spawnedBox.GetComponent<Dropped_Box>().itemQuantity = FindObjectOfType<Inventory>().InitialSlotQuantityDrag;
        }    

        FindObjectOfType<Inventory>().InitialSlotNumberDrag = 0;
        FindObjectOfType<Inventory>().InitialSlotItemCodeDrag = 0;
        FindObjectOfType<Inventory>().InitialSlotQuantityDrag = 0;
        FindObjectOfType<Inventory>().Initial_Slot_Gameobject = null;

    }
}