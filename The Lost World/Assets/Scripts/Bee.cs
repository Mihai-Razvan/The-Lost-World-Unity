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
    [SerializeField]
    private bool isDead;
    [SerializeField]
    private bool grounded;
    [SerializeField]
    public bool hitWhenDead;
    private float timeSinceHitWhenDead;


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
            if (attackPhase == false)
            {
                if (transform.position == destination)
                    Destination();

                Movement();
            }
            else
            {
                Attack();
                if (Vector3.Distance(transform.position, player.transform.position) > 3)    //sa nu se puna exact in pozitia playerului
                    Movement();
            }
        }
        else
        {
            Die();

            if (hitWhenDead == true)
            {
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                timeSinceHitWhenDead += Time.deltaTime;
                if (timeSinceHitWhenDead > 5f)
                    Destroy(this.gameObject);
            }
        }

        Despawn();   //daca sunt departe de player
    }





    void Movement()
    {
        Vector3 move = destination - transform.position;

        transform.rotation = Quaternion.LookRotation(move);

        float y;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.4f, groundLayerMask))
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, 500, playerMask);
        if (colliders.Length == 0)
            Destroy(this.gameObject);
    }


    void Attack()
    {
        destination = player.transform.position;

        if (Vector3.Distance(transform.position, player.transform.position) <= 4)
        {
            attackInstantiateTime += Time.deltaTime;
            if (attackInstantiateTime >= 2f)
            {
                FindObjectOfType<Player_Stats>().playerHealth -= 5;
                FindObjectOfType<Player_Stats>().playerPoison += 5;
                if (FindObjectOfType<Player_Stats>().playerPoison > 100)
                    FindObjectOfType<Player_Stats>().playerPoison = 100;
                attackInstantiateTime = 0;
            }
        }
        else
            attackInstantiateTime = 0;
        

        if (Vector3.Distance(transform.position, player.transform.position) > 300) //daca e prea departe renunta la atac
        {
            attackPhase = false;
            Destination();
        }
        
    }



    void Die()
    {
        isDead = true;

        if(grounded == false)
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        GetComponent<Rigidbody>().useGravity = true;
        Destroy(GetComponent<Animator>());
    }



    void CollidingDestination()      //cand se loveste de cv schimba destinatia da spre deosebire de destination() normal aici sfere e in spate nu in juru albinei ca albina e mijl sfferei
    {
        destination = Random.insideUnitSphere * sphereRadius + (transform.position - transform.forward * 10);

        RaycastHit hit;
        if (Physics.Raycast(destination, -transform.up, out hit, 100, islandMask))
        {
            destination.y = hit.collider.transform.position.y + Random.Range(7, 23);   //alege y
        }
        else
            Destination();      //daca destinatia e inafara insulei alege alta dest          
    }



    private void OnCollisionEnter(Collision collision)   //daca se loveste de cv schimba destinatia
    {
        //Destination();

        if (isDead == false)
        {
            attackPhase = false;
            CollidingDestination();
        }
        else if(isDead == true && (collision.collider.gameObject.layer == 9 || collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 13 || collision.collider.gameObject.layer == 15 || collision.collider.gameObject.layer == 17))
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            grounded = true;
        }
    }
}
