using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Handing_Item : MonoBehaviour
{                                               /// itemel din mana intra aci si placementu de buildinguri ////

  
    [SerializeField]
    public int SelectedItemBarSlot;
    public int SelectedItemCode;
    public bool handing_placeable;          //true daca poti sa o vezi in fata ta adica e in plasare 
    public bool handing_Item;                     //aici intra toolurile gen spear gps bee ood
    public GameObject item_In_Hands;
    [SerializeField]
    public GameObject Building_Spawn_Position;
    public RaycastHit hit;
    [SerializeField]
    private LayerMask animalMask;
  

    //pt scriptu  ColorPlacingChange 
    public Material OkMaterial;
    [SerializeField]
    public Material CollidingMaterial;



    ///// Items to handle (tools,weapons) or buildings to plac  \\\\\\

    [SerializeField]
    private GameObject Item_012;    //spear
    [SerializeField]
    private GameObject Item_025;    //GPS


    void Start()
    {
        Item_012.SetActive(false);
        Item_025.SetActive(false);
    }


    void Update()
    {
        if (FindObjectOfType<Inventory>().inventory_craftingIsActive == false)
        {
            SelectItemSlot();
            UseItem();
            Eat();
        }

        if (handing_placeable == true || handing_Item == true)
            FindObjectOfType<Buttons>().RemoveButton.gameObject.SetActive(true);
        else
            FindObjectOfType<Buttons>().RemoveButton.gameObject.SetActive(false);
    }


    void UseItem()
    {
        if (SelectedItemCode == 12)    //spear
        {
            Item_012.SetActive(true);
            handing_Item = true;
            item_In_Hands = Item_012;
        }
        else if (SelectedItemCode == 25)    //GPS
        {
            Item_025.SetActive(true);
            handing_Item = true;
            item_In_Hands = Item_025;
        }

    }


    void Eat()
    {
        if (SelectedItemCode == 11)   //apple
        {
            FindObjectOfType<Buttons>().EatButton.SetActive(true);
            FindObjectOfType<Buttons>().EatButton.transform.Find("Food_Name").GetComponent<TextMeshProUGUI>().text = "Eat 'Apple'";
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                FindObjectOfType<Player_Stats>().playerFood += 10;
                FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15]--;   

                if (FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] == 0)
                {
                    FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] = 0;
                    FindObjectOfType<Handing_Item>().SelectedItemCode = 0;
                }
            }
        }
        else if (SelectedItemCode == 13)  //honeycomb
        {
            FindObjectOfType<Buttons>().EatButton.SetActive(true);
            FindObjectOfType<Buttons>().EatButton.transform.Find("Food_Name").GetComponent<TextMeshProUGUI>().text = "Eat 'Honeycomb'";
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                FindObjectOfType<Player_Stats>().playerFood += 10;
                FindObjectOfType<Player_Stats>().playerPoison -= 5;

                FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15]--;
                if (FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] == 0)
                {
                    FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] = 0;
                    FindObjectOfType<Handing_Item>().SelectedItemCode = 0;
                }
            }
        }
        else if (SelectedItemCode == 27)  //cactus sap
        {
            FindObjectOfType<Buttons>().EatButton.SetActive(true);
            FindObjectOfType<Buttons>().EatButton.transform.Find("Food_Name").GetComponent<TextMeshProUGUI>().text = "Eat 'Cactus sap'";
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                FindObjectOfType<Player_Stats>().playerPoison -= 10;

                FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15]--;
                if (FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] == 0)
                {
                    FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] = 0;
                    FindObjectOfType<Handing_Item>().SelectedItemCode = 0;
                }
            }
        }
        else if (SelectedItemCode == 28)  //Blackberries
        {
            FindObjectOfType<Buttons>().EatButton.SetActive(true);
            FindObjectOfType<Buttons>().EatButton.transform.Find("Food_Name").GetComponent<TextMeshProUGUI>().text = "Eat 'Blackberries'";
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                FindObjectOfType<Player_Stats>().playerFood += 20;
                FindObjectOfType<Player_Stats>().playerPoison += 5;

                FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15]--;
                if (FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] == 0)
                {
                    FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] = 0;
                    FindObjectOfType<Handing_Item>().SelectedItemCode = 0;
                }
            }
        }
        else if (SelectedItemCode == 29)  //bee food chiar daca nu se poate manca pot manca albinele
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 20f, animalMask) && hit.collider.tag == "Bee" && hit.collider.gameObject.GetComponent<Bee>().isPet == false)
            {
                FindObjectOfType<Buttons>().EatButton.SetActive(true);
                FindObjectOfType<Buttons>().EatButton.transform.Find("Food_Name").GetComponent<TextMeshProUGUI>().text = "Pet Bee'";

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    hit.collider.gameObject.GetComponent<Bee>().isPet = true;
                    hit.collider.gameObject.GetComponent<Bee>().attackPhase = false; //daca ii dai pet cand te ataca sa nu te mai atace

                    FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15]--;
                    if (FindObjectOfType<Inventory>().Slot_Item_Quantity[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] == 0)
                    {
                        FindObjectOfType<Inventory>().Slot_Item_Code[FindObjectOfType<Handing_Item>().SelectedItemBarSlot + 15] = 0;
                        FindObjectOfType<Handing_Item>().SelectedItemCode = 0;
                    }
                }
            }
            else
                FindObjectOfType<Buttons>().EatButton.SetActive(false);
        }

        else
            FindObjectOfType<Buttons>().EatButton.SetActive(false);
    }


    void SelectItemSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (FindObjectOfType<Place_Building>().Has_Building_In_Hand == true)                    
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);  
            else if (FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand == true)
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);
            
            SelectedItemBarSlot = 1;
            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[16] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[16] = 0;

            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[16];

            handing_placeable = false;
            FindObjectOfType<Place_Building>().Has_Building_In_Hand = false;
            FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand = false;

            if (handing_Item == true)
            {
                item_In_Hands.SetActive(false);
                handing_Item = false;
            }      

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (FindObjectOfType<Place_Building>().Has_Building_In_Hand == true)               //daca nu scriu asta apare un bug de incurca unityu            
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);  //daca ai o cladire in mana si schimbi pe alt slot sau tot pe ala pe care esti sa nu mai ai cladirea in mana
            else if (FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand == true)
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);

            SelectedItemBarSlot = 2;
            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[17] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[17] = 0;

            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[17];

            handing_placeable = false;
            FindObjectOfType<Place_Building>().Has_Building_In_Hand = false;
            FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand = false;

            if (handing_Item == true)
            {
                item_In_Hands.SetActive(false);
                handing_Item = false;
            }


        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (FindObjectOfType<Place_Building>().Has_Building_In_Hand == true)               //daca nu scriu asta apare un bug de incurca unityu            
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);  //daca ai o cladire in mana si schimbi pe alt slot sau tot pe ala pe care esti sa nu mai ai cladirea in mana
            else if (FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand == true)
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);

            SelectedItemBarSlot = 3;
            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[18] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[18] = 0;

            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[18];

            handing_placeable = false;
            FindObjectOfType<Place_Building>().Has_Building_In_Hand = false;
            FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand = false;

            if (handing_Item == true)
            {
                item_In_Hands.SetActive(false);
                handing_Item = false;
            }


        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (FindObjectOfType<Place_Building>().Has_Building_In_Hand == true)               //daca nu scriu asta apare un bug de incurca unityu            
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);  //daca ai o cladire in mana si schimbi pe alt slot sau tot pe ala pe care esti sa nu mai ai cladirea in mana
            else if (FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand == true)
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);

            SelectedItemBarSlot = 4;
            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[19] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[19] = 0;

            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[19];

            handing_placeable = false;
            FindObjectOfType<Place_Building>().Has_Building_In_Hand = false;
            FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand = false;

            if (handing_Item == true)
            {
                item_In_Hands.SetActive(false);
                handing_Item = false;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (FindObjectOfType<Place_Building>().Has_Building_In_Hand == true)               //daca nu scriu asta apare un bug de incurca unityu            
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);  //daca ai o cladire in mana si schimbi pe alt slot sau tot pe ala pe care esti sa nu mai ai cladirea in mana
            else if (FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand == true)
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);

            SelectedItemBarSlot = 5;
            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[20] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[20] = 0;

            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[20];

            handing_placeable = false;
            FindObjectOfType<Place_Building>().Has_Building_In_Hand = false;
            FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand = false;

            if (handing_Item == true)
            {
                item_In_Hands.SetActive(false);
                handing_Item = false;
            }


        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (FindObjectOfType<Place_Building>().Has_Building_In_Hand == true)               //daca nu scriu asta apare un bug de incurca unityu            
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);  //daca ai o cladire in mana si schimbi pe alt slot sau tot pe ala pe care esti sa nu mai ai cladirea in mana
            else if (FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand == true)
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);

            SelectedItemBarSlot = 6;
            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[21] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[21] = 0;

            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[21];

            handing_placeable = false;
            FindObjectOfType<Place_Building>().Has_Building_In_Hand = false;
            FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand = false;

            if (handing_Item == true)
            {
                item_In_Hands.SetActive(false);
                handing_Item = false;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (FindObjectOfType<Place_Building>().Has_Building_In_Hand == true)               //daca nu scriu asta apare un bug de incurca unityu            
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);  //daca ai o cladire in mana si schimbi pe alt slot sau tot pe ala pe care esti sa nu mai ai cladirea in mana
            else if (FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand == true)
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);

            SelectedItemBarSlot = 7;
            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[22] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[22] = 0;

            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[22];

            handing_placeable = false;
            FindObjectOfType<Place_Building>().Has_Building_In_Hand = false;
            FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand = false;

            if (handing_Item == true)
            {
                item_In_Hands.SetActive(false);
                handing_Item = false;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (FindObjectOfType<Place_Building>().Has_Building_In_Hand == true)               //daca nu scriu asta apare un bug de incurca unityu            
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);  //daca ai o cladire in mana si schimbi pe alt slot sau tot pe ala pe care esti sa nu mai ai cladirea in mana
            else if (FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand == true)
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);

            SelectedItemBarSlot = 8;
            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[23] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[23] = 0;

            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[23];

            handing_placeable = false;
            FindObjectOfType<Place_Building>().Has_Building_In_Hand = false;
            FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand = false;

            if (handing_Item == true)
            {
                item_In_Hands.SetActive(false);
                handing_Item = false;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (FindObjectOfType<Place_Building>().Has_Building_In_Hand == true)               //daca nu scriu asta apare un bug de incurca unityu            
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);  //daca ai o cladire in mana si schimbi pe alt slot sau tot pe ala pe care esti sa nu mai ai cladirea in mana
            else if (FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand == true)
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);

            SelectedItemBarSlot = 9;
            if (FindObjectOfType<Inventory>().Slot_Item_Quantity[24] <= 0)
                FindObjectOfType<Inventory>().Slot_Item_Code[24] = 0;

            SelectedItemCode = FindObjectOfType<Inventory>().Slot_Item_Code[24];

            handing_placeable = false;
            FindObjectOfType<Place_Building>().Has_Building_In_Hand = false;
            FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand = false;

            if (handing_Item == true)
            {
                item_In_Hands.SetActive(false);
                handing_Item = false;
            }

        }
        else if (Input.GetKeyDown(KeyCode.X))    // daca ai ceva in mana sa nu mai ai
        {
            if (FindObjectOfType<Place_Building>().Has_Building_In_Hand == true)                
                Destroy(FindObjectOfType<Place_Building>().Building_In_Hand.gameObject);  
            else if (FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand == true)
                Destroy(FindObjectOfType<Place_Prefab>().Prefab_In_Hand.gameObject);

            SelectedItemBarSlot = 0;
            SelectedItemCode = 0;
            
            handing_placeable = false;
            FindObjectOfType<Place_Building>().Has_Building_In_Hand = false;
            FindObjectOfType<Place_Prefab>().Has_Prefab_In_Hand = false;

            if (handing_Item == true)
            {
                item_In_Hands.SetActive(false);
                handing_Item = false;
            }

        }

      
    }

}

