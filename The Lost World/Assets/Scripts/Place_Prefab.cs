using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place_Prefab : MonoBehaviour
{
    [SerializeField]
    public Transform playerPos;           //pt bridge ca daca e pe pod sa sas puna la nnivelu podului chiar daca e pe insula
    [SerializeField]
    private LayerMask other_Preab_Mask;       // numa bridge ca sa vada daca playeru e pe bridge

    public GameObject Prefab_In_Hand;
    public bool Has_Prefab_In_Hand;
    [SerializeField]
    public GameObject Building_Spawn_Position;
    [SerializeField]
    private LayerMask Floor_placeable_Surface_Mask;      //pt pe care poate ffi pusa floor asta nu poate ffi pusa pe alt floor ; masca asta inseamna layerele pe care POATE FI PUS LORU ADICA SA NU AIBA COLIZIUNE CU ASTEA
    [SerializeField]
    public LayerMask Bridge_placeable_Surface_Mask;     //e ca la floor
    [SerializeField]
    private LayerMask Wall_Placeable_Surface_Mask;
  
    //[SerializeField]
    //private LayerMask Floor_Mask;      //ala cu layeru de floor ca sa poate ffface snap apte fflooruri sau walluri

    public bool isSnapped;


    //pt colorplacing la bridge ca nu pot atribui pe script
    public LayerMask Floating_Prefabs_Mask;     
    [SerializeField]
    public LayerMask groundedBUTnotbridgeMak;   //layerurile pe care daca sta e grounded CU EXCEPTIA podului adica layerul B_S_F
    //


    [SerializeField]
    private GameObject Item_008;    //wooden floor 
    [SerializeField]
    private GameObject Item_014;
    [SerializeField]
    private GameObject Item_015;    //ramp
    [SerializeField]
    private GameObject Item_016;    //platform
    [SerializeField]
    private GameObject Item_017;    //wall wooden

    [SerializeField]
    private LayerMask islandMask;

    void Start()
    {
        
    }

    
    void Update()
    {      

        if (FindObjectOfType<Handing_Item>().handing_placeable == false)      //daca nu e cond asta ai mai multe iteme in mana in ac timp
            PrefabSpawn();

                   

        if (Has_Prefab_In_Hand == true)  //aici il muta
        {
            SnapDetach();     // e snapeed si te departezi sa nu mai ffie snapped si sa fie din nou dupa tine

            if (isSnapped == false)   // daca e snapped sa nu l mai poti muta(oricum nu puteai pe x,z ca i scoteai parentu dar se schimba pe y
            {
                RaycastHit hit;

                if (FindObjectOfType<Handing_Item>().SelectedItemCode == 8)     //floor wood
                {
                    if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Floor_placeable_Surface_Mask))
                        Prefab_In_Hand.transform.position = new Vector3(Prefab_In_Hand.transform.position.x, hit.point.y, Prefab_In_Hand.transform.position.z);
                }
                else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 14)     //bridge
                {
                    if (Physics.CheckSphere(FindObjectOfType<PlayerMovement>().groundCheck.position, 0.4f, Floating_Prefabs_Mask))           //e pe pod ; situatia asta e daca e in aer merge si fara da ar trebui ca prima buc din pod sa fie la marginea insulei si daca vrei sa faci design nu prea merge
                    {
                        Prefab_In_Hand.transform.position = new Vector3(Prefab_In_Hand.transform.position.x, Building_Spawn_Position.transform.position.y - 4f, Prefab_In_Hand.transform.position.z);
                    }
                    else if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Bridge_placeable_Surface_Mask))
                        Prefab_In_Hand.transform.position = new Vector3(Prefab_In_Hand.transform.position.x, hit.point.y, Prefab_In_Hand.transform.position.z);
              
                }
                else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 15)     //stairs
                {             

                    if (Physics.CheckSphere(FindObjectOfType<PlayerMovement>().groundCheck.position, 0.4f, Floating_Prefabs_Mask))            //e pe pod ; situatia asta e daca e in aer merge si fara da ar trebui ca prima buc din pod sa fie la marginea insulei si daca vrei sa faci design nu prea merge
                    {
                          Prefab_In_Hand.transform.position = new Vector3(Prefab_In_Hand.transform.position.x, Building_Spawn_Position.transform.position.y - 4f, Prefab_In_Hand.transform.position.z);
                    }
                    else if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Bridge_placeable_Surface_Mask))
                        Prefab_In_Hand.transform.position = new Vector3(Prefab_In_Hand.transform.position.x, hit.point.y, Prefab_In_Hand.transform.position.z);
                    
                }
                else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 16)     //platform
                {  

                    if (Physics.CheckSphere(FindObjectOfType<PlayerMovement>().groundCheck.position, 0.4f, Floating_Prefabs_Mask))           //e pe pod ; situatia asta e daca e in aer merge si fara da ar trebui ca prima buc din pod sa fie la marginea insulei si daca vrei sa faci design nu prea merge
                    {
                          Prefab_In_Hand.transform.position = new Vector3(Prefab_In_Hand.transform.position.x, Building_Spawn_Position.transform.position.y - 4f, Prefab_In_Hand.transform.position.z);
                    }
                    else if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Bridge_placeable_Surface_Mask))
                        Prefab_In_Hand.transform.position = new Vector3(Prefab_In_Hand.transform.position.x, hit.point.y, Prefab_In_Hand.transform.position.z);

                }
                if (FindObjectOfType<Handing_Item>().SelectedItemCode == 17)     //wall wooden
                {
                    if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Wall_Placeable_Surface_Mask))
                        Prefab_In_Hand.transform.position = new Vector3(Prefab_In_Hand.transform.position.x, hit.point.y, Prefab_In_Hand.transform.position.z);
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && Prefab_In_Hand.transform.GetChild(0).GetComponent<ColorPlacingChange>().placeable == true)
            {
                PlacePrefab();
            }

        }
    }



    void PrefabSpawn()     //cand o ai in inventar si selectezi slotu sa apara buildingu si sa l poti muta
    {
        if (FindObjectOfType<Handing_Item>().SelectedItemCode == 8) // place wooden_floor
        {
            RaycastHit hit;
            if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Floor_placeable_Surface_Mask))
            {
                FindObjectOfType<Handing_Item>().handing_placeable = true;
                Has_Prefab_In_Hand = true;
                Prefab_In_Hand = Instantiate(Item_008, hit.point, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }

            Prefab_In_Hand.transform.SetParent(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform);
            Prefab_In_Hand.transform.localRotation = Quaternion.identity;
            Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<Snap_Floor>();

        }
        else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 14) // place bridge
        {
            RaycastHit hit;
            if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 1f, Bridge_placeable_Surface_Mask))
            {
                FindObjectOfType<Handing_Item>().handing_placeable = true;
                Has_Prefab_In_Hand = true;
                Prefab_In_Hand = Instantiate(Item_014, hit.point, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }
            else         // nu gaseste cv sub inseamna ca nu e pe insulea da sa poata ace snap cu alt pod in aer
            {
                FindObjectOfType<Handing_Item>().handing_placeable = true;
                Has_Prefab_In_Hand = true;
                Vector3 pos = new Vector3(Building_Spawn_Position.transform.position.x, Building_Spawn_Position.transform.position.y - 4f, Building_Spawn_Position.transform.position.z);
                Prefab_In_Hand = Instantiate(Item_014, pos, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }


            Prefab_In_Hand.transform.SetParent(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform);
            Prefab_In_Hand.transform.localRotation = Quaternion.identity;
            Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<Snap_B_S_F>();

        }
        else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 15) // place ramp
        {
            RaycastHit hit;
            if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 1f, Bridge_placeable_Surface_Mask))
            {
                FindObjectOfType<Handing_Item>().handing_placeable = true;
                Has_Prefab_In_Hand = true;
                Prefab_In_Hand = Instantiate(Item_015, hit.point, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }
            else         // nu gaseste cv sub inseamna ca nu e pe insulea da sa poata ace snap cu alt pod in aer
            {
                FindObjectOfType<Handing_Item>().handing_placeable = true;
                Has_Prefab_In_Hand = true;
                Vector3 pos = new Vector3(Building_Spawn_Position.transform.position.x, Building_Spawn_Position.transform.position.y - 4f, Building_Spawn_Position.transform.position.z);
                Prefab_In_Hand = Instantiate(Item_015, pos, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }


            Prefab_In_Hand.transform.SetParent(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform);
            Prefab_In_Hand.transform.localRotation = Quaternion.identity;
            Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<Snap_B_S_F>();

        }
        else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 16) // place platform
        {
            RaycastHit hit;
            if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 1f, Bridge_placeable_Surface_Mask))
            {
                FindObjectOfType<Handing_Item>().handing_placeable = true;
                Has_Prefab_In_Hand = true;
                Prefab_In_Hand = Instantiate(Item_016, hit.point, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }
            else         // nu gaseste cv sub inseamna ca nu e pe insulea da sa poata ace snap cu alt pod in aer
            {
                FindObjectOfType<Handing_Item>().handing_placeable = true;
                Has_Prefab_In_Hand = true;
                Vector3 pos = new Vector3(Building_Spawn_Position.transform.position.x, Building_Spawn_Position.transform.position.y - 4f, Building_Spawn_Position.transform.position.z);
                Prefab_In_Hand = Instantiate(Item_016, pos, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }


            Prefab_In_Hand.transform.SetParent(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform);
            Prefab_In_Hand.transform.localRotation = Quaternion.identity;
            Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<Snap_B_S_F>();

        }
        if (FindObjectOfType<Handing_Item>().SelectedItemCode == 17) // place wooden_wall
        {
            RaycastHit hit;
            if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Wall_Placeable_Surface_Mask))
            {
                FindObjectOfType<Handing_Item>().handing_placeable = true;
                Has_Prefab_In_Hand = true;
                Prefab_In_Hand = Instantiate(Item_017, hit.point, Quaternion.Euler(Building_Spawn_Position.transform.rotation.x, Building_Spawn_Position.transform.rotation.y, Building_Spawn_Position.transform.rotation.z));
                Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<ColorPlacingChange>();
            }

            Prefab_In_Hand.transform.SetParent(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform);
            Prefab_In_Hand.transform.localRotation = Quaternion.identity;
            Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<Snap_Wall>();

        }

        if(Has_Prefab_In_Hand == true)
        {
            Prefab_In_Hand.transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
            Prefab_In_Hand.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }


    void PlacePrefab()   // ui ai handing si apesi sa ramana pe pozitie
    {
        FindObjectOfType<Handing_Item>().handing_placeable = false;
        Has_Prefab_In_Hand = false;

        if (FindObjectOfType<Handing_Item>().SelectedItemCode == 8)  //wooden floor
            Instantiate(Item_008, Prefab_In_Hand.transform.position, Quaternion.Euler(Prefab_In_Hand.transform.rotation.x, Prefab_In_Hand.transform.eulerAngles.y, Prefab_In_Hand.transform.rotation.z));
        else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 14)  //bridge
        {
            if((Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, 10f, Bridge_placeable_Surface_Mask)) == true || isSnapped == true)       //ori e pe cv insula ori daca nu sa ie snapped de alt pod ca sa nu poti pune in aer
               Instantiate(Item_014, Prefab_In_Hand.transform.position, Quaternion.Euler(Prefab_In_Hand.transform.rotation.x, Prefab_In_Hand.transform.eulerAngles.y, Prefab_In_Hand.transform.rotation.z));
        }
        else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 15)  //ramp
        {
            if ((Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, 10f, Bridge_placeable_Surface_Mask)) == true || isSnapped == true)       //ori e pe cv insula ori daca nu sa ie snapped de alt pod ca sa nu poti pune in aer
                Instantiate(Item_015, Prefab_In_Hand.transform.position, Quaternion.Euler(Prefab_In_Hand.transform.rotation.x, Prefab_In_Hand.transform.eulerAngles.y, Prefab_In_Hand.transform.rotation.z));
        }
        else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 16)  //platform
        {
            if ((Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, 10f, Bridge_placeable_Surface_Mask)) == true || isSnapped == true)       //ori e pe cv insula ori daca nu sa ie snapped de alt pod ca sa nu poti pune in aer
                Instantiate(Item_016, Prefab_In_Hand.transform.position, Quaternion.Euler(Prefab_In_Hand.transform.rotation.x, Prefab_In_Hand.transform.eulerAngles.y, Prefab_In_Hand.transform.rotation.z));
        }
        if (FindObjectOfType<Handing_Item>().SelectedItemCode == 17)  //wooden wall
            Instantiate(Item_017, Prefab_In_Hand.transform.position, Quaternion.Euler(Prefab_In_Hand.transform.rotation.x, Prefab_In_Hand.transform.eulerAngles.y, Prefab_In_Hand.transform.rotation.z));

        isSnapped = false;


        RaycastHit hit;
        if (Physics.Raycast(Building_Spawn_Position.transform.position, -transform.up, out hit, 100f, islandMask))
        {
            if (hit.collider.transform.parent.tag == "Island Point Type 1")
                hit.collider.transform.parent.GetComponent<IslandObjects_Forest>().hasBuildingOnIt = true;
            else if (hit.collider.transform.parent.tag == "Island Point Type 2")
                hit.collider.transform.parent.GetComponent<IslandObjects_Snow>().hasBuildingOnIt = true;
            else if (hit.collider.transform.parent.tag == "Island Point Type 3")
                hit.collider.transform.parent.GetComponent<IslandObjects_Desert>().hasBuildingOnIt = true;
        }


        Destroy(Prefab_In_Hand.gameObject);

        FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15]--;   //ai plasat cladirea o scoate din inventar
        if (FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] == 0)
        {
            FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] = 0;
            FindObjectOfType<Handing_Item>().SelectedItemCode = 0;
        }

        FindObjectOfType<Sounds_Player>().place_building_prefab_sound.Play();
    }

    void SnapDetach()      // e snapeed si te departezi sa nu mai ffie snapped si sa fie din nou dupa tine
    {
        if (isSnapped == true && Vector3.Distance(Prefab_In_Hand.transform.position, FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position) > 5) //e snapped de alt floor si playeru s a departat prea mult si tre sa revina sa poate muta playeru
        {
            isSnapped = false;
            if (FindObjectOfType<Handing_Item>().SelectedItemCode == 8)
            {
                RaycastHit hit;
                if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Floor_placeable_Surface_Mask))
                {
                    Prefab_In_Hand.transform.position = hit.point;
                }
            }
            else if(FindObjectOfType<Handing_Item>().SelectedItemCode == 14)
            {
                RaycastHit hit;
                if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Bridge_placeable_Surface_Mask))
                {
                    Prefab_In_Hand.transform.position = hit.point;
                }
                else
                    Prefab_In_Hand.transform.position = new Vector3(Building_Spawn_Position.transform.position.x, Building_Spawn_Position.transform.position.y - 4f, Building_Spawn_Position.transform.position.z);
            }
            else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 15)
            {
                RaycastHit hit;
                if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Bridge_placeable_Surface_Mask))
                {
                    Prefab_In_Hand.transform.position = hit.point;
                }
                else
                    Prefab_In_Hand.transform.position = new Vector3(Building_Spawn_Position.transform.position.x, Building_Spawn_Position.transform.position.y - 4f, Building_Spawn_Position.transform.position.z);
            }
            else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 16)
            {
                RaycastHit hit;
                if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Bridge_placeable_Surface_Mask))
                {
                    Prefab_In_Hand.transform.position = hit.point;
                }
                else
                    Prefab_In_Hand.transform.position = new Vector3(Building_Spawn_Position.transform.position.x, Building_Spawn_Position.transform.position.y - 4f, Building_Spawn_Position.transform.position.z);
            }
            else if (FindObjectOfType<Handing_Item>().SelectedItemCode == 17)
            {
                RaycastHit hit;
                if (Physics.Raycast(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform.position, -transform.up, out hit, 10f, Wall_Placeable_Surface_Mask))
                {
                    Prefab_In_Hand.transform.position = hit.point;
                }
            }

           Prefab_In_Hand.transform.SetParent(FindObjectOfType<Handing_Item>().Building_Spawn_Position.transform);
           Prefab_In_Hand.transform.localRotation = Quaternion.identity;
            
            
       }
    }


    
}
