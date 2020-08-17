using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPlacingChange : MonoBehaviour
{
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
        if (collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 13)
        numberOfCollidingObjects++;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 13)
        numberOfCollidingObjects--;
    }
}
