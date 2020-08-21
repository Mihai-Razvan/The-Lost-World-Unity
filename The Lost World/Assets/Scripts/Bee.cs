using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    private Vector3 destination;
    private float sphereRadius = 200;
    [SerializeField]
    private LayerMask islandMask;
       
      
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().player;
       
        destination = Random.insideUnitSphere * 1 + new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }

    
    void Update()
    {
        if (Vector3.Distance(transform.position, destination) < 2)
            Destination();

        Movement(); 
    }





     void Movement()
     {       
        Vector3 move = destination - transform.position;

         transform.rotation = Quaternion.LookRotation(move);

         transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed);     

     }
     


    void Destination()
    {
        destination = Random.insideUnitSphere * sphereRadius + new Vector3(transform.position.x, transform.position.y, transform.position.z);

        RaycastHit hit;
        if (Physics.Raycast(destination, -transform.up, out hit, 100, islandMask))
        {
            destination.y = hit.collider.transform.position.y + Random.Range(7, 23);   //alege y
        }
        else
            Destination();      //daca destinatia e inafara insulei alege alta dest          
    }

   
}
