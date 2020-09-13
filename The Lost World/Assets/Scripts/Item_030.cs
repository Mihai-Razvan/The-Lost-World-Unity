using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_030 : MonoBehaviour
{
    public bool BuildingAccessed;
    public int[] Slot_Item_Code;             //codu itemului din fiecare slot din inventar
    public int[] Slot_Item_Quantity;          //cantitatea
    private int quantityToAdd;
    private int itemCodeToAdd;
}
