using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_026 : MonoBehaviour
{
                                                                                     /////sap extractor//////

    public bool isPlaced;                     //sa nu se produca si sa nu se puna aia verde (full) cat timp e in placement
    public float production_Time = 300;      //cat dureaza o productie
    public float time_On_This_Round;    //cat timp a trecut din prod asta
    [SerializeField]
    private GameObject full;          //ala verde
    void Start()
    {
        
    }

    
    void Update()
    {
        if (isPlaced == true)
        {
            time_On_This_Round += Time.deltaTime;

            if (time_On_This_Round >= production_Time)
                full.SetActive(true);
            else
                full.SetActive(false);
        }

    }
}
