using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickup_Item : MonoBehaviour
{
    [SerializeField]
    public float DetectionCapsuleLength;
    [SerializeField]
    public float DetectionCapsuleRadius;
    [SerializeField]
    private LayerMask CollectablesMask;
    [SerializeField]
    private LayerMask animalMask;
    [SerializeField]
    private GameObject player;

    void Start()
    {
        
    }

   
    void Update()
    {
        //fac cu capsule in loc de ray ca nush dc nu merge cu ray 
        if (FindObjectOfType<Place_Building>().Building_In_Hand == null && FindObjectOfType<Place_Prefab>().Prefab_In_Hand == null)     //sa nu poti aduna cand pui cladiri
        {
            Collider[] colliders = Physics.OverlapCapsule(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * DetectionCapsuleLength, DetectionCapsuleRadius, CollectablesMask);
            if (colliders.Length > 0)
            {
                FindObjectOfType<Buttons>().Pickup_Button.SetActive(true);

                if (colliders[0].tag != "Trash Box")
                    FindObjectOfType<Buttons>().Pickup_Button.transform.Find("Item_Name").GetComponent<TextMeshProUGUI>().text = "Collect '" + colliders[0].gameObject.tag + "'";
                else
                    FindObjectOfType<Buttons>().Pickup_Button.transform.Find("Item_Name").GetComponent<TextMeshProUGUI>().text = "Collect '" + FindObjectOfType<List_Of_Items>().Item_Name[colliders[0].GetComponent<Dropped_Box>().itemCode] + "'";

                if (Input.GetKeyDown(KeyCode.E))
                {
                   
                    if (colliders[0].gameObject.tag == "Stick")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = 1;
                        FindObjectOfType<Inventory>().itemCodeToAdd = 1;
                        Destroy(colliders[0].transform.parent.gameObject);
                    }
                    else if (colliders[0].gameObject.tag == "Iron ore")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = 1;
                        FindObjectOfType<Inventory>().itemCodeToAdd = 3;
                        Destroy(colliders[0].transform.parent.gameObject);
                    }
                    else if (colliders[0].gameObject.tag == "Stone")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = 1;
                        FindObjectOfType<Inventory>().itemCodeToAdd = 5;
                        Destroy(colliders[0].transform.parent.gameObject);    //distruge si punctu la care e atasat
                    }
                    else if (colliders[0].gameObject.tag == "Apple")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = 1;
                        FindObjectOfType<Inventory>().itemCodeToAdd = 11;
                        Destroy(colliders[0].gameObject);    //maru nu are pct ca nu trebuie si optimizare
                        BeeAttract();
                    }
                    else if (colliders[0].gameObject.tag == "Log")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = 1;
                        FindObjectOfType<Inventory>().itemCodeToAdd = 18;
                        Destroy(colliders[0].transform.parent.gameObject);    
                    }
                    else if (colliders[0].gameObject.tag == "Copper ore")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = 1;
                        FindObjectOfType<Inventory>().itemCodeToAdd = 19;
                        Destroy(colliders[0].transform.parent.gameObject);
                    }
                    else if (colliders[0].gameObject.tag == "Silicon")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = 1;
                        FindObjectOfType<Inventory>().itemCodeToAdd = 21;
                        Destroy(colliders[0].transform.parent.gameObject);
                    }
                    else if (colliders[0].gameObject.tag == "Crystal")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = 1;
                        FindObjectOfType<Inventory>().itemCodeToAdd = 22;
                        Destroy(colliders[0].transform.parent.gameObject);
                    }
                    else if (colliders[0].gameObject.tag == "Rich iron ore")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = 1;
                        FindObjectOfType<Inventory>().itemCodeToAdd = 23;
                        Destroy(colliders[0].transform.parent.gameObject);
                    }
                    else if (colliders[0].gameObject.tag == "Blackberries")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = 1;
                        FindObjectOfType<Inventory>().itemCodeToAdd = 28;
                        Destroy(colliders[0].gameObject);    //maru nu are pct ca nu trebuie si optimizare
                        BeeAttract();
                    }
                    else if (colliders[0].gameObject.tag == "Trash Box")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = colliders[0].GetComponent<Dropped_Box>().itemQuantity;
                        FindObjectOfType<Inventory>().itemCodeToAdd = colliders[0].GetComponent<Dropped_Box>().itemCode;
                        Destroy(colliders[0].gameObject);
                    }
                    else if (colliders[0].gameObject.tag == "Spooky pumpkin")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = 1;
                        FindObjectOfType<Inventory>().itemCodeToAdd = 34;
                        Destroy(colliders[0].transform.parent.gameObject);
                    }
                    else if (colliders[0].gameObject.tag == "Bee bait")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = 1;
                        FindObjectOfType<Inventory>().itemCodeToAdd = 29;
                        Destroy(colliders[0].transform.parent.gameObject);
                    }
                    else if (colliders[0].gameObject.tag == "Pumpkin pie")
                    {
                        FindObjectOfType<Inventory>().quantityToAdd = 1;
                        FindObjectOfType<Inventory>().itemCodeToAdd = 36;
                        Destroy(colliders[0].transform.parent.gameObject);
                    }


                    FindObjectOfType<Sounds_Player>().collect_item_sound.Play();
                }
            }
            else
                FindObjectOfType<Buttons>().Pickup_Button.SetActive(false);
        }
        else
            FindObjectOfType<Buttons>().Pickup_Button.SetActive(false);
    }


    public void BeeAttract()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, 60f, animalMask);
        for (int i = 0; i < colliders.Length; i++)
            if(colliders[i].tag == "Bee" && colliders[i].gameObject.GetComponent<Bee>().isPet == false)
                colliders[i].gameObject.GetComponent<Bee>().attackPhase = true;

    }
}
