﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Around_Player : MonoBehaviour
{
    private float IslandSpawnsphereRadius = 750f;       //750     //sfera in care se spawneza insulele
    private float IslanSPhereRadius = 300f;   //300    //sfera unei insule cand se alege punctu de spawn verifica sa nu fie alta insula in sfera aia
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

    [SerializeField]
    private LayerMask islandMask;
    [SerializeField]
    private LayerMask animalMask;
    private float animalSphereRadius = 250;           //in asta verifica cate animale sunt
    private int maxAnimalNumber = 10;
    private Vector3 spawnPoint;
    private int animalRandomNumber;     //ce animal sa spawneze
    [SerializeField]
    private GameObject animal_1;   //bee

    [SerializeField]
    private LayerMask cloudMask;
    private int maxCloudNumber = 100;
    private int cloudSphereRadius = 750;
    [SerializeField]
    private GameObject cloud;

    void Start()
    {
        for(int i = 1; i <= 50; i++)         //spawneaza nori la inceput ca dupa nu spawneaza odata la cateva sec am facut asa ca sa nu verifice cati nori sunt in jur sa nu pun box coliider pe nori
        {
            spawnPoint = Random.insideUnitSphere * cloudSphereRadius + transform.position;
            Instantiate(cloud, spawnPoint, Quaternion.Euler(0, Random.Range(-180, 180), 0));
        }
    }


    void Update()
    {
        IslandSpawn();
        AnimalSpawn();
        if((int) Random.Range(1, 1000) == 1)
           CloudsSpawn();
    }

    void IslandSpawn()
    {
        Vector3 spawnPosition = Random.insideUnitSphere * IslandSpawnsphereRadius + new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Collider[] colliders = Physics.OverlapSphere(spawnPosition, IslanSPhereRadius);
        if (colliders.Length == 0)
        {
            randomIslandNumber = Random.Range(1, 4) ;
            if (randomIslandNumber < 3)
                Instantiate(island_forest_1, spawnPosition, Quaternion.identity);
            else
                Instantiate(island_snow_1, spawnPosition, Quaternion.identity);


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

                if (Physics.Raycast(spawnPoint, -transform.up, 50, islandMask))   // sa nu spawneze animale in afara insulei
                {
                    animalRandomNumber = Random.Range(1, 1);
                    if (animalRandomNumber == 1)     //bee
                        Instantiate(animal_1, spawnPoint, Quaternion.identity);

                }

            }
        }
    }


    void CloudsSpawn()
    {
        spawnPoint = Random.insideUnitSphere * cloudSphereRadius + transform.position;
        Instantiate(cloud, spawnPoint, Quaternion.Euler(0, Random.Range(-180, 180), 0));
    }

}