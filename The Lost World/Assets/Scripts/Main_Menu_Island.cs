using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu_Island : MonoBehaviour
{
    [SerializeField]
    private float camera_rotation_speed;
    [SerializeField]
    private GameObject[] island_type;
    private GameObject spawnedIsland;
    [SerializeField]
    private GameObject cloud;

    void Start()
    {
        spawnedIsland = Instantiate(island_type[(int)Random.Range(1, 3)], new Vector3(0, 0, 0), Quaternion.identity);

        for (int i = 1; i < 300; i++)
            CloudsSpawn();
    }

    
    void Update()
    {
        Camera.main.transform.RotateAround(spawnedIsland.transform.position, Vector3.up, camera_rotation_speed * Time.deltaTime);

        if ((int)Random.Range(1, 300) == 1)
            CloudsSpawn();
    }


    void CloudsSpawn()
    {
        Vector3 spawnPoint = Random.insideUnitSphere * 800 + new Vector3(0, 0, 0);
        Instantiate(cloud, spawnPoint, Quaternion.Euler(0, Random.Range(-180, 180), 0));
    }
}
