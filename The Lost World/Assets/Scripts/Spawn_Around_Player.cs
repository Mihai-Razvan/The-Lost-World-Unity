using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Around_Player : MonoBehaviour
{
    /// ISLANDS                                   

    private float IslandSpawnsphereRadius = 1000f;       //1000    //sfera in care se spawneza insulele
    private float IslanSphereRadius = 700f;   //700    //sfera unei insule cand se alege punctu de spawn verifica sa nu fie alta insula in sfera aia
    private int randomIslandNumber;
    private GameObject spawnedIsland;
    [SerializeField]
    GameObject island_forest_1;
    [SerializeField]
    GameObject island_snow_1;
    [SerializeField]
    GameObject island_desert_1;



    [SerializeField]
    private LayerMask islandMask;
    private Vector3 spawnPoint;

    ///CLOUDS
  
    [SerializeField]
    private LayerMask cloudMask;
    private int cloudSphereRadius = 800;
    [SerializeField]
    private GameObject cloud;



    float time;
    void Start()
    {
        //spawnedIsland = Instantiate(island_forest_1, new Vector3(2315, 0 ,1000), Quaternion.identity);    //spawn island

        for (int i = 1; i <= 20; i++)         //spawneaza nori la inceput 
        {
           // IslandSpawn();
            CloudsSpawn();
        }
        
    }


    void Update()
    {

        //if(FindObjectOfType<Save>().loaded == true)
        IslandSpawn();
     
        if((int) Random.Range(1, 300) == 1)
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
            randomIslandNumber = (int) Random.Range(1, 4) ;
            if (randomIslandNumber == 1)
              spawnedIsland = Instantiate(island_forest_1, spawnPosition, Quaternion.identity);
            else if(randomIslandNumber == 2)
              spawnedIsland = Instantiate(island_snow_1, spawnPosition, Quaternion.identity);
            else if (randomIslandNumber == 3)
                spawnedIsland = Instantiate(island_desert_1, spawnPosition, Quaternion.identity);

        }
    }


    void CloudsSpawn()
    {
        spawnPoint = Random.insideUnitSphere * cloudSphereRadius + transform.position;
        Instantiate(cloud, spawnPoint, Quaternion.Euler(0, Random.Range(-180, 180), 0));
    }

   
}