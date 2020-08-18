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
                    

        if (numberOfCollidingObjects == 0 && FindObjectOfType<Handing_Item>().hit.normal.x < 0.3f && FindObjectOfType<Handing_Item>().hit.normal.x > -0.3f)
            {
                gameObject.GetComponent<Renderer>().materials = OkmaterialsArray;
                placeable = true;
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
            if (collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 13)
                numberOfCollidingObjects++;
        }
        else if (gameObject.layer == 15)   // e floor si la asta se pune si flooru ca nu se poate lovi de el sau pune peste
        {
            if (collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 13 || collision.collider.gameObject.layer == 15)
                numberOfCollidingObjects++;
        }
    }  

    void OnCollisionExit(Collision collision)
    {
        if (gameObject.layer == 13)
        {
            if (collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 13)
                numberOfCollidingObjects--;
        }
        else if (gameObject.layer == 15)   
        {
            if (collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 13 || collision.collider.gameObject.layer == 15)
                numberOfCollidingObjects--;
        }
    }
}
