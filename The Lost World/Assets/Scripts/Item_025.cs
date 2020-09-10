using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item_025 : MonoBehaviour
{
    //GPS// 
    [SerializeField]
    private GameObject player;
    [SerializeField]
    public GameObject Xcoord;
    [SerializeField]
    public GameObject Ycoord;
    [SerializeField]
    public GameObject Zcoord;
    void Start()
    {
        
    }

    
    void Update()
    {
        Xcoord.GetComponent<TextMeshProUGUI>().text = "X: " + ((int) (player.transform.position.x / 10)).ToString();
        Ycoord.GetComponent<TextMeshProUGUI>().text = "Y: " + ((int) (player.transform.position.y / 10)).ToString();
        Zcoord.GetComponent<TextMeshProUGUI>().text = "Z: " + ((int) (player.transform.position.z / 10)).ToString();
    }
}
