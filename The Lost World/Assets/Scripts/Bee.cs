using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    private GameObject player;
    public float moveSpeed;
    [SerializeField]
    private Vector3 destination;
    private float sphereRadius = 200; //sfera inuntrul careia alege un punct unde sa se duca
    [SerializeField]
    private LayerMask islandMask;
    [SerializeField]
    private LayerMask playerMask;
    public bool attackPhase;
    private float attackInstantiateTime;   //cat timp a stat aproape de player si daca a stat un timp il ataca
    [SerializeField]
    private LayerMask groundLayerMask;        //daca e prea jos sa urce da aici intra orice ar putea fi sub adica si player si cladiri

    public int health = 10;
    
      
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
        if (health > 0)
        {
            if (transform.position == destination && attackPhase == false)
                Destination();
            else if (attackPhase == true)
                Attack();

            Movement();
        }
        else
            Die();

        Despawn();   //daca sunt departe de player
    }





     void Movement()
     {       
         Vector3 move = destination - transform.position;

         transform.rotation = Quaternion.LookRotation(move);

        float y;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 3f, groundLayerMask))
            y = hit.point.y + 5;
        else
            y = destination.y;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(destination.x, y, destination.z) , moveSpeed);
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

    void Despawn()      //cand e playeru departe
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 300, playerMask);
        if (colliders.Length == 0)
            Destroy(this.gameObject);
    }


    void Attack()
    {
        destination = player.transform.position + player.transform.forward * 0.01f;
        
        if(Vector3.Distance(transform.position, player.transform.position) <= 3)
        {
            attackInstantiateTime += Time.deltaTime;
            if (attackInstantiateTime >= 2f)
            {
                FindObjectOfType<Player_Stats>().playerHealth -= 5;
                attackInstantiateTime = 0;
            }
        }

        if (Vector3.Distance(transform.position, player.transform.position) > 30) //daca e prea departe renunta la atac
            attackPhase = false;
        
    }



    void Die()
    {
        transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
        transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Destroy(GetComponent<Animator>());
    }


    private void OnCollisionEnter(Collision collision)   //daca se loveste de cv schimba destinatia
    {
        Destination();
    }
}
