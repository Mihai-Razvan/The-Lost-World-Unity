using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private LayerMask islandMask;

    [SerializeField]
    public int numberOfIslands;
    

    [SerializeField]
    private GameObject island_type_1;      //forest
    [SerializeField]
    private GameObject island_type_2;      //snow
    [SerializeField]
    private GameObject island_type_3;      //desert

    [SerializeField]
    private int[] test;

    private bool loaded;

    private void Awake()
    {
        LoadFunction();
        /*
        numberOfIslands = PlayerPrefs.GetInt("Number_Of_Islands");

        
        for (int i = 1; i <= numberOfIslands; i++)
        {
            island_Type[i] = PlayerPrefs.GetInt("String_island_Type_" + i.ToString());          
            island_X[i] = PlayerPrefs.GetFloat("String_island_X_" + i.ToString());
            island_Y[i] = PlayerPrefs.GetFloat("String_island_Y_" + i.ToString());
            island_Z[i] = PlayerPrefs.GetFloat("String_island_Z_" + i.ToString());


            test[i] = PlayerPrefs.GetInt("String_island_Type_" + i.ToString());
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
        */
        
    }
    void Update()
    {
        
        /*
        if(Input.GetKey(KeyCode.N))
            for (int i = 1; i <= 10; i++)
                PlayerPrefs.SetInt("String_island_Type_" + i.ToString(), island_Type[i]);


        if (Input.GetKey(KeyCode.M))
            for (int i = 1; i <= 10; i++)
                test[i] = PlayerPrefs.GetInt("String_island_Type_" + i.ToString());

        if (Input.GetKey(KeyCode.P))
            PlayerPrefs.DeleteAll();
       
        */
    }

    private void OnApplicationQuit()
    {
        SaveFunction();
        /*
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Number_Of_Islands", numberOfIslands);
        for (int i = 1; i <= numberOfIslands; i++)
        {        
            PlayerPrefs.SetInt("String_island_Type_" + i.ToString(), island_Type[i]);
            PlayerPrefs.SetFloat("String_island_X_" + i.ToString(), island_X[i]);
            PlayerPrefs.SetFloat("String_island_Y_" + i.ToString(), island_Y[i]);
            PlayerPrefs.SetFloat("String_island_Z_" + i.ToString(), island_Z[i]);
            
        }
        */
    }

    public void SaveFunction()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetFloat("Player_Position_X", player.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_Y", player.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_Z", player.transform.position.z);

        Collider[] colliders = Physics.OverlapSphere(player.transform.position, 7000, islandMask);
        int j = 0;
        for(int i = 0; i < colliders.Length; i ++)
        {
            if (colliders[i].tag == "Island Point Type 1" || colliders[i].tag == "Island Point Type 2" || colliders[i].tag == "Island Point Type 3")
            {             
                j++;
                
                PlayerPrefs.SetInt("String_island_Type_" + i.ToString(), GetIslandType(colliders, i));
                PlayerPrefs.SetFloat("String_island_X_" + i.ToString(), colliders[i].transform.position.x);
                PlayerPrefs.SetFloat("String_island_Y_" + i.ToString(), colliders[i].transform.position.y);
                PlayerPrefs.SetFloat("String_island_Z_" + i.ToString(), colliders[i].transform.position.z);
            }
        }

        PlayerPrefs.SetInt("Number_Of_Islands", j);
    }

    public void LoadFunction()
    {
        float playerX = PlayerPrefs.GetFloat("Player_Position_X");
        float playerY = PlayerPrefs.GetFloat("Player_Position_Y");
        float playerZ = PlayerPrefs.GetFloat("Player_Position_Z");

        player.transform.position = new Vector3(playerX, playerY, playerZ);

        numberOfIslands = PlayerPrefs.GetInt("Number_Of_Islands");

        for (int i = 0; i < numberOfIslands; i++)
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
    }



    private int GetIslandType(Collider[] colliders, int i)
    {

        if (colliders[i].tag == "Island Point Type 1")
        {
            Debug.Log("1");
            return 1;
        }
        else if (colliders[i].tag == "Island Point Type 2")
        {
            Debug.Log("2");
            return 2;
        }
        else if (colliders[i].tag == "Island Point Type 3")
        {
            Debug.Log("3");
            return 3;
        }


        return 0;
    }

}
