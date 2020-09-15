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
    float gravity = -20f;
    [SerializeField]
    public Transform groundCheck;
    [SerializeField]
    float sphereRadius = 0.4f;
    [SerializeField]
    public bool isGrounded;
    [SerializeField]
    public LayerMask GroundMask;
    [SerializeField]
    public bool MovementFrozen;  //daca e deschis vreun inventar sau ceva

    private float time = 0;
    void Start()
    {
        time = 0;
    }

   
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > 5)
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

            if (MovementFrozen == false)
                controller.Move(move * moveSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            //   MiniIslandMove();


            if (Input.GetKey(KeyCode.M))
                Application.Quit();
        }

    }

    void MiniIslandMove()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 5f, GroundMask))
            if (hit.collider.tag == "MiniIsland")
            {
                transform.SetParent(hit.collider.GetComponentInParent<Transform>().transform);
                transform.position = new Vector3(GetComponentInParent<Transform>().transform.position.x, hit.point.y + 2f, transform.position.z);           
            }
    }
    
}
