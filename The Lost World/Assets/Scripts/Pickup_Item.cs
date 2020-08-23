using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Item : MonoBehaviour
{
    [SerializeField]
    private float DetectionCapsuleLength;
    [SerializeField]
    private float DetectionCapsuleRadius;
    [SerializeField]
    private LayerMask CollectablesMask;
    [SerializeField]
    bool aaa;
    void Start()
    {
        
    }

   
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {   
            //fac cu capsule in loc de ray ca nush dc nu merge cu ray
            Collider[] colliders = Physics.OverlapCapsule(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * DetectionCapsuleLength, DetectionCapsuleRadius, CollectablesMask);                 
            if (colliders.Length > 0)
            {
                FindObjectOfType<Inventory>().quantityToAdd = 1;

                if (colliders[0].gameObject.tag == "branch")
                    FindObjectOfType<Inventory>().itemCodeToAdd = 1;
                else if (colliders[0].gameObject.tag == "iron_ore")
                    FindObjectOfType<Inventory>().itemCodeToAdd = 3;
                else if (colliders[0].gameObject.tag == "stone")
                    FindObjectOfType<Inventory>().itemCodeToAdd = 5;
                else if (colliders[0].gameObject.tag == "apple")
                    FindObjectOfType<Inventory>().itemCodeToAdd = 11;
                   
                      if(colliders[0].gameObject.tag == "apple")  //maru nu are pct ca nu trebuie si optimizare
                         Destroy(colliders[0].gameObject);         
                      else
                         Destroy(colliders[0].transform.parent.gameObject);
            }

        }
    }
}
