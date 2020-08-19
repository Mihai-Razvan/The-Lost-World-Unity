using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place_Building : MonoBehaviour
{
    [SerializeField]
    private GameObject Item_004;
    public GameObject Building_In_Hand;
    [SerializeField]
    public GameObject Building_Spawn_Position;
    [SerializeField]
    private LayerMask Building_placeable_Surface_Mask;          //pe care pot fi puse cladirile
    void Start()
    {
        
    }

   
    void Update()
    {

        if (FindObjectOfType<Handing_Item>().handing_item == false)      //daca nu e cond asta ai mai multe iteme in mana in ac timp
            BuildingSpawn();


        /// pt urmatoarele de plasat codu asta si intu in prefab si selectez o rotatie nu la punct da la obiectu de e pus pe punct in prefab  a i sa fie cu fata la player


        if (FindObjectOfType<Handing_Item>().handing_item == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Building_placeable_Surface_Mask))
                Building_In_Hand.transform.position = new Vector3(Building_In_Hand.transform.position.x, hit.point.y, Building_In_Hand.transform.position.z);

            if (Input.GetKeyDown(KeyCode.Mouse0) && Building_In_Hand.transform.GetChild(0).GetComponent<ColorPlacingChange>().placeable == true)
            {
                PlaceBuilding();
            }

        }
    }

    void BuildingSpawn()     //cand o ai in inventar si selectezi slotu sa apara buildingu si sa l poti muta
    {
        if (FindObjectOfType<Handing_Item>().SelectedItemCode == 4) // place furnace
        {
            RaycastHit hit;
            if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Building_placeable_Surface_Mask))
            {
                FindObjectOfType<Handing_Item>().handing_item = true;
                Building_In_Hand = Instantiate(Item_004, hit.point, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Building_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }

            Building_In_Hand.transform.SetParent(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform);
            Building_In_Hand.transform.localRotation = Quaternion.identity;

        }
    }


    void PlaceBuilding()   // ui ai handing si apesi sa ramana pe pozitie
    {
        FindObjectOfType<Handing_Item>().handing_item = false;

        if (FindObjectOfType<Handing_Item>().SelectedItemCode == 4)  //furnace
            Instantiate(Item_004, Building_In_Hand.transform.position, Quaternion.Euler(Building_In_Hand.transform.rotation.x, Building_In_Hand.transform.eulerAngles.y, Building_In_Hand.transform.rotation.z));
      


        Destroy(Building_In_Hand.gameObject);

        FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15]--;   //ai plasat cladirea o scoate din inventar
        if (FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] == 0)
            FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] = -1;
    }


}
