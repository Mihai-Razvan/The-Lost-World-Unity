using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        for (int i = 1; i < 50; i++)
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



    public void NewWorld()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetFloat("Player_Position_X", 1000);
        PlayerPrefs.SetFloat("Player_Position_Y", 1030);
        PlayerPrefs.SetFloat("Player_Position_Z", 1000);

        PlayerPrefs.SetFloat("Player_Health", 100);
        PlayerPrefs.SetFloat("Player_Food", 100);
        PlayerPrefs.SetFloat("Player_Poison", 0);

        PlayerPrefs.SetInt("Inventory_Battery_Code_Special_Slot", 31);
        PlayerPrefs.SetFloat("Inventory_Battery_Charge_Special_Slot", 20);
        PlayerPrefs.SetFloat("Inventory_Battery_Max_Charge_Special_Slot", 20);

        SceneManager.LoadScene("Game");
    }


    public void LoadWorld()
    {
        SceneManager.LoadScene("Game");
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
