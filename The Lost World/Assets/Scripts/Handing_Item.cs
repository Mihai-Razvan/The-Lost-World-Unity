using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handing_Item : MonoBehaviour
{                                               /// itemel din mana intra aci si placementu de buildinguri ////

    /// </summary>
    private int SelectedItemBarSlot;
    private int SelectedItemCode;
    public bool handing_item;
    [SerializeField]
    private GameObject Building_Spawn_Position;
    [SerializeField]
    private LayerMask placeable_Surface_Mask;
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

    void Start()
    {

    }


    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectedItemBarSlot = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectedItemBarSlot = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectedItemBarSlot = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectedItemBarSlot = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectedItemBarSlot = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            SelectedItemBarSlot = 5;
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            SelectedItemBarSlot = 6;
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            SelectedItemBarSlot = 7;
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            SelectedItemBarSlot = 8;
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            SelectedItemBarSlot = 9;
        else if (Input.GetKeyDown(KeyCode.Q))
            SelectedItemBarSlot = 0;                                 // NU AI NICIUN SLOT SELECTAT

        SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[SelectedItemBarSlot + 15];

        if (handing_item == false)
            BuildingSpawn();
        
        /// pt urmatoarele de plasat codu asta si intu in prefab si selectez o rotatie nu la punct da la obiectu de e pus pe punct in prefab  a i sa fie cu fata la player

        if (handing_item == true)
        {
          
            if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, placeable_Surface_Mask))
                Object_In_Hand.transform.position = new Vector3(Object_In_Hand.transform.position.x, hit.point.y, Object_In_Hand.transform.position.z);

            if (Input.GetKeyDown(KeyCode.Mouse0) && Object_In_Hand.transform.GetChild(0).GetComponent<ColorPlacingChange>().placeable == true)
            {
                PlaceBuilding();
            }
            
        }


        /////////////////////////////picku/////////////////////////////

        /*
        pickaxeAnimationCooldown += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetBool("pressButton", false);
            harvestCooldown = 0f;
            harvestFirstHitWasAlready = false;
        }

        
        if (SelectedItemCode == 2)
        {
            pickaxe.SetActive(true);
            if (Input.GetKey(KeyCode.Mouse0)  && animator.GetBool("pressButton") == false )

            {
                animator.SetBool("pressButton", true);
                pickaxeAnimationCooldown = 0;
                harvestCooldown += Time.deltaTime;

                RaycastHit hit;

                if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, 2f, pickaxeHarvestableMask))
                {

                    if (harvestFirstHitWasAlready == false)
                    {
                        if (harvestCooldown >= 0.5f)
                        {
                            harvestFirstHitWasAlready = true;
                            harvestCooldown = 0f;
                            FindObjectOfType<Inventory>().quantityToAdd = 1;

                            if (hit.collider.gameObject.tag == "Tree")
                                FindObjectOfType<Inventory>().itemCodeToAdd = 1;

                            hit.collider.GetComponent<HealthObjects>().health -= 25;
                        }
                    }
                    else
                    {
                        if (harvestCooldown >= 1f)
                        {
                            harvestFirstHitWasAlready = true;
                            harvestCooldown = 0f;
                            FindObjectOfType<Inventory>().quantityToAdd = 1;

                            if (hit.collider.gameObject.tag == "Tree")
                                FindObjectOfType<Inventory>().itemCodeToAdd = 1;

                            hit.collider.GetComponent<HealthObjects>().health -= 25;
                        }
                    }
                }

            }




            //}
            //else
            //pickaxe.SetActive(false);


        }
         */
    }

    void BuildingSpawn()     //cand o ai in inventar si selectezi slotu sa apara buildingu si sa l poti muta
    {
        if (SelectedItemCode == 4) // place furnace
        {
            RaycastHit hit;
            if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, placeable_Surface_Mask))
            {
                handing_item = true;
                Object_In_Hand = Instantiate(Item_004, hit.point, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
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
        Destroy(Object_In_Hand.gameObject);

        FindObjectOfType<Inventory>().Slot_Item_Quantity[SelectedItemBarSlot + 15]--;

    }


    ////////////// ITEMS ///////////
    
    /*void Jetpack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FindObjectOfType<PlayerMovement>().moveSpeed = jetpackMoveSpeed;
            velocity.y = -2f;
            controller.Move(jumpForce * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.Space))
            moveSpeed = normalMoveSpeed;
    }*/

}

