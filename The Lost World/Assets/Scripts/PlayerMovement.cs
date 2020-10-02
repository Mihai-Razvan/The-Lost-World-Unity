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
    float sphereRadius = 1f;
    [SerializeField]
    public bool isGrounded;
    [SerializeField]
    public LayerMask GroundMask;
    [SerializeField]
    public bool MovementFrozen;  //daca e deschis vreun inventar sau ceva

    [SerializeField]
    private float flySpeed;
    [SerializeField]
    private bool isFlying;
    [SerializeField]
    private int flyFrame;
    private Vector3 jumpDirection;

    private float time = 0;

    [SerializeField]
    private LayerMask buildingMask;
    [SerializeField]
    private LayerMask objects_collectablesMask;

    void Start()
    {
        time = 0;
        jumpDirection = new Vector3(0, 1, 0);
    }

   
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, GroundMask);


            if (isGrounded == true && velocity.y < 0)
            {
                if (velocity.y < -50)
                    FindObjectOfType<Player_Stats>().playerHealth -= Mathf.Abs((velocity.y + 50) * FindObjectOfType<Player_Stats>().falling_damage);
                velocity.y = -2f;
            }


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

          //  controller.Move(velocity * Time.deltaTime);

            if(MovementFrozen == false)
                Jump();

            InactiveBuildings();
            

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

    void Jump()
    {
        if(isGrounded == true && isFlying == false)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isFlying = true;
                flyFrame = 0;             
            }
        }
        else if (isFlying == true)
        {
            if (flyFrame < 10)
            {
                FindObjectOfType<PlayerMovement>().controller.Move((Camera.main.transform.forward + new Vector3(0,1,0)) * flySpeed * (10 - flyFrame) * Time.deltaTime);             
                flyFrame++;
            }
            else
                isFlying = false;
        }
            

    }



    void InactiveBuildings()
    {
        //daca e departe e activ doar punctu/// daca e la dist medie e activ doar child(0) adica cladirea da nu si colliderele de le mai are gen cum au prefaburile ///daca e apropae tot activ
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100, buildingMask);
        
        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i].tag == "Item Point 008" || colliders[i].tag == "Item Point 009" || colliders[i].tag == "Item Point 014" || colliders[i].tag == "Item Point 015" || colliders[i].tag == "Item Point 016" || colliders[i].tag == "Item Point 017" || colliders[i].tag == "Item Point 030")
            {
                if (Vector3.Distance(transform.position, colliders[i].gameObject.transform.position) < 50)
                {
                    if (Vector3.Distance(transform.position, colliders[i].gameObject.transform.position) < 20)
                        for (int j = 0; j < colliders[i].transform.childCount; j++)
                            colliders[i].transform.GetChild(j).gameObject.SetActive(true);
                    else
                    {
                        colliders[i].transform.GetChild(0).gameObject.SetActive(true);
                        for (int j = 1; j < colliders[i].transform.childCount; j++)
                            colliders[i].transform.GetChild(j).gameObject.SetActive(false);
                    }
                }
                else
                    for (int j = 0; j < colliders[i].transform.childCount; j++)
                        colliders[i].transform.GetChild(j).gameObject.SetActive(false);
            }     


    }


  

}
