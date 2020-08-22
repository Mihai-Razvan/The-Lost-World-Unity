using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandObjects : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerMask;
    private bool CollesctablesHaveSpawned;
    private bool ObjectdHaveSpawned;

    [SerializeField]
    public float SpawnHeight;         // se adunna la pozitia insulei si acolo spawneaza obiectele si de acolo face uin ray in jos 
    private int minRange = -250;     //de la centru insulei la ce x si z random sa se spawneze obiectele
    private int maxRange = 250; 
    private int minRangeRelief = -120;   // e mai mic ca iese de pe insula
    private int maxRangeRelief = 120;
    private float randomReliefScale;

    private int numberOfObjects;
    private int numberOfRelief;
    private int numberOfCollectables;
    private int spawnedObjectsNumber;
    private int spawnedReliefNumeber;
    private int objectRandomNumber;
    private int notSpawnedConsecutively;
    private int spawnedAlready;
    [SerializeField]
    private LayerMask reliefMask;
    [SerializeField]
    private LayerMask objectsMask;
    [SerializeField]
    private LayerMask collectablesMask;
    [SerializeField]
    private LayerMask islandMask;
    [SerializeField]
    private GameObject object_1;
    [SerializeField]
    private GameObject object_2;
    [SerializeField]
    private GameObject object_3;
    [SerializeField]
    private GameObject object_4;
    [SerializeField]
    private GameObject object_5;
    [SerializeField]
    private GameObject relief_1;
    [SerializeField]
    private GameObject relief_2;
    [SerializeField]
    private GameObject relief_3;
    [SerializeField]
    private GameObject relief_4;

    [SerializeField]
    private GameObject collectables_1;                   //branche
    [SerializeField]
    private GameObject collectables_2;                   //iron_ore
    [SerializeField]
    private GameObject collectables_3;                   //stone

    private GameObject lastSpawned;
   
    void Start()
    {   
        ReliefSpawn();  
    }

    
    void Update()
    {
        if(CollesctablesHaveSpawned == false)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 200f, playerMask);
            if (colliders.Length != 0)
            {
                CollectablesSpawn();
                CollesctablesHaveSpawned = true;
            }
        }

        if (ObjectdHaveSpawned == false)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 400f, playerMask);
            if (colliders.Length != 0)
            {
                ObjectsSpawn();
                ObjectdHaveSpawned = true;
            }
        }


        Collider[] despawncolliders = Physics.OverlapSphere(transform.position, 300f, playerMask);
        if (despawncolliders.Length != 0)
            for (int i = 1; i < transform.GetChild(0).childCount; i++)
                transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);


    }


    void ReliefSpawn()
    {
        numberOfRelief = Random.Range(8, 13);
        while (spawnedReliefNumeber < numberOfRelief)
        {
           
            RaycastHit hit;
            Physics.Raycast(new Vector3(transform.position.x + Random.Range(minRangeRelief, maxRangeRelief), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRangeRelief, maxRangeRelief)), Vector3.down, out hit, 100, islandMask);

            objectRandomNumber = Random.Range(1, 4);
            if (objectRandomNumber == 1)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 25, reliefMask);
                if (colliders.Length == 0)
                {
                    randomReliefScale = Random.Range(1, 3);
                    GameObject relief = Instantiate(relief_1, hit.point, Quaternion.identity);
                    relief.transform.localScale = new Vector3(randomReliefScale, randomReliefScale, randomReliefScale);
                }
            }
            else if (objectRandomNumber == 2)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 20, reliefMask);
                if (colliders.Length == 0)
                {
                    randomReliefScale = Random.Range(1, 3);
                    GameObject relief = Instantiate(relief_2, hit.point, Quaternion.identity);
                    relief.transform.localScale = new Vector3(randomReliefScale, randomReliefScale, randomReliefScale);
                }
            }
            else if (objectRandomNumber == 3)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 15, reliefMask);
                if (colliders.Length == 0)
                {
                    randomReliefScale = Random.Range(1, 3);
                    GameObject relief = Instantiate(relief_3, hit.point, Quaternion.identity);
                    relief.transform.localScale = new Vector3(randomReliefScale, randomReliefScale, randomReliefScale);
                }
            }
            else if (objectRandomNumber == 4)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 20, reliefMask);
                if (colliders.Length == 0)
                {
                    randomReliefScale = Random.Range(1, 3);
                    GameObject relief = Instantiate(relief_4, hit.point, Quaternion.identity);
                    relief.transform.localScale = new Vector3(randomReliefScale, randomReliefScale, randomReliefScale);
                }
            }

            spawnedReliefNumeber++;
        }
    }


    void ObjectsSpawn()
    {
        numberOfObjects = Random.Range(80, 120);       //15,30
        while (spawnedObjectsNumber < numberOfObjects && notSpawnedConsecutively < 50)
        {
            RaycastHit hit;
            Physics.Raycast(new Vector3(transform.position.x + Random.Range(minRange, maxRange), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRange, maxRange)), Vector3.down, out hit, 100, islandMask);
          
                objectRandomNumber = Random.Range(1, 5);
                if (objectRandomNumber == 1)
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 10, objectsMask);
                    if (colliders.Length == 0)
                    {
                        lastSpawned = Instantiate(object_1, hit.point, Quaternion.identity);
                        lastSpawned.transform.SetParent(hit.collider.transform);
                        notSpawnedConsecutively = 0;
                        spawnedObjectsNumber++;
                    }
                    else
                        notSpawnedConsecutively++;
                }
                else if (objectRandomNumber == 2)
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 10, objectsMask);
                    if (colliders.Length == 0)
                    {
                        lastSpawned = Instantiate(object_2, hit.point, Quaternion.identity);
                        lastSpawned.transform.SetParent(hit.collider.transform);
                        notSpawnedConsecutively = 0;
                        spawnedObjectsNumber++;
                    }
                    else
                        notSpawnedConsecutively++;
                }
                else if (objectRandomNumber == 3)
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 15, objectsMask);
                    if (colliders.Length == 0)
                    {
                        lastSpawned = Instantiate(object_3, hit.point, Quaternion.identity);
                        lastSpawned.transform.SetParent(hit.collider.transform);
                        notSpawnedConsecutively = 0;
                        spawnedObjectsNumber++;
                    }
                    else
                        notSpawnedConsecutively++;
                }
                else if (objectRandomNumber == 4)
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 5, objectsMask);
                    if (colliders.Length == 0)
                    {
                        lastSpawned = Instantiate(object_4, hit.point, Quaternion.identity);
                        lastSpawned.transform.SetParent(hit.collider.transform);
                        notSpawnedConsecutively = 0;
                        spawnedObjectsNumber++;
                }
                    else
                        notSpawnedConsecutively++;
                }
                else if (objectRandomNumber == 5)
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 10, objectsMask);
                    if (colliders.Length == 0)
                    {
                        lastSpawned = Instantiate(object_5, hit.point, Quaternion.identity);
                        lastSpawned.transform.SetParent(hit.collider.transform);
                        notSpawnedConsecutively = 0;
                        spawnedObjectsNumber++;
                    }
                    else
                        notSpawnedConsecutively++;
                }              
            
        }
    }
    


    void CollectablesSpawn()
    {
        numberOfCollectables = Random.Range(20, 30);     //20,30
        while(spawnedAlready < numberOfCollectables && notSpawnedConsecutively < 20)
        {
            RaycastHit hit;
            Physics.Raycast(new Vector3(transform.position.x + Random.Range(minRange, maxRange), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRange, maxRange)), Vector3.down, out hit, 100, islandMask);

            Collider[] colliders = Physics.OverlapSphere(hit.point, 3, collectablesMask);
            if (colliders.Length == 0)
            {
                objectRandomNumber = Random.Range(1, 4);

                if (objectRandomNumber == 1)
                {
                    lastSpawned = Instantiate(collectables_1, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    lastSpawned.transform.SetParent(hit.collider.transform);
                }
                else if (objectRandomNumber == 2)
                {
                    lastSpawned = Instantiate(collectables_2, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    lastSpawned.transform.SetParent(hit.collider.transform);
                }
                else if (objectRandomNumber == 3)
                {
                    lastSpawned = Instantiate(collectables_3, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    lastSpawned.transform.SetParent(hit.collider.transform);
                }

                notSpawnedConsecutively = 0;
                spawnedAlready++;
            }
            else
                notSpawnedConsecutively++;
            
        }
    }

    
}
