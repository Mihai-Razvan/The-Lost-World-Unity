using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Handing_Item : MonoBehaviour
{                                               /// itemel din mana intra aci si placementu de buildinguri ////

    /// </summary>
    [SerializeField]
    private int SelectedItemBarSlot;
    private int SelectedItemCode;
    public bool handing_item;
    [SerializeField]
    private GameObject Building_Spawn_Position;
    [SerializeField]
    private LayerMask Building_placeable_Surface_Mask;    //pe care pot ffi puse cladirile
    [SerializeField]
    private LayerMask Floor_placeable_Surface_Mask;       //pt pe care poate ffi pusa floor asta nu poate ffi pusa pe alt floor 
    private GameObject Object_In_Hand;
    public RaycastHit hit;
  

    //pt scriptu  ColorPlacingChange ca acolo nu pot atrinui
    public Material OkMaterial;
    [SerializeField]
    public Material CollidingMaterial;
    //


    /*
    private float harvestCooldown;
    private bool harvestFirstHitWasAlready;
    private float pickaxeAnimationCooldown;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private LayerMask pickaxeHarvestableMask;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject pickaxe;
    */


    ///// Items to handle (tools,weapons) or buildings to plac  \\\\\\

    [SerializeField]
    private GameObject Item_004;    //furnace
    [SerializeField]
    private GameObject Item_008;    //wooden floor

    void Start()
    {

    }


    void Update()
    {
        SelectItemSlot();

        if (handing_item == false)    //daca nu e cond asta ai mai multe iteme in mana in ac timp
            BuildingSpawn();
        
        /// pt urmatoarele de plasat codu asta si intu in prefab si selectez o rotatie nu la punct da la obiectu de e pus pe punct in prefab  a i sa fie cu fata la player

        if (handing_item == true)
        {
          
            if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Building_placeable_Surface_Mask))
                Object_In_Hand.transform.position = new Vector3(Object_In_Hand.transform.position.x, hit.point.y, Object_In_Hand.transform.position.z);

            if (Input.GetKeyDown(KeyCode.Mouse0) && Object_In_Hand.transform.GetChild(0).GetComponent<ColorPlacingChange>().placeable == true)
            {
                PlaceBuilding();
            }
            
        }

    }

    void BuildingSpawn()     //cand o ai in inventar si selectezi slotu sa apara buildingu si sa l poti muta
    {
        if (SelectedItemCode == 4) // place furnace
        {
            RaycastHit hit;
            if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Building_placeable_Surface_Mask))
            {
                handing_item = true;
                Object_In_Hand = Instantiate(Item_004, hit.point, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Object_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }

            Object_In_Hand.transform.SetParent(Building_Spawn_Position.transform);
            Object_In_Hand.transform.localRotation = Quaternion.identity;

        }
        else if (SelectedItemCode == 8)  // place wooden floor
        {
            RaycastHit hit;
            if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Floor_placeable_Surface_Mask))
            {
                handing_item = true;
                Object_In_Hand = Instantiate(Item_008, hit.point, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Object_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }

            Object_In_Hand.transform.SetParent(Building_Spawn_Position.transform);
            Object_In_Hand.transform.localRotation = Quaternion.identity;
        }
    }


    void PlaceBuilding()   // ui ai handing si apesi sa ramana pe pozitie
    {
        handing_item = false;

        if(SelectedItemCode == 4)  //furnace
            Instantiate(Item_004, Object_In_Hand.transform.position, Quaternion.Euler(Object_In_Hand.transform.rotation.x, Object_In_Hand.transform.eulerAngles.y, Object_In_Hand.transform.rotation.z));
        else if (SelectedItemCode == 8) //wooden floor
            Instantiate(Item_008, Object_In_Hand.transform.position, Quaternion.Euler(Object_In_Hand.transform.rotation.x, Object_In_Hand.transform.eulerAngles.y, Object_In_Hand.transform.rotation.z));


        Destroy(Object_In_Hand.gameObject);

        FindObjectOfType<Inventory>().Slot_Item_Quantity[SelectedItemBarSlot + 15]--;   //ai plasat cladirea o scoate din inventar

    }


    void SelectItemSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {   
            if(handing_item == true)               //daca nu scriu asta apare un bug de incurca unityu
               Destroy(Object_In_Hand.gameObject);  //daca ai o cladire in mana si schimbi pe alt slot sau tot pe ala pe care esti sa nu mai ai cladirea in mana

            SelectedItemBarSlot = 1;
            handing_item = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        { 
            if(handing_item == true)
               Destroy(Object_In_Hand.gameObject);  

            SelectedItemBarSlot = 2;
            handing_item = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (handing_item == true)
                Destroy(Object_In_Hand.gameObject);

            SelectedItemBarSlot = 3;
            handing_item = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (handing_item == true)
                Destroy(Object_In_Hand.gameObject);

            SelectedItemBarSlot = 4;
            handing_item = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (handing_item == true)
                Destroy(Object_In_Hand.gameObject);

            SelectedItemBarSlot = 5;
            handing_item = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (handing_item == true)
                Destroy(Object_In_Hand.gameObject);

            SelectedItemBarSlot = 6;
            handing_item = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (handing_item == true)
                Destroy(Object_In_Hand.gameObject);

            SelectedItemBarSlot = 7;
            handing_item = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (handing_item == true)
                Destroy(Object_In_Hand.gameObject);

            SelectedItemBarSlot = 8;
            handing_item = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (handing_item == true)
                Destroy(Object_In_Hand.gameObject);

            SelectedItemBarSlot = 9;
            handing_item = false;
        }
        else if (Input.GetKeyDown(KeyCode.X))    // daca ai ceva in mana sa nu mai ai
        {
            if (handing_item == true)
                Destroy(Object_In_Hand.gameObject);

            SelectedItemCode = -1;
            SelectedItemBarSlot = -16;
            handing_item = false;           
        }

        SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[SelectedItemBarSlot + 15];
    }

}

