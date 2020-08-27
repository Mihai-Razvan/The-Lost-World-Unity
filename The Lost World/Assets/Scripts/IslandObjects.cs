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
    private GameObject island;
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

    private bool thingsOnIslandActive;
    [SerializeField]
    private bool islandActivated = true;

    void Start()
    {
        ReliefSpawn();  
    }

    
    void Update()
    {
        if(CollesctablesHaveSpawned == false)       //spawneaza cand e playeru aproape de is
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
            Collider[] colliders = Physics.OverlapSphere(transform.position, 450f, playerMask);       //450
            if (colliders.Length != 0)
            {
                ObjectsSpawn();
                ObjectdHaveSpawned = true;
                thingsOnIslandActive = true;
            }
        }


        DespawnIsland();
        InactiveIsland();
        IslandObjectsDeavtivateAtivate();                          
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
                    relief.transform.SetParent(hit.collider.transform);         //relief ca e lastspawned
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
                    relief.transform.SetParent(hit.collider.transform);
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
                    relief.transform.SetParent(hit.collider.transform);
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
                    relief.transform.SetParent(hit.collider.transform);
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
        numberOfCollectables = Random.Range(200, 300);     //20,30
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


    void IslandObjectsDeavtivateAtivate()
    {
        Collider[] despawnObjectsCollider = Physics.OverlapSphere(transform.position, 600f, playerMask);      //600
        if (despawnObjectsCollider.Length == 0) //deactivate objects
        {
            if (thingsOnIslandActive == true)
            {
                thingsOnIslandActive = false;

                for (int i = 0; i < island.transform.childCount; i++)
                    island.transform.GetChild(i).gameObject.SetActive(false);


                  for (int j = 1; j < transform.childCount; j++)     //despawneaza miniislandurile care sunt puse la punctu pe care e pusa insula insulei
                  Destroy(transform.GetChild(j).gameObject);
            }
        }
        else if (thingsOnIslandActive == false)   //reactivate objects
        {
            thingsOnIslandActive = true;

            for (int i = 0; i < island.transform.childCount; i++)
                island.transform.GetChild(i).gameObject.SetActive(true);
        }
    }



    void DespawnIsland()     //daca e la sit mare dispare de tot CU TOT CU PUNCT
    {
        Collider[] despawnIslandCollider = Physics.OverlapSphere(transform.position, 10000f, playerMask);   
        if (despawnIslandCollider.Length == 0)
            Destroy(gameObject);
    }

    void InactiveIsland()   //daca e la dist medie dezactiveaza is NU PUNCTUL
    {
        Collider[] despawnIslandCollider = Physics.OverlapSphere(transform.position, 1500f, playerMask);
        if (despawnIslandCollider.Length == 0)
        {
            if(islandActivated == true)
                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).gameObject.SetActive(false);
        }
        else if (islandActivated == false )
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(true);

    }
    
}
