using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    public CharacterController controller;
    [SerializeField]
    private float x;
    private float z;
    public int moveSpeed;
    [SerializeField]
    public Vector3 velocity;
    [SerializeField]
    float gravity = -9.81f;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    float sphereRadius = 0.4f;
    [SerializeField]
    public bool isGrounded;
    [SerializeField]
    public LayerMask GroundMask;
    public bool MovementFrozen;  //daca e deschis vreun inventar sau ceva
   

   



    void Start()
    {
        
    }

   
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, GroundMask);

        if (isGrounded == true && velocity.y < 0)
            velocity.y = -2f;

        if (MovementFrozen == false)   
        {
             x = Input.GetAxisRaw("Horizontal");
             z = Input.GetAxisRaw("Vertical");
        }

        if (FindObjectOfType<Special_Item>().useJetpack == true)    //asta cu jetpacku sa nu poti merge normal daca folosesti jetpacku pt ca se aduna vitezele si se poate exploata
            moveSpeed = 0;
        else
            moveSpeed = 10;

       Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


    }
}
