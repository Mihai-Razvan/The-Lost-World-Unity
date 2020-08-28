using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickup_Item : MonoBehaviour
{
    [SerializeField]
    private float DetectionCapsuleLength;
    [SerializeField]
    private float DetectionCapsuleRadius;
    [SerializeField]
    private LayerMask CollectablesMask;
    [SerializeField]
    private LayerMask animalMask;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject Pickup_Button;   //E-u ala de apare pe ecran sa stie playeru pe ce sa apese
    void Start()
    {
        Pickup_Button.SetActive(false);
    }

   
    void Update()
    {
        //fac cu capsule in loc de ray ca nush dc nu merge cu ray 
        if (FindObjectOfType<Place_Building>().Building_In_Hand == null && FindObjectOfType<Place_Prefab>().Prefab_In_Hand == null)     //sa nu poti aduna cand pui cladiri
        {
            Collider[] colliders = Physics.OverlapCapsule(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * DetectionCapsuleLength, DetectionCapsuleRadius, CollectablesMask);
            if (colliders.Length > 0)
            {
                Pickup_Button.SetActive(true);
                Pickup_Button.transform.Find("Item_Name").GetComponent<TextMeshProUGUI>().text = "Collect '" + colliders[0].gameObject.tag + "'";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    FindObjectOfType<Inventory>().quantityToAdd = 1;

                    if (colliders[0].gameObject.tag == "Wood")
                    {
                        FindObjectOfType<Inventory>().itemCodeToAdd = 1;
                        Destroy(colliders[0].transform.parent.gameObject);
                    }
                    else if (colliders[0].gameObject.tag == "Iron ore")
                    {
                        FindObjectOfType<Inventory>().itemCodeToAdd = 3;
                        Destroy(colliders[0].transform.parent.gameObject);
                    }
                    else if (colliders[0].gameObject.tag == "Stone")
                    {
                        FindObjectOfType<Inventory>().itemCodeToAdd = 5;
                        Destroy(colliders[0].transform.parent.gameObject);    //distruge si punctu la care e atasat
                    }
                    else if (colliders[0].gameObject.tag == "Apple")
                    {
                        FindObjectOfType<Inventory>().itemCodeToAdd = 11;
                        Destroy(colliders[0].gameObject);    //maru nu are pct ca nu trebuie si optimizare
                        BeeAttract();
                    }

                }
            }
            else
                Pickup_Button.SetActive(false);
        }
        else
            Pickup_Button.SetActive(false);
    }


    void BeeAttract()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, 500f, animalMask);
        for (int i = 0; i < colliders.Length; i++)
            if(colliders[i].tag == "Bee")
                colliders[i].gameObject.GetComponent<Bee>().attackPhase = true;

    }
}
