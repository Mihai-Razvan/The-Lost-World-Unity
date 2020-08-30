using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPlacingChange : MonoBehaviour
{
                                      /// clasa asta se pune pe obiect cand il plasezi ////
                                      
    private bool IsPlaced;
    [SerializeField]
    private int numberOfCollidingObjects;
    [SerializeField]
    private Material[] OkmaterialsArray;
    [SerializeField]
    private Material[] CollidingmaterialsArray;
    [SerializeField]
    private int numberOfMaterials;
    public bool placeable;

    void Update()
    {
        
            numberOfMaterials = gameObject.GetComponent<Renderer>().materials.Length;

            Material[] OkmaterialsArray = new Material[numberOfMaterials];
            Material[] CollidingmaterialsArray = new Material[numberOfMaterials];

            for (int i = 0; i < numberOfMaterials; i++)
            {
                OkmaterialsArray[i] = FindObjectOfType<Handing_Item>().OkMaterial;
                CollidingmaterialsArray[i] = FindObjectOfType<Handing_Item>().CollidingMaterial;
            }
                    

        if (numberOfCollidingObjects == 0)
        {
            /*
            if (gameObject.tag != "Bridge")     //ca la bridge sa fie rosu si daca e in aer si nu e snapped 
            {
                placeable = true;
                gameObject.GetComponent<Renderer>().materials = OkmaterialsArray;
            }
            else if (Physics.Raycast(FindObjectOfType<Place_Prefab>().playerPos.position, -transform.up, 10f, FindObjectOfType<Place_Prefab>().Bridge_placeable_Surface_Mask) == true || FindObjectOfType<Place_Prefab>().isSnapped == true) //e in aer si nu e prin de alt bridge
            {
                placeable = true;
                gameObject.GetComponent<Renderer>().materials = OkmaterialsArray;
            }
            else
            {
                gameObject.GetComponent<Renderer>().materials = CollidingmaterialsArray;
                placeable = false;     //e pod si nu se respecta cond 2 adica e in aer da e snap sau nu e in aer
            }
            */
            placeable = true;
            gameObject.GetComponent<Renderer>().materials = OkmaterialsArray;
        }
        else
        {
            gameObject.GetComponent<Renderer>().materials = CollidingmaterialsArray;
            placeable = false;
        }




    }


    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.layer == 13)      // inseamna ca e building si nu e prob daca se loveste de floor ca se pune pe floor
        {
            if (collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 13 || collision.collider.gameObject.layer == 18)
                numberOfCollidingObjects++;
        }
        else if (gameObject.layer == 15)   // e floor si la asta se pune si flooru ca nu se poate lovi de el sau pune peste
        {
            if (collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 13 || collision.collider.gameObject.layer == 15 || collision.collider.gameObject.layer == 18)
                numberOfCollidingObjects++;
        }
        else if (gameObject.layer == 18)      // inseamna ca e other prefab si nu se e ca floru sa se poata pune buildinguri pe el da nici ca building sa poata fi pu pe ffloruri
        {
            if (collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 13 || collision.collider.gameObject.layer == 15 || collision.collider.gameObject.layer == 18)
                numberOfCollidingObjects++;
        }
    }  

    void OnCollisionExit(Collision collision)
    {
        if (gameObject.layer == 13)
        {
            if (collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 13 || collision.collider.gameObject.layer == 18)
                numberOfCollidingObjects--;
        }
        else if (gameObject.layer == 15)   
        {
            if (collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 13 || collision.collider.gameObject.layer == 15 || collision.collider.gameObject.layer == 18)
                numberOfCollidingObjects--;
        }
        else if (gameObject.layer == 18)      
        {
            if (collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 13 || collision.collider.gameObject.layer == 15 || collision.collider.gameObject.layer == 18)
                numberOfCollidingObjects--;
        }
    }
}
