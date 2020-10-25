using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField]
    public GameObject Pickup_Button;
    [SerializeField]
    public GameObject AccesBuildingButton;
    [SerializeField]
    public GameObject RemoveButton;      // X ala de apare 
    [SerializeField]
    public GameObject EatButton;
    [SerializeField]
    public GameObject AddButton;         //adaugi lemene si incrediente la cooking pot
    [SerializeField]
    private Transform normalPosition;
    [SerializeField]
    private Transform leftPosition;
    [SerializeField]
    private Transform rightPosition;


    void Start()
    {
        Pickup_Button.SetActive(false);
        AccesBuildingButton.SetActive(false);
        RemoveButton.SetActive(false);
        EatButton.SetActive(false);
        AddButton.SetActive(false);
    }

    
    void Update()
    {
        if (AccesBuildingButton.activeInHierarchy == true)
        {
            if (EatButton.activeInHierarchy == false)
                AccesBuildingButton.transform.position = normalPosition.position;
            else
            {
                AccesBuildingButton.transform.position = rightPosition.position;
                EatButton.transform.position = leftPosition.position;
            }


            if (RemoveButton.activeInHierarchy == false)
                AccesBuildingButton.transform.position = normalPosition.position;
            else
            {
                AccesBuildingButton.transform.position = rightPosition.position;
                RemoveButton.transform.position = leftPosition.position;
            }
        }
        else if (RemoveButton.activeInHierarchy == true)                   //nu au cum sa fie si acces si remove active in ac tim asa ca pun else pt optimizare
        {
            if (EatButton.activeInHierarchy == false)
                RemoveButton.transform.position = normalPosition.position;
            else
            {
                RemoveButton.transform.position = rightPosition.position;
                EatButton.transform.position = leftPosition.position;
            }
        } 
        else if (EatButton.activeInHierarchy == true)                         //daca asta e activ si restu nu e in poz normala
            EatButton.transform.position = normalPosition.position;

    }
}
