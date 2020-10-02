using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour
{
    [SerializeField]
    private float camera_rotation_speed;
    [SerializeField]
    private GameObject[] island_type;
    private GameObject spawnedIsland;
    [SerializeField]
    private GameObject cloud;
    [SerializeField]
    private Button Load_World_Button;
    [SerializeField]
     private GameObject are_you_sure_Panel;

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

        if (PlayerPrefs.GetInt("World_Created") == 1)
            Load_World_Button.GetComponent<Button>().interactable = true;
        else
            Load_World_Button.GetComponent<Button>().interactable = false;

    }


    void CloudsSpawn()
    {
        Vector3 spawnPoint = Random.insideUnitSphere * 800 + new Vector3(0, 0, 0);
        Instantiate(cloud, spawnPoint, Quaternion.Euler(0, Random.Range(-180, 180), 0));
    }


    public void NewWorld()
    {
        if (PlayerPrefs.GetInt("World_Created") == 1)
            are_you_sure_Panel.SetActive(true);
        else
            Yes();       
    }

    public void Yes()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetFloat("Player_Position_X", 1000);
        PlayerPrefs.SetFloat("Player_Position_Y", 1002);
        PlayerPrefs.SetFloat("Player_Position_Z", 1000);

        PlayerPrefs.SetFloat("Player_Health", 100);
        PlayerPrefs.SetFloat("Player_Food", 100);
        PlayerPrefs.SetFloat("Player_Poison", 0);

        PlayerPrefs.SetInt("Inventory_Battery_Code_Special_Slot", 31);
        PlayerPrefs.SetFloat("Inventory_Battery_Charge_Special_Slot", 20);
        PlayerPrefs.SetFloat("Inventory_Battery_Max_Charge_Special_Slot", 20);

        PlayerPrefs.SetInt("String_island_Type_1", 1);
        PlayerPrefs.SetFloat("String_island_X_1", 1000);
        PlayerPrefs.SetFloat("String_island_Y_1", 1000);
        PlayerPrefs.SetFloat("String_island_Z_1", 1000);

        PlayerPrefs.SetInt("Number_Of_Islands", 1);

        PlayerPrefs.SetInt("World_Created", 1);
        SceneManager.LoadScene("Game");
    }


    public void No()
    {
        are_you_sure_Panel.SetActive(false);
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
