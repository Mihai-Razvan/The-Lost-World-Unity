using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float mouseX;
    float mouseY;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    Transform playerTransform;
    float xRotation = 0f;
    [SerializeField]
    private float CollectablesDetectionCapsuleRadius;
    [SerializeField]
    private float DetectionCapsuleLength;
    [SerializeField]
    private LayerMask CollectablesDetectionSphereMask;
    [SerializeField]
    private LayerMask BuildingsDetectionSphereMask;
    [SerializeField]
    private float BuildingsDetectionCapsuleRadius;
    [SerializeField]
    private GameObject Inventory_Crafting_Panel;
    [SerializeField]
    private GameObject IC_Normal_Position;
    [SerializeField]
    private GameObject IC_Backup_Position;               // unde stau inv si craffting panelu cand ai deschis in dr inventaru la cv
   // public bool Other_Inventory_Open;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (FindObjectOfType<PlayerMovement>().MovementFrozen == false)                              //sa nu se miste camera sa si pickup cand esti in inventar
        {
           
            mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -75f, 80f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerTransform.Rotate(Vector3.up * mouseX);
            
        }
             
        
    }

}
