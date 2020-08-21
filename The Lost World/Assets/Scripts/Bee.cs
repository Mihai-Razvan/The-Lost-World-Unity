using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    public CharacterController controller;
    public float moveSpeed;
    [SerializeField]
    private Vector3 destination;
    private float circleRadius = 200;
    [SerializeField]
    private Vector3 ppos;
       
      
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().player;
    }

    
    void Update()
    {
        if (Vector3.Distance(transform.position, destination) < 2)
            ChooseDestination();
        Movement();
    }


    
    
     
    void Movement()
    {       
        Vector3 move = destination - transform.position;

        transform.rotation = Quaternion.LookRotation(new Vector3(move.x, move.y, move.z));

        controller.Move(move * moveSpeed * Time.deltaTime);
   
    }

    void ChooseDestination()
    {
        ppos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        destination = Random.insideUnitSphere * circleRadius + ppos;
    }


    
}
