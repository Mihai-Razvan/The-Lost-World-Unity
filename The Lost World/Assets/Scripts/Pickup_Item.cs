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
    private LayerMask DetectionSphereMask;


    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            Collider[] colliders = Physics.OverlapCapsule(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * DetectionCapsuleLength, DetectionCapsuleRadius, DetectionSphereMask);

            if (colliders.Length > 0)
            {
                FindObjectOfType<Inventory>().quantityToAdd = 1;

                if (colliders[0].gameObject.name == "Branch")
                    FindObjectOfType<Inventory>().itemCodeToAdd = 1;
                else if (colliders[0].gameObject.name == "Iron_Ore")
                    FindObjectOfType<Inventory>().itemCodeToAdd = 3;
                else if (colliders[0].gameObject.name == "Stone")
                    FindObjectOfType<Inventory>().itemCodeToAdd = 5;

                Destroy(colliders[0].gameObject);
            }

        }
    }
}
