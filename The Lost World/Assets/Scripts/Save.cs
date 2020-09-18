using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private LayerMask saveableMask;
    [SerializeField]
    private LayerMask islandMask;        //sa devina child la insula relieu sa se poate despawna

    [SerializeField]
    public int numberOfIslands;
    [SerializeField]
    private int numberOfRelief;
    

    [SerializeField]
    private GameObject island_type_1;      //forest
    [SerializeField]
    private GameObject island_type_2;      //snow
    [SerializeField]
    private GameObject island_type_3;      //desert


    [SerializeField]
    private GameObject[] relief_type;
    


    [SerializeField]
    private int[] test;

    private void Awake()
    {
        LoadFunction();
       
    }
    void Update()
    {

    }

    private void OnApplicationQuit()
    {
        SaveFunction();

    }

    public void SaveFunction()
    {
        PlayerPrefs.DeleteAll();

        SavePlayer();     
        SaveIslandsANDRelief();
    }

    public void LoadFunction()
    {
        LoadPlayer();
        LoadIslandsANDRelief();
    }


    void SavePlayer()
    {
        PlayerPrefs.SetFloat("Player_Position_X", player.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_Y", player.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_Z", player.transform.position.z);

        for(int i = 1; i <= 25; i ++)
        {
            PlayerPrefs.SetInt("Inventory_Item_Code_Slot_" + i.ToString(), FindObjectOfType<Inventory>().Slot_Item_Code[i]);
            PlayerPrefs.SetInt("Inventory_Item_Quantity_Slot_" + i.ToString(), FindObjectOfType<Inventory>().Slot_Item_Quantity[i]);
        }
    }



    void LoadPlayer()
    {
        float playerX = PlayerPrefs.GetFloat("Player_Position_X");
        float playerY = PlayerPrefs.GetFloat("Player_Position_Y");
        float playerZ = PlayerPrefs.GetFloat("Player_Position_Z");

        player.transform.position = new Vector3(playerX, playerY, playerZ);

        for (int i = 1; i <= 25; i++)
        {
            FindObjectOfType<Inventory>().Slot_Item_Code[i] = PlayerPrefs.GetInt("Inventory_Item_Code_Slot_" + i.ToString());
            FindObjectOfType<Inventory>().Slot_Item_Quantity[i] = PlayerPrefs.GetInt("Inventory_Item_Quantity_Slot_" + i.ToString());
        }
    }



    void SaveIslandsANDRelief()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, 7000, saveableMask);

        numberOfIslands = 0;
        numberOfRelief = 0;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Island Point Type 1" || colliders[i].tag == "Island Point Type 2" || colliders[i].tag == "Island Point Type 3")
            {
                numberOfIslands++;

                PlayerPrefs.SetInt("String_island_Type_" + numberOfIslands.ToString(), GetIslandType(colliders, i));
                PlayerPrefs.SetFloat("String_island_X_" + numberOfIslands.ToString(), colliders[i].transform.position.x);
                PlayerPrefs.SetFloat("String_island_Y_" + numberOfIslands.ToString(), colliders[i].transform.position.y);
                PlayerPrefs.SetFloat("String_island_Z_" + numberOfIslands.ToString(), colliders[i].transform.position.z);
            }
            else if (colliders[i].tag == "Relief Point Type 1" || colliders[i].tag == "Relief Point Type 2" || colliders[i].tag == "Relief Point Type 3" || colliders[i].tag == "Relief Point Type 4" || colliders[i].tag == "Relief Point Type 5" || colliders[i].tag == "Relief Point Type 6" || colliders[i].tag == "Relief Point Type 7" || colliders[i].tag == "Relief Point Type 8" || colliders[i].tag == "Relief Point Type 9" || colliders[i].tag == "Relief Point Type 10" || colliders[i].tag == "Relief Point Type 11" || colliders[i].tag == "Relief Point Type 12" || colliders[i].tag == "Relief Point Type 13" || colliders[i].tag == "Relief Point Type 14" || colliders[i].tag == "Relief Point Type 15" || colliders[i].tag == "Relief Point Type 16")
            {
                numberOfRelief++;

                PlayerPrefs.SetInt("String_relief_Type_" + numberOfRelief.ToString(), GetReliefType(colliders, i));
                PlayerPrefs.SetFloat("String_relief_X_" + numberOfRelief.ToString(), colliders[i].transform.position.x);
                PlayerPrefs.SetFloat("String_relief_Y_" + numberOfRelief.ToString(), colliders[i].transform.position.y);
                PlayerPrefs.SetFloat("String_relief_Z_" + numberOfRelief.ToString(), colliders[i].transform.position.z);
            }
        }

        PlayerPrefs.SetInt("Number_Of_Islands", numberOfIslands);
        PlayerPrefs.SetInt("Number_Of_Relief", numberOfRelief);
    }


    private void LoadIslandsANDRelief()
    {
        numberOfIslands = PlayerPrefs.GetInt("Number_Of_Islands");
        numberOfRelief = PlayerPrefs.GetInt("Number_Of_Relief");

        for (int i = 1; i <= numberOfIslands; i++)
        {

            float xPos = PlayerPrefs.GetFloat("String_island_X_" + i.ToString());
            float yPos = PlayerPrefs.GetFloat("String_island_Y_" + i.ToString());
            float zPos = PlayerPrefs.GetFloat("String_island_Z_" + i.ToString());

            if (PlayerPrefs.GetInt("String_island_Type_" + i.ToString()) == 1)
            {
                GameObject spawned = Instantiate(island_type_1, new Vector3(xPos, yPos, zPos), Quaternion.identity);
                spawned.GetComponent<IslandObjects_Forest>().Respawned = true;
            }
            else if (PlayerPrefs.GetInt("String_island_Type_" + i.ToString()) == 2)
            {
                GameObject spawned = Instantiate(island_type_2, new Vector3(xPos, yPos, zPos), Quaternion.identity);
                spawned.GetComponent<IslandObjects_Snow>().Respawned = true;
            }
            else if (PlayerPrefs.GetInt("String_island_Type_" + i.ToString()) == 3)
            {
                GameObject spawned = Instantiate(island_type_3, new Vector3(xPos, yPos, zPos), Quaternion.identity);
                spawned.GetComponent<IslandObjects_Desert>().Respawned = true;
            }
        }


        for (int i = 1; i <= numberOfRelief; i++)
        {
            // test[i] = PlayerPrefs.GetInt("String_relief_Type_" + i.ToString());
            float xPos = PlayerPrefs.GetFloat("String_relief_X_" + i.ToString());
            float yPos = PlayerPrefs.GetFloat("String_relief_Y_" + i.ToString());
            float zPos = PlayerPrefs.GetFloat("String_relief_Z_" + i.ToString());

            RaycastHit hit;
            if (Physics.Raycast(new Vector3(xPos, yPos + 50, zPos), -transform.up, out hit, 100, islandMask))
            {
                GameObject spawnedrelief = Instantiate(relief_type[PlayerPrefs.GetInt("String_relief_Type_" + i.ToString())], hit.point, Quaternion.identity);
                spawnedrelief.transform.SetParent(hit.collider.transform);
            }


        }
    }
     
    private int GetIslandType(Collider[] colliders, int i)
    {

        if (colliders[i].tag == "Island Point Type 1")
            return 1;
        else if (colliders[i].tag == "Island Point Type 2")
            return 2;
        else if (colliders[i].tag == "Island Point Type 3")      
            return 3;
        


        return -1;
    }


    private int GetReliefType(Collider[] colliders, int i)
    {
        for (int j = 1; j <= 16; j++)
            if (colliders[i].tag == "Relief Point Type " + j.ToString())
                return j;

        return 0;    
    }
}
