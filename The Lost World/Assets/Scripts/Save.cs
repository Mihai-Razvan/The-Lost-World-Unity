using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    [SerializeField]
    public int numberOfIslands;
    [SerializeField]
    public int[] island_Type;
    [SerializeField]
    public float[] island_X;
    [SerializeField]
    public float[] island_Y;
    [SerializeField]
    public float[] island_Z;

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

        numberOfIslands = PlayerPrefs.GetInt("Number_Of_Islands");

        
        for (int i = 1; i <= numberOfIslands; i++)
        {
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
        
    }
    void Update()
    {
        
        
        if(Input.GetKey(KeyCode.N))
            for (int i = 1; i <= 10; i++)
                PlayerPrefs.SetInt("String_island_Type_" + i.ToString(), island_Type[i]);

        if (Input.GetKey(KeyCode.M))
            for (int i = 1; i <= 10; i++)
                test[i] = PlayerPrefs.GetInt("String_island_Type_" + i.ToString());

        if (Input.GetKey(KeyCode.P))
            PlayerPrefs.DeleteAll();
       
        
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Number_Of_Islands", numberOfIslands);
        for (int i = 1; i <= numberOfIslands; i++)
        {        
            PlayerPrefs.SetInt("String_island_Type_" + i.ToString(), island_Type[i]);
            PlayerPrefs.SetFloat("String_island_X_" + i.ToString(), island_X[i]);
            PlayerPrefs.SetFloat("String_island_Y_" + i.ToString(), island_Y[i]);
            PlayerPrefs.SetFloat("String_island_Z_" + i.ToString(), island_Z[i]);
            
        }
    }
}
