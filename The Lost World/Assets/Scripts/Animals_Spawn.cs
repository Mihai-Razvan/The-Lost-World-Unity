using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals_Spawn : MonoBehaviour
{
    [SerializeField]
    private LayerMask islandMask;
    [SerializeField]
    private LayerMask animalMask;
    private float sphereCheckRadius = 250;
    private int maxAnimalNumber = 50;
    private Vector3 spawnPoint;

    private int animalRandomNumber;     //ce animal sa spawneze
    [SerializeField]
    private GameObject animal_1;   //bee
    void Start()
    {
        
    }

    
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, 20, islandMask))    //daca playeru e pe insula altfel nu spanweza animale
        {
            AnimalSpawn();
        }
    }


    void AnimalSpawn()     
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereCheckRadius, animalMask);
        if(colliders.Length < maxAnimalNumber)      //verifica sa nu fie prea mult animale pe un radius in juru playerului(adik pe insula)
        {
            spawnPoint = Random.insideUnitSphere * sphereCheckRadius + transform.position;

            if (Physics.Raycast(spawnPoint, -transform.up, 50, islandMask))   // sa nu spawneze animale in afara insulei
            {
                animalRandomNumber = Random.Range(1, 1);
                if(animalRandomNumber == 1)     //bee
                    Instantiate(animal_1, spawnPoint, Quaternion.identity);
                
            }
                
        }
    }

    
}
