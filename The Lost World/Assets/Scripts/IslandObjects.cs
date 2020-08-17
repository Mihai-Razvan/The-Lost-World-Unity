using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandObjects : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerMask;
    [SerializeField]
    private bool objectsHasSpawned;
    [SerializeField]
    private int island_Relief_Radius;                             //cat e sfera deasupra insulei cand se spawneaza reliefu
    [SerializeField]
    private int island_Objects_Radius;                            //cat e sfera deasupra insulei cand se spawneaza obiectele
    [SerializeField]
    private int island_Collectables_Radius;
    [SerializeField]
    private int islandHeight;
    private int numberOfObjects;
    private int numberOfRelief;
    private int numberOfCollectables;
    private int spawnedObjectsNumber;
    private int spawnedReliefNumeber;
    private int objectRandomNumber;
    private int notSpawnedConsecutively;
    private int spawnedAlready;
    [SerializeField]
    private int minimIslandRadius;                //sa nu spawneze in afara insulei
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
   

   
    void Start()
    {   ///////////////////////////// METODA PATRATULUI ////////////////////////////////////////


        /* for (int i = (int)transform.position.x - islandRadius; i <= (int)transform.position.x + islandRadius; i+=15)
             for (int j = (int)transform.position.z - islandRadius; j <= (int)transform.position.z + islandRadius; j+=15)
             {

                 RaycastHit hit;
                 if(Physics.Raycast(new Vector3(i, transform.position.y + islandHeight, j), Vector3.down, out hit, islandMask))
                 {

                     Instantiate(stone_1, new Vector3(i, hit.point.y, j), Quaternion.Euler(-90f, 0f, 0f));
                 }
             } */


        /////////////////////////// MERODA SFEREI //////////////////////////////////////////
        ///
        ReliefSpawn();
        ObjectsSpawn();

        Collider[] colliders = Physics.OverlapSphere(transform.position, 300f, playerMask);
        if (colliders.Length != 0)
        {
            CollectablesSpawn();
            objectsHasSpawned = true;
        }

    }

    
    void Update()
    {
        if(objectsHasSpawned == false)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 300f, playerMask);
            if (colliders.Length != 0)
            {
                CollectablesSpawn();
                objectsHasSpawned = true;
            }
        }
    }


    void ReliefSpawn()
    {
        numberOfRelief = Random.Range(2, 5);
        while (spawnedReliefNumeber < numberOfRelief)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * island_Relief_Radius + new Vector3(transform.position.x, transform.position.y + islandHeight, transform.position.z);

            RaycastHit hit;
            Physics.Raycast(spawnPosition, Vector3.down, out hit, islandMask);

            objectRandomNumber = Random.Range(1, 4);
            if (objectRandomNumber == 1)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 25, reliefMask);
                if (colliders.Length == 0)
                    Instantiate(relief_1, hit.point, Quaternion.identity);
            }
            else if (objectRandomNumber == 2)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 20, reliefMask);
                if (colliders.Length == 0)
                    Instantiate(relief_2, hit.point, Quaternion.identity);
            }
            else if (objectRandomNumber == 3)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 15, reliefMask);
                if (colliders.Length == 0)
                    Instantiate(relief_3, hit.point, Quaternion.identity);
            }
            else if (objectRandomNumber == 4)
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, 20, reliefMask);
                if (colliders.Length == 0)
                    Instantiate(relief_4, hit.point, Quaternion.identity);
            }

            spawnedReliefNumeber++;
        }
    }


    void ObjectsSpawn()
    {
        numberOfObjects = Random.Range(15, 30);
        while (spawnedObjectsNumber < numberOfObjects && notSpawnedConsecutively < 30)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * island_Objects_Radius + new Vector3(transform.position.x, transform.position.y + islandHeight, transform.position.z);

            if (Mathf.Abs(spawnPosition.x - transform.position.x) < minimIslandRadius && Mathf.Abs(spawnPosition.z - transform.position.z) < minimIslandRadius)

            {
                RaycastHit hit;
                Physics.Raycast(spawnPosition, Vector3.down, out hit, islandMask);

                objectRandomNumber = Random.Range(1, 5);
                if (objectRandomNumber == 1)
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 15, objectsMask);
                    if (colliders.Length == 0)
                    {
                        Instantiate(object_1, hit.point, Quaternion.identity);
                        notSpawnedConsecutively = 0;
                    }
                    else
                        notSpawnedConsecutively++;
                }
                else if (objectRandomNumber == 2)
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 15, objectsMask);
                    if (colliders.Length == 0)
                    {
                        Instantiate(object_2, hit.point, Quaternion.identity);
                        notSpawnedConsecutively = 0;
                    }
                    else
                        notSpawnedConsecutively++;
                }
                else if (objectRandomNumber == 3)
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 20, objectsMask);
                    if (colliders.Length == 0)
                    {
                        Instantiate(object_3, hit.point, Quaternion.identity);
                        notSpawnedConsecutively = 0;
                    }
                    else
                        notSpawnedConsecutively++;
                }
                else if (objectRandomNumber == 4)
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 10, objectsMask);
                    if (colliders.Length == 0)
                    {
                        Instantiate(object_4, hit.point, Quaternion.identity);
                        notSpawnedConsecutively = 0;
                    }
                    else
                        notSpawnedConsecutively++;
                }
                else if (objectRandomNumber == 5)
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, 15, objectsMask);
                    if (colliders.Length == 0)
                    {
                        Instantiate(object_5, hit.point, Quaternion.identity);
                        notSpawnedConsecutively = 0;
                    }
                    else
                        notSpawnedConsecutively++;
                }

                spawnedObjectsNumber++;
            }
        }
    }
    


    void CollectablesSpawn()
    {
        numberOfCollectables = Random.Range(30, 50);
        while(spawnedAlready < numberOfCollectables && notSpawnedConsecutively < 20)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * island_Collectables_Radius + new Vector3(transform.position.x, transform.position.y + islandHeight, transform.position.z);

            if (Mathf.Abs(spawnPosition.x - transform.position.x) < minimIslandRadius && Mathf.Abs(spawnPosition.z - transform.position.z) < minimIslandRadius)

            {
                RaycastHit hit;
                Physics.Raycast(spawnPosition, Vector3.down, out hit, islandMask);

                Collider[] colliders = Physics.OverlapSphere(hit.point, 3, collectablesMask);
                if (colliders.Length == 0)
                {
                    objectRandomNumber = Random.Range(1, 5);

                    if (objectRandomNumber <= 2)
                        Instantiate(collectables_1, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    else if (objectRandomNumber == 3)
                        Instantiate(collectables_2, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    else if (objectRandomNumber == 4)
                        Instantiate(collectables_3, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

                    notSpawnedConsecutively = 0;
                    spawnedAlready ++ ;
                }
                else
                    notSpawnedConsecutively++;
            }
        }
    }
}
