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
    private float sphereRadius = 200; //sfera inuntrul careia alege un punct unde sa se duca
    [SerializeField]
    private LayerMask islandMask;
    [SerializeField]
    private LayerMask playerMask;
       
      
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().player;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, islandMask))     //se pot spawna la un y prea sus (prea jos nu pot ca e facut in animal spawn sa fie deasupra insulei)
            transform.position = new Vector3(transform.position.x, hit.collider.transform.position.y + Random.Range(7, 23), transform.position.z);
            
        destination = Random.insideUnitSphere * 1 + new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }

    
    void Update()
    {
        if (Vector3.Distance(transform.position, destination) < 2)
            Destination();

        Movement();

        Despawn();   //daca sunt departe de player
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

    void Despawn()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 300, playerMask);
        if (colliders.Length == 0)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)   //daca se loveste de cv schimba destinatia
    {
        Destination();
    }
}
