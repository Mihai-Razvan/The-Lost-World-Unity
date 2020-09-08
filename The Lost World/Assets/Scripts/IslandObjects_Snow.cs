using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandObjects_Snow : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerMask;
    private bool CollesctablesHaveSpawned;
    private bool ObjectdHaveSpawned;
    private bool ReliefHasSpawned;
    private GameObject island;
    [SerializeField]
    public float SpawnHeight;         // se adunna la pozitia insulei si acolo spawneaza obiectele si de acolo face uin ray in jos 
    private int minRange = -300;     //de la centru insulei la ce x si z random sa se spawneze obiectele
    private int maxRange = 300;
    private int minRangeRelief = -120;   // e mai mic ca iese de pe insula
    private int maxRangeRelief = 120;
    private int minRangeBigRelief = -40;
    private int maxRangeBigRelief = 40;
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
    private LayerMask collectablesMask;      //cand faca sfera in juru obiectului inainte sa instantiate sa vada daca se colliding cu cv cu care nu are voie adik collectable sau tree
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

    private GameObject lastSpawned;

    private bool thingsOnIslandActive;
    [SerializeField]
    private bool islandActivated = true;
    private bool IsCaveOnIsland;

    [SerializeField]
    private GameObject Mini_Snow_Island;



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
    private GameObject snow_animal_1;   //bee




    void Update()
    {
        if (ReliefHasSpawned == false)
        {
            island = transform.GetChild(0).gameObject;
            ReliefHasSpawned = true;
            BigReliefSpawn();
            ReliefSpawn();
            SpawnMiniIsland();

          //  for (int i = 1; i < 50; i++)
             //   AnimalSpawn();
        }

        if (CollesctablesHaveSpawned == false)       //spawneaza cand e playeru aproape de is
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 200f, playerMask);
            if (colliders.Length != 0)
            {
                CollectablesSpawn();
                if (IsCaveOnIsland)
                {
                    CaveCollectablesSpawn();
                    CaveObjectSpawn();
                }
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

        AnimalSpawn();

        DespawnIsland();
        InactiveIsland();
        IslandObjectsDeavtivateAtivate();
    }


    void BigReliefSpawn()
    {
        notSpawnedConsecutively = 0;
        numberOfBigRelief = Random.Range(10, 40);
        while (spawnedBigReliefNumber < numberOfBigRelief && notSpawnedConsecutively < 20)
        {

            RaycastHit hit;
            
            if(Physics.Raycast(new Vector3(transform.position.x + Random.Range(minRangeBigRelief, maxRangeBigRelief), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRangeBigRelief, maxRangeBigRelief)), Vector3.down, out hit, 100, Spawn_Surface_Mask))
            {
                objectRandomNumber = (int)Random.Range(1, 3);
                if (objectRandomNumber == 1)
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 300, reliefMask);
                    if (colliders.Length == 0)
                    {
                        randomReliefScale = Random.Range(1, 3);
                        GameObject relief = Instantiate(Big_relief[1], hit.point, Quaternion.Euler(0, Random.Range(0, 360), 0));
                        //   relief.transform.localScale = new Vector3(randomReliefScale, randomReliefScale, randomReliefScale);
                        relief.transform.SetParent(hit.collider.transform);         //relief ca e lastspawned
                    }
                }
                else       //MUNTELE CU CAVE
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 300, reliefMask);
                    if (colliders.Length == 0)
                    {
                        float yRotation = Random.Range(0, 360);

                        GameObject relief = Instantiate(Big_relief[2], hit.point, Quaternion.Euler(0, yRotation, 0));     //cave base
                        relief.transform.SetParent(hit.collider.transform);

                        relief = Instantiate(Big_relief[3], hit.point, Quaternion.Euler(0, yRotation, 0));     //cave over
                        relief.transform.SetParent(hit.collider.transform);

                        IsCaveOnIsland = true;
                    }
                }

                spawnedBigReliefNumber++;
            }
            else
                notSpawnedConsecutively++;
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
                    relief.transform.SetParent(hit.collider.transform);         //relief ca e lastspawned
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
                    relief.transform.SetParent(hit.collider.transform);
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
                    relief.transform.SetParent(hit.collider.transform);
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
                    relief.transform.SetParent(hit.collider.transform);
                }
            }

            spawnedReliefNumber++;
        }
    }


    void ObjectsSpawn()
    {
        spawnedObjectsNumber = 0;
        notSpawnedConsecutively = 0;
        numberOfObjects = Random.Range(80, 120);       //15,30
        while (spawnedObjectsNumber < numberOfObjects && notSpawnedConsecutively < 50)
        {
            RaycastHit hit;
            Physics.Raycast(new Vector3(transform.position.x + Random.Range(minRange, maxRange), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRange, maxRange)), Vector3.down, out hit, 100, Spawn_Surface_Mask);
            if (hit.normal.x > -40 && hit.normal.x < 40 && hit.normal.z > -40 && hit.normal.z < 40)
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
        numberOfCollectables = Random.Range(50, 80);     //20,30
        while (spawnedAlready < numberOfCollectables && notSpawnedConsecutively < 20)
        {
            RaycastHit hit;
            Physics.Raycast(new Vector3(transform.position.x + Random.Range(minRange, maxRange), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRange, maxRange)), Vector3.down, out hit, 100, Spawn_Surface_Mask);
            if (hit.normal.x > -40 && hit.normal.x < 40 && hit.normal.z > -40 && hit.normal.z < 40)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 3, collectablesMask);
                if (colliders.Length == 0)
                {
                    objectRandomNumber = (int)Random.Range(1, 5);

                    if (objectRandomNumber == 1)
                    {
                        lastSpawned = Instantiate(Collectables[1], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        lastSpawned.transform.SetParent(hit.collider.transform);
                    }
                    else if (objectRandomNumber == 2)
                    {
                        lastSpawned = Instantiate(Collectables[2], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        lastSpawned.transform.SetParent(hit.collider.transform);
                    }
                    else if (objectRandomNumber == 3)
                    {
                        lastSpawned = Instantiate(Collectables[3], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        lastSpawned.transform.SetParent(hit.collider.transform);
                    }
                    else if (objectRandomNumber == 4)
                    {
                        lastSpawned = Instantiate(Collectables[4], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
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
                    if (island.transform.GetChild(i).gameObject.tag != "Relief" && island.transform.GetChild(i).gameObject.tag != "Cave Base")
                        Destroy(island.transform.GetChild(i).gameObject);


                for (int j = 1; j < transform.childCount; j++)     //despawneaza miniislandurile care sunt puse la punctu pe care e pusa insula insulei
                    Destroy(transform.GetChild(j).gameObject);
            }
        }
        else if (thingsOnIslandActive == false)   //reactivate objects
        {
            thingsOnIslandActive = true;

            // for (int i = 0; i < island.transform.childCount; i++)
            //island.transform.GetChild(i).gameObject.SetActive(true);

            ObjectsSpawn();
            CollectablesSpawn();
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
            if (islandActivated == true)
                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).gameObject.SetActive(false);

        }
        else if (islandActivated == false)
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(true);

    }


    void CaveCollectablesSpawn()
    {
        spawnedAlready = 0;
        notSpawnedConsecutively = 0;
        numberOfCollectables = Random.Range(20, 50);     //20,30
        while (spawnedAlready < numberOfCollectables && notSpawnedConsecutively < 20)
        {
            RaycastHit[] hit = Physics.RaycastAll(new Vector3(transform.position.x + Random.Range(minRange, maxRange), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRange, maxRange)), Vector3.down, 100, Spawn_Surface_Mask);

            for(int i = 0; i < hit.Length; i ++)
                if (hit[i].collider.tag == "Cave Base")
                {
                    Collider[] colliders = Physics.OverlapSphere(hit[i].point, 3, collectablesMask);
                    if (colliders.Length == 0)
                    {
                        objectRandomNumber = (int)Random.Range(1, 50);

                        if (objectRandomNumber == 1)
                        {
                            lastSpawned = Instantiate(Collectables[1], hit[i].point, Quaternion.FromToRotation(Vector3.up, hit[i].normal));
                            lastSpawned.transform.SetParent(hit[i].collider.transform);
                        }
                        else if (objectRandomNumber == 2)
                        {
                            lastSpawned = Instantiate(Collectables[2], hit[i].point, Quaternion.FromToRotation(Vector3.up, hit[i].normal));
                            lastSpawned.transform.SetParent(hit[i].collider.transform);
                        }
                        else if (objectRandomNumber == 3)
                        {
                            lastSpawned = Instantiate(Collectables[3], hit[i].point, Quaternion.FromToRotation(Vector3.up, hit[i].normal));
                            lastSpawned.transform.SetParent(hit[i].collider.transform);
                        }
                        else if (objectRandomNumber == 4)
                        {
                            lastSpawned = Instantiate(Collectables[4], hit[i].point, Quaternion.FromToRotation(Vector3.up, hit[i].normal));
                            lastSpawned.transform.SetParent(hit[i].collider.transform);
                        }
                        else 
                        {
                            lastSpawned = Instantiate(Collectables[5], hit[i].point, Quaternion.FromToRotation(Vector3.up, hit[i].normal));
                            lastSpawned.transform.SetParent(hit[i].collider.transform);
                        }


                        notSpawnedConsecutively = 0;
                        spawnedAlready++;
                    }
                    else
                        notSpawnedConsecutively++;
                }

        }
    }


    void CaveObjectSpawn()
    {
        spawnedAlready = 0;
        notSpawnedConsecutively = 0;
        numberOfObjects = Random.Range(10, 30);     //20,30
        while (spawnedAlready < numberOfObjects && notSpawnedConsecutively < 20)
        {
            RaycastHit[] hit = Physics.RaycastAll(new Vector3(transform.position.x + Random.Range(minRange, maxRange), transform.position.y + SpawnHeight, transform.position.z + Random.Range(minRange, maxRange)), Vector3.down, 100, Spawn_Surface_Mask);

            for (int i = 0; i < hit.Length; i++)
                if (hit[i].collider.tag == "Cave Base")
                {
                    Collider[] colliders = Physics.OverlapSphere(hit[i].point, 3, collectablesMask);
                    if (colliders.Length == 0)
                    {
                        objectRandomNumber = (int)Random.Range(1, 6);

                        if (objectRandomNumber == 1)
                        {
                            lastSpawned = Instantiate(Objects[6], hit[i].point, Quaternion.FromToRotation(Vector3.up, hit[i].normal));
                            lastSpawned.transform.SetParent(hit[i].collider.transform);
                        }
                        else if (objectRandomNumber == 2)
                        {
                            lastSpawned = Instantiate(Objects[7], hit[i].point, Quaternion.FromToRotation(Vector3.up, hit[i].normal));
                            lastSpawned.transform.SetParent(hit[i].collider.transform);
                        }
                        else if (objectRandomNumber > 2) //turturii care pot fi pusi si pe tavan
                        {
                            RaycastHit hit2;
                            Physics.Raycast(hit[i].point, transform.up, out hit2, 50, Spawn_Surface_Mask);

                            if(hit2.collider.gameObject.tag == "Cave Over")
                            {
                                lastSpawned = Instantiate(Objects[8], hit2.point, Quaternion.Euler(180, 0, 0));
                                lastSpawned.transform.SetParent(hit2.collider.transform);
                            }
                            
                        }

                        notSpawnedConsecutively = 0;
                        spawnedAlready++;
                    }
                    else
                        notSpawnedConsecutively++;
                }

        }
    }

    void SpawnMiniIsland()
    {
        for (int i = 1; i <= 18; i++)
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
                GameObject spawnedMiniIsland = Instantiate(Mini_Snow_Island, new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z), Quaternion.identity);
                spawnedMiniIsland.transform.SetParent(this.gameObject.transform);
            }
        }
    }


    void AnimalSpawn()
    {      //iau playeru de la inventory in loc sa il fac variabila aici ca insula fiind prefab nu merge sa dau drag din hierchy in inspector,pot numai prefaburi sa dau drag in inspector
        RaycastHit hit;
        if (Physics.Raycast(FindObjectOfType<Inventory>().player.transform.position, -transform.up, out hit, 20, islandMask))    //daca playeru e pe insula altfel nu spanweza animale
        {
            Collider[] colliders = Physics.OverlapSphere(FindObjectOfType<Inventory>().player.transform.position, animalSphereRadius, animalMask);
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
                            Instantiate(snow_animal_1, spawnPoint, Quaternion.identity);
                    }

                }

            }
        }
    }
}
