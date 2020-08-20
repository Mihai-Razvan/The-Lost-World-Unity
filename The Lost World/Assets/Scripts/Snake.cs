using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    public CharacterController controller;
    private float x;
    private float z;
    private float lastx;
    private float lastz;
    public float moveSpeed;
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

    [SerializeField]
    private GameObject[] body_parts;
    [SerializeField]

    private float dis;
    [SerializeField]
    private float mindis;
    private float angle;

    

    
      
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 5)
        {
            HeadMovement();
            BodyPartsFollow();
        }

    }


    void BodyPartsFollow()
    {
        for (int i = body_parts.Length - 1; i > 0; i--)
        {
            GameObject part = body_parts[i - 1];

            float T = Time.deltaTime * dis / mindis * moveSpeed;
            if (T > 0.5f)
                T = 0.5f;

            body_parts[i].transform.position = Vector3.Slerp(body_parts[i].transform.position, part.transform.position, T);  //de pe net las o asa
        }
    }

    void HeadMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, GroundMask);

        if (isGrounded == true && velocity.y < 0)
            velocity.y = -2f;

        Vector3 move = player.transform.position - transform.position;


        controller.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;

        
    }


    
}
