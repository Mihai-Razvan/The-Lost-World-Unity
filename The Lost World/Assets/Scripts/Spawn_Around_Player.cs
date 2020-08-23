using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Around_Player : MonoBehaviour
{
    /// ISLANDS                                   

    private float IslandSpawnsphereRadius = 1000f;       //750     //sfera in care se spawneza insulele
    private float IslanSphereRadius = 700f;   //300    //sfera unei insule cand se alege punctu de spawn verifica sa nu fie alta insula in sfera aia
    private int randomIslandNumber;
    private GameObject spawnedIsland;
    [SerializeField]
    GameObject island_forest_1;
    [SerializeField]
    GameObject island_forest_2;
    [SerializeField]
    GameObject island_forest_3;
    [SerializeField]
    GameObject island_forest_4;
    [SerializeField]
    GameObject island_forest_5;
    [SerializeField]
    GameObject island_forest_6;
    [SerializeField]
    GameObject island_forest_7;
    [SerializeField]
    GameObject island_forest_8;
    [SerializeField]
    GameObject island_forest_9;
    [SerializeField]
    GameObject island_forest_10;
    [SerializeField]
    GameObject island_snow_1;
                                                                      
    //CLOUDS                                                              

    [SerializeField]
    private LayerMask islandMask;
    [SerializeField]
    private LayerMask animalMask;
    private float animalSphereRadius = 500;           //in asta verifica cate animale sunt
    private int maxAnimalNumber = 10;
    private Vector3 spawnPoint;
    private int animalRandomNumber;     //ce animal sa spawneze
    [SerializeField]
    private GameObject animal_1;   //bee
             
    ///CLOUDS
  
    [SerializeField]
    private LayerMask cloudMask;
    private int cloudSphereRadius = 800;
    [SerializeField]
    private GameObject cloud;

    ///MINI ISLANDS

    private GameObject spawnedMiniIsland;
    [SerializeField]
    private GameObject Mini_Forest_Island;


    void Start()
    {
        spawnedIsland = Instantiate(island_forest_1, new Vector3(2315, 0 ,1000), Quaternion.identity);    //spawn island
        SpawnMiniIsland();

        for (int i = 1; i <= 50; i++)         //spawneaza nori la inceput 
        {
            IslandSpawn();
           // AnimalSpawn();
            CloudsSpawn();
        }
        
    }


    void Update()
    {
        IslandSpawn();
       // AnimalSpawn();
        if((int) Random.Range(1, 1000) == 1)
           CloudsSpawn();

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1000f, islandMask);      //activeaza insulele din apropiere; dezactivarea insulelor se face din scriptu lor
        for (int i = 0; i < colliders.Length; i++)
            colliders[i].gameObject.SetActive(true);
       
    }

    void IslandSpawn()
    {
        Vector3 spawnPosition = Random.insideUnitSphere * IslandSpawnsphereRadius + new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Collider[] colliders = Physics.OverlapSphere(spawnPosition, IslanSphereRadius);
        if (colliders.Length == 0)
        {
            randomIslandNumber = Random.Range(1, 4) ;
            if (randomIslandNumber < 3)
              spawnedIsland =  Instantiate(island_forest_1, spawnPosition, Quaternion.identity);
            else
              spawnedIsland = Instantiate(island_snow_1, spawnPosition, Quaternion.identity);

            SpawnMiniIsland();
        }
    }


    void AnimalSpawn()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 20, islandMask))    //daca playeru e pe insula altfel nu spanweza animale
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, animalSphereRadius, animalMask);
            if (colliders.Length < maxAnimalNumber)      //verifica sa nu fie prea mult animale pe un radius in juru playerului(adik pe insula)
            {
                spawnPoint = Random.insideUnitSphere * animalSphereRadius + transform.position;

                RaycastHit hit2;
                if (Physics.Raycast(spawnPoint, -transform.up, out hit2 , 50, islandMask))   // sa nu spawneze animale in afara insulei
                {
                    if (hit.collider.tag != "MiniIsland")                  //sa nu spawneze si pe miniislanduri ca alea tot Island au layeru si intra in islandMask
                    {
                        animalRandomNumber = Random.Range(1, 1);
                        if (animalRandomNumber == 1)     //bee
                            Instantiate(animal_1, spawnPoint, Quaternion.identity);
                    }

                }

            }
        }
    }


    void CloudsSpawn()
    {
        spawnPoint = Random.insideUnitSphere * cloudSphereRadius + transform.position;
        Instantiate(cloud, spawnPoint, Quaternion.Euler(0, Random.Range(-180, 180), 0));
    }


    void SpawnMiniIsland()
    {
        for (int i = 1; i <= 15; i++)
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

                if (Vector3.Distance(spawnedIsland.transform.position, new Vector3(spawnedIsland.transform.position.x + x, spawnedIsland.transform.position.y + y, spawnedIsland.transform.position.z + z)) > 250)  // sa nu spawneze in insula
                {
                    spawnedMiniIsland = Instantiate(Mini_Forest_Island, new Vector3(spawnedIsland.transform.position.x + x, spawnedIsland.transform.position.y + y, spawnedIsland.transform.position.z + z), Quaternion.identity);
                    spawnedMiniIsland.transform.SetParent(spawnedIsland.transform);
                }
        }
 
    }

   
}