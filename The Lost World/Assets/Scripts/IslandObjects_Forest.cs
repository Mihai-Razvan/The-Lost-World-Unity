using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IslandObjects_Forest : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerMask;
    private bool CollesctablesHaveSpawned;
    private bool ObjectdHaveSpawned;
    private bool ReliefHasSpawned;
    private GameObject island;
    [SerializeField]
    public float SpawnHeight;         // se adunna la pozitia insulei si acolo spawneaza obiectele si de acolo face uin ray in jos 
    private int minRange = -350;     //de la centru insulei la ce x si z random sa se spawneze obiectele
    private int maxRange = 350;
    private int minRangeRelief = -120;   // e mai mic ca iese de pe insula
    private int maxRangeRelief = 120;
    private int minRangeBigRelief = -80;
    private int maxRangeBigRelief = 80;
    private float randomReliefScale;

    private int numberOfObjects;
    private int numberOfBigRelief;
    private int numberOfRelief;
    private int numberOfCollectables;
    private int spawnedObjectsNumber;
    private int spawnedReliefNumber;
    private int spawnedBigReliefNumber;
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
    private LayerMask Spawn_Surface_Mask;    //PE CARE se pot spawna objecturile si collectable

    [SerializeField]
    private GameObject[] Big_relief;
    [SerializeField]
    private GameObject[] Relief;
    [SerializeField]
    private GameObject[] Objects;
    [SerializeField]
    private GameObject[] Collectables;
    [SerializeField]
    private GameObject Mini_Forest_Island;


    private GameObject lastSpawned;

    private bool thingsOnIslandActive;
    [SerializeField]
    private bool islandActivated = true;

    [SerializeField]
    private GameObject[] halloweenObjects;
    

    //animals

    [SerializeField]
    private LayerMask islandMask;
    [SerializeField]
    private LayerMask animalMask;
    private float animalSphereRadius = 500;           //in asta verifica cate animale sunt
    private int maxAnimalNumber = 10;       //10
    private Vector3 spawnPoint;
    private int animalRandomNumber;     //ce animal sa spawneze
    [SerializeField]
    private GameObject forest_animal_1;   //bee


    public bool Respawned;

    private Vector3 firstMiniIslandPos;
    private Vector3 lastMiniIslandPos;

    public bool hasBuildingOnIt;
    


    private void Start()
    {/*
        if (Respawned == false)
        {
            FindObjectOfType<Save>().numberOfIslands++;
            FindObjectOfType<Save>().island_Type[FindObjectOfType<Save>().numberOfIslands] = 1;
            FindObjectOfType<Save>().island_X[FindObjectOfType<Save>().numberOfIslands] = transform.position.x;
            FindObjectOfType<Save>().island_Y[FindObjectOfType<Save>().numberOfIslands] = transform.position.y;
            FindObjectOfType<Save>().island_Z[FindObjectOfType<Save>().numberOfIslands] = transform.position.z;
        }
        */
    }

    void Update()
    {
        
        if (ReliefHasSpawned == false)
        {
            island = transform.GetChild(0).gameObject;
            ReliefHasSpawned = true;

            if (Respawned == false)
            {
                BigReliefSpawn();
                ReliefSpawn();
            }

            SpawnMiniIsland();
           // MiniIslandsBridgeSpawn();

            for (int i = 1; i < 50; i++)
                AnimalSpawn();
        }

        if (CollesctablesHaveSpawned == false)       //spawneaza cand e playeru aproape de is
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 300f, playerMask);
            if (colliders.Length != 0)
            {
                CollectablesSpawn();
                CollesctablesHaveSpawned = true;
            }
        }

        if (ObjectdHaveSpawned == false)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 700f, playerMask);       //700
            if (colliders.Length != 0)
            {
                ObjectsSpawn();
                HalloweenObjectsSpawn();
                ObjectdHaveSpawned = true;
                thingsOnIslandActive = true;
            }
        }

        AnimalSpawn();

        if (hasBuildingOnIt == false)
            DespawnIsland();

        InactiveIsland();
        IslandObjectsDeavtivateAtivate();
    }


    void BigReliefSpawn()
    {
        numberOfBigRelief = Random.Range(10, 40);
        while (spawnedBigReliefNumber < numberOfBigRelief)
        {

            RaycastHit hit;
            Physics.Raycast(new Vector3(transform.position.x + Random.Range(minRangeBigRelief, maxRangeBigRelief), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRangeBigRelief, maxRangeBigRelief)), Vector3.down, out hit, 100, Spawn_Surface_Mask);

            objectRandomNumber = Random.Range(1, 1);
            if (objectRandomNumber == 1)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 300, reliefMask);
                if (colliders.Length == 0)
                {
                    randomReliefScale = Random.Range(1, 3);
                    GameObject relief = Instantiate(Big_relief[1], hit.point, Quaternion.Euler(0, Random.Range(0, 360), 0));
                    //   relief.transform.localScale = new Vector3(randomReliefScale, randomReliefScale, randomReliefScale);
                    relief.transform.SetParent(island.transform);         //relief ca e lastspawned
                }
            }

            spawnedBigReliefNumber++;
        }
    }


    void ReliefSpawn()
    {
        numberOfRelief = Random.Range(8, 13);
        while (spawnedReliefNumber < numberOfRelief)
        {

            RaycastHit hit;
            Physics.Raycast(new Vector3(transform.position.x + Random.Range(minRangeRelief, maxRangeRelief), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRangeRelief, maxRangeRelief)), Vector3.down, out hit, 100, Spawn_Surface_Mask);

            objectRandomNumber = Random.Range(1, 4);
            if (objectRandomNumber == 1)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 25, reliefMask);
                if (colliders.Length == 0)
                {
                    randomReliefScale = Random.Range(1, 3);
                    GameObject relief = Instantiate(Relief[1], hit.point, Quaternion.identity);
                    relief.transform.localScale = new Vector3(randomReliefScale, randomReliefScale, randomReliefScale);
                    relief.transform.SetParent(island.transform);         //relief ca e lastspawned
                }
            }
            else if (objectRandomNumber == 2)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 20, reliefMask);
                if (colliders.Length == 0)
                {
                    randomReliefScale = Random.Range(1, 3);
                    GameObject relief = Instantiate(Relief[2], hit.point, Quaternion.identity);
                    relief.transform.localScale = new Vector3(randomReliefScale, randomReliefScale, randomReliefScale);
                    relief.transform.SetParent(island.transform);
                }
            }
            else if (objectRandomNumber == 3)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 15, reliefMask);
                if (colliders.Length == 0)
                {
                    randomReliefScale = Random.Range(1, 3);
                    GameObject relief = Instantiate(Relief[3], hit.point, Quaternion.identity);
                    relief.transform.localScale = new Vector3(randomReliefScale, randomReliefScale, randomReliefScale);
                    relief.transform.SetParent(island.transform);
                }
            }
            else if (objectRandomNumber == 4)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 20, reliefMask);
                if (colliders.Length == 0)
                {
                    randomReliefScale = Random.Range(1, 3);
                    GameObject relief = Instantiate(Relief[4], hit.point, Quaternion.identity);
                    relief.transform.localScale = new Vector3(randomReliefScale, randomReliefScale, randomReliefScale);
                    relief.transform.SetParent(island.transform);
                }
            }

            spawnedReliefNumber++;
        }
    }


    void ObjectsSpawn()
    {
        spawnedObjectsNumber = 0;
        notSpawnedConsecutively = 0;
        numberOfObjects = Random.Range(80, 120);       //80 120
        while (spawnedObjectsNumber < numberOfObjects && notSpawnedConsecutively < 50)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(new Vector3(transform.position.x + Random.Range(minRange, maxRange), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRange, maxRange)), Vector3.down, out hit, 100, Spawn_Surface_Mask) && hit.normal.x > -40 && hit.normal.x < 40 && hit.normal.z > -40 && hit.normal.z < 40)
            {
                objectRandomNumber = Random.Range(1, 5);
                if (objectRandomNumber == 1)
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 10, objectsMask);
                    if (colliders.Length == 0)
                    {
                        lastSpawned = Instantiate(Objects[1], hit.point, Quaternion.identity);
                        lastSpawned.transform.SetParent(island.transform);
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
                        lastSpawned = Instantiate(Objects[2], hit.point, Quaternion.identity);
                        lastSpawned.transform.SetParent(island.transform);
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
                        lastSpawned = Instantiate(Objects[3], hit.point, Quaternion.identity);
                        lastSpawned.transform.SetParent(island.transform);
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
                        lastSpawned = Instantiate(Objects[4], hit.point, Quaternion.identity);
                        lastSpawned.transform.SetParent(island.transform);
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
                        lastSpawned = Instantiate(Objects[5], hit.point, Quaternion.identity);
                        lastSpawned.transform.SetParent(island.transform);
                        notSpawnedConsecutively = 0;
                        spawnedObjectsNumber++;
                    }
                    else
                        notSpawnedConsecutively++;
                }
            }
        }
    }



    void CollectablesSpawn()
    {
        spawnedAlready = 0;
        notSpawnedConsecutively = 0;
        numberOfCollectables = Random.Range(80, 150);     //20,30
        while (spawnedAlready < numberOfCollectables && notSpawnedConsecutively < 50)
        {
            RaycastHit hit;
            Physics.Raycast(new Vector3(transform.position.x + Random.Range(minRange, maxRange), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRange, maxRange)), Vector3.down, out hit, 100, Spawn_Surface_Mask);
            if (hit.normal.x > -40 && hit.normal.x < 40 && hit.normal.z > -40 && hit.normal.z < 40)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 3, collectablesMask);
                if (colliders.Length == 0)
                {
                    objectRandomNumber = (int)Random.Range(1, 100);

                    if (objectRandomNumber <= 25)
                    {
                        lastSpawned = Instantiate(Collectables[1], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        lastSpawned.transform.SetParent(island.transform);
                    }
                    else if (objectRandomNumber <= 55)
                    {
                        lastSpawned = Instantiate(Collectables[2], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        lastSpawned.transform.SetParent(island.transform);
                    }
                    else if (objectRandomNumber <= 80)
                    {
                        lastSpawned = Instantiate(Collectables[3], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        lastSpawned.transform.SetParent(island.transform);
                    }
                    else 
                    {
                        lastSpawned = Instantiate(Collectables[4], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        lastSpawned.transform.SetParent(island.transform);
                    }


                    notSpawnedConsecutively = 0;
                    spawnedAlready++;
                }
                else
                    notSpawnedConsecutively++;
            }

        }


        HalloweenCollectablesSpawn();
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
                    // island.transform.GetChild(i).gameObject.SetActive(false);
                    if (island.transform.GetChild(i).gameObject.layer != 10)
                        Destroy(island.transform.GetChild(i).gameObject);


                //for (int j = 1; j < transform.childCount; j++)     //despawneaza miniislandurile care sunt puse la punctu pe care e pusa insula insulei
                  //  Destroy(transform.GetChild(j).gameObject);
            }
        }
        else if (thingsOnIslandActive == false)   //reactivate objects
        {
            thingsOnIslandActive = true;

            // for (int i = 0; i < island.transform.childCount; i++)
            //island.transform.GetChild(i).gameObject.SetActive(true);

            ObjectsSpawn();
            HalloweenObjectsSpawn();
            CollectablesSpawn();
        }
    }



    void DespawnIsland()     //daca e la sit mare dispare de tot CU TOT CU PUNCT
    {
        Collider[] despawnIslandCollider = Physics.OverlapSphere(transform.position, 3000f, playerMask);  //3000
        if (despawnIslandCollider.Length == 0)
            Destroy(gameObject);
    }

    void InactiveIsland()   //daca e la dist medie dezactiveaza is NU PUNCTUL
    {
        Collider[] despawnIslandCollider = Physics.OverlapSphere(transform.position, 1500f, playerMask);  //1500
        if (despawnIslandCollider.Length == 0)
        {
            if (islandActivated == true)
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).tag == "MiniIsland")
                        Destroy(transform.GetChild(i).gameObject);
                    else
                        transform.GetChild(i).gameObject.SetActive(false);
                }

            islandActivated = false;
        }
        else if (islandActivated == false)
        {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(true);

            SpawnMiniIsland();
            islandActivated = true;
        }

    }



    void SpawnMiniIsland()
    {
        /*
        int number;
        int distance;
        if (SceneManager.GetActiveScene().name == "Menu")      // asta ca sa spawneze mai multe miniislanduri pe insula din main menu
        {
            number = 10;   //15
            distance = 220;  //200
        }
        else
        {
            number = 10;
            distance = 220;
        }
        */

        for (int i = 1; i <= 10; i++)
        {
            float y = 15 * i;                //asta nu e random ca e ca sa nu se ciocneasca mini insulele
            if ((int)Random.Range(1, 3) == 1)
                y = -y;

            float z = Random.Range(0, 200);

            if ((int)Random.Range(1, 3) == 1)
                z = -z;

            float x = Random.Range(0, 200);

            if ((int)Random.Range(1, 3) == 1)
                x = -x;

            if (Vector3.Distance(transform.position, new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z)) > 220)  // sa nu spawneze in insula
            {
                GameObject spawnedMiniIsland = Instantiate(Mini_Forest_Island, new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z), Quaternion.identity);
                spawnedMiniIsland.transform.SetParent(this.gameObject.transform);
            }
        }
    }



    void AnimalSpawn()
    {      //iau playeru de la inventory in loc sa il fac variabila aici ca insula fiind prefab nu merge sa dau drag din hierchy in inspector,pot numai prefaburi sa dau drag in inspector
        RaycastHit hit;
        if (Physics.Raycast(FindObjectOfType<Inventory>().player.transform.position, -transform.up, out hit, 20, islandMask))    //daca playeru e pe insula altfel nu spanweza animale
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, animalSphereRadius, animalMask);
            if (colliders.Length < maxAnimalNumber)      //verifica sa nu fie prea mult animale pe un radius in juru playerului(adik pe insula)
            {
                spawnPoint = Random.insideUnitSphere * animalSphereRadius + new Vector3(transform.position.x, transform.position.y + SpawnHeight, transform.position.z);

                RaycastHit hit2;
                if (Physics.Raycast(spawnPoint, -transform.up, out hit2, 50, islandMask))   // sa nu spawneze animale in afara insulei
                {
                    if (hit.collider.tag != "MiniIsland")                  //sa nu spawneze si pe miniislanduri ca alea tot Island au layeru si intra in islandMask
                    {
                        animalRandomNumber = Random.Range(1, 1);
                        if (animalRandomNumber == 1)     //bee
                            Instantiate(forest_animal_1, spawnPoint, Quaternion.identity);
                    }

                }

            }
        }
    }


    void MiniIslandsBridgeSpawn()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1000f, islandMask);

        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].tag == "Island Point Type 1" || colliders[i].tag == "Island Point Type 2" || colliders[i].tag == "Island Point Type 3" && (Mathf.Abs(transform.position.y - colliders[i].transform.position.y) < Mathf.Abs(transform.position.x - colliders[i].transform.position.x) || Mathf.Abs(transform.position.y - colliders[i].transform.position.y) < Mathf.Abs(transform.position.z - colliders[i].transform.position.z)))
            {//asta cu Math.abs e ca daca diferenta dintre Y e cea mai mica diff si nu e mai mica dif dintr Z sau X nu calculeaza bn last island adica bug
                Vector3 firstdirection = (colliders[i].gameObject.transform.position - transform.position).normalized;
                for(int j = 0; j <= 100; j++)
                {
                    Vector3 point = transform.position + firstdirection * j * 50;

                    if(Physics.Raycast(new Vector3(point.x, point.y + 100, point.z), - transform.up, 200f, islandMask) == false)
                    {
                        GameObject firstMiniIsland = Instantiate(Mini_Forest_Island, new Vector3(point.x, transform.position.y - 10, point.z), Quaternion.identity);
                        firstMiniIsland.transform.SetParent(this.gameObject.transform);
                        firstMiniIslandPos = new Vector3(point.x, transform.position.y - 10, point.z);
                        break;
                    }              
                }

                
                Vector3 lastdirection = (transform.position - colliders[i].gameObject.transform.position).normalized;
                for (int j = 0; j <= 100; j++)
                {
                    Vector3 point = colliders[i].gameObject.transform.position + lastdirection * j * 50;

                    if (Physics.Raycast(new Vector3(point.x, point.y + 100, point.z), -transform.up, 200f, islandMask) == false)
                    {
                        GameObject lastMiniIsland = Instantiate(Mini_Forest_Island, new Vector3(point.x, colliders[i].gameObject.transform.position.y, point.z), Quaternion.identity);
                        lastMiniIsland.transform.SetParent(colliders[i].gameObject.transform);
                        lastMiniIslandPos = new Vector3(point.x, colliders[i].gameObject.transform.position.y, point.z);
                        break;
                    }
                }


                Vector3 direction = (lastMiniIslandPos - firstMiniIslandPos).normalized;

                for (int j = 0; ; j++)
                {
                    Vector3 point = firstMiniIslandPos + direction * j * 50;

                    if (Vector3.Distance(lastMiniIslandPos, point) > 40)
                    {
                        GameObject spawned = Instantiate(Mini_Forest_Island, point, Quaternion.identity);
                        spawned.transform.SetParent(this.gameObject.transform);
                    }
                    else
                        break;
                }

            }
        }
    }


    private void HalloweenCollectablesSpawn()
    {
        spawnedAlready = 0;
        notSpawnedConsecutively = 0;
        numberOfCollectables = Random.Range(45, 70);     
        while (spawnedAlready < numberOfCollectables && notSpawnedConsecutively < 50)
        {
            RaycastHit hit;
            Physics.Raycast(new Vector3(transform.position.x + Random.Range(minRange, maxRange), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRange, maxRange)), Vector3.down, out hit, 100, Spawn_Surface_Mask);
            if (hit.normal.x > -40 && hit.normal.x < 40 && hit.normal.z > -40 && hit.normal.z < 40)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 3, collectablesMask);
                if (colliders.Length == 0)
                {
                    lastSpawned = Instantiate(Collectables[5], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    lastSpawned.transform.SetParent(island.transform);

                    notSpawnedConsecutively = 0;
                    spawnedAlready++;
                }
                else
                    notSpawnedConsecutively++;
            }

        }
    }

    /////////////////////////////////// HALLOWEEN/////////////////
    ///

    void HalloweenObjectsSpawn()
    {
        int halloweenSpawnedNum = 0;
        int numOfHalloweenObjects = Random.Range(150, 190);       
        while (halloweenSpawnedNum < numOfHalloweenObjects)
        {
            RaycastHit hit;

            if (Physics.Raycast(new Vector3(transform.position.x + Random.Range(minRange, maxRange), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRange, maxRange)), Vector3.down, out hit, 100, Spawn_Surface_Mask) && hit.normal.x > -40 && hit.normal.x < 40 && hit.normal.z > -40 && hit.normal.z < 40)
            {
                int objectRandomNumber = Random.Range(1, halloweenObjects.Length);
                Debug.Log(objectRandomNumber);
                Collider[] colliders = Physics.OverlapSphere(hit.point, 10, objectsMask);
                if (colliders.Length == 0)
                {
                     lastSpawned = Instantiate(halloweenObjects[objectRandomNumber], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                     lastSpawned.transform.SetParent(island.transform);
                     halloweenSpawnedNum++;
                }
            }
        }
    }
}
