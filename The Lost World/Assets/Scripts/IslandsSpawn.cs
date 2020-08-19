using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandsSpawn : MonoBehaviour
{
    private float sphereRadius = 750f;
    private float IslanSPhereRadius = 250f;
    private int randomIslandNumber;
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
    void Start()
    {
        Vector3 spawnPosition = Random.insideUnitSphere * sphereRadius + new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Collider[] colliders = Physics.OverlapSphere(spawnPosition, IslanSPhereRadius);
        if (colliders.Length == 0)
            Instantiate(island_forest_1, spawnPosition, Quaternion.identity);
    }

    
    void Update()
    {
        Vector3 spawnPosition = Random.insideUnitSphere * sphereRadius + new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Collider[] colliders = Physics.OverlapSphere(spawnPosition, IslanSPhereRadius);
        if (colliders.Length == 0)
        {
            randomIslandNumber = Random.Range(1, 4);
            if (randomIslandNumber < 3)
                Instantiate(island_forest_1, spawnPosition, Quaternion.identity);
            else
                Instantiate(island_snow_1, spawnPosition, Quaternion.identity);
            

        }

    }
}
