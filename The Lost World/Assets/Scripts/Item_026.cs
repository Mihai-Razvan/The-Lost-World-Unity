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

    [SerializeField]
    private GameObject cactusCheckPoint;   //punctu ala de verifica daca e legat de cactus
    [SerializeField]
    public GameObject cactusSpawnPoint;
    [SerializeField]
    private LayerMask islandObjectsMask;
    public Vector3 attached_cactus_rotation;
    void Awake()
    {

        Collider[] colliders = Physics.OverlapSphere(cactusCheckPoint.transform.position, 3f, islandObjectsMask);
        if (colliders.Length != 0)
        {
            colliders[0].transform.parent.gameObject.tag = "Undespawnable Object";
            attached_cactus_rotation = new Vector3(colliders[0].transform.parent.transform.eulerAngles.x, colliders[0].transform.parent.transform.eulerAngles.y, colliders[0].transform.parent.transform.eulerAngles.z);
        }
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
