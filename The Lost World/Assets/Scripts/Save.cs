using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    [SerializeField]
    public int numberOfIslands;
    [SerializeField]
    public string[] String_island_Type;
    [SerializeField]
    public int[] island_Type;
    [SerializeField]
    public string[] String_island_X;
    [SerializeField]
    public float[] island_X;
    public string[] String_island_Y;
    [SerializeField]
    public float[] island_Y;
    public string[] String_island_Z;
    [SerializeField]
    public float[] island_Z;

    void Start()
    {       
         
    }

   
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        for (int i = 1; i < numberOfIslands; i++)
        {
            PlayerPrefs.SetInt(String_island_Type[i], island_Type[i]);
            PlayerPrefs.SetFloat(String_island_X[i], island_X[i]);
            PlayerPrefs.SetFloat(String_island_Y[i], island_Y[i]);
            PlayerPrefs.SetFloat(String_island_Z[i], island_Z[i]);
        }
    }
}
