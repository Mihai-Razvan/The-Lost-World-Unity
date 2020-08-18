using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place_Prefab : MonoBehaviour
{
    [SerializeField]
    private GameObject Item_008;    //wooden floor
    public GameObject Prefab_In_Hand;
    [SerializeField]
    public GameObject Building_Spawn_Position;
    [SerializeField]
    private LayerMask Floor_placeable_Surface_Mask;
    void Start()
    {
        
    }

    
    void Update()
    {      

        if (FindObjectOfType<Handing_Item>().handing_item == false)      //daca nu e cond asta ai mai multe iteme in mana in ac timp
            PrefabSpawn();


        if (FindObjectOfType<Handing_Item>().handing_item == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Floor_placeable_Surface_Mask))
                Prefab_In_Hand.transform.position = new Vector3(Prefab_In_Hand.transform.position.x, hit.point.y, Prefab_In_Hand.transform.position.z);

            if (Input.GetKeyDown(KeyCode.Mouse0) && Prefab_In_Hand.transform.GetChild(0).GetComponent<ColorPlacingChange>().placeable == true)
            {
                PlacePrefab();
            }

        }
    }



    void PrefabSpawn()     //cand o ai in inventar si selectezi slotu sa apara buildingu si sa l poti muta
    {
        if (FindObjectOfType<Handing_Item>().SelectedItemCode == 8) // place furnace
        {
            RaycastHit hit;
            if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, FindObjectOfType<Handing_Item>().Building_placeable_Surface_Mask))
            {
                FindObjectOfType<Handing_Item>().handing_item = true;
                Prefab_In_Hand = Instantiate(Item_008, hit.point, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }

            Prefab_In_Hand.transform.SetParent(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform);
            Prefab_In_Hand.transform.localRotation = Quaternion.identity;

        }
    }


    void PlacePrefab()   // ui ai handing si apesi sa ramana pe pozitie
    {
        FindObjectOfType<Handing_Item>().handing_item = false;

        if (FindObjectOfType<Handing_Item>().SelectedItemCode == 8)  //furnace
            Instantiate(Item_008, Prefab_In_Hand.transform.position, Quaternion.Euler(Prefab_In_Hand.transform.rotation.x, Prefab_In_Hand.transform.eulerAngles.y, Prefab_In_Hand.transform.rotation.z));



        Destroy(Prefab_In_Hand.gameObject);

        FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15]--;   //ai plasat cladirea o scoate din inventar
        if (FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] == 0)
            FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] = -1;
    }
}
