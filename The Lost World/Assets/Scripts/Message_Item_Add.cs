using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message_Item_Add : MonoBehaviour
{
    [SerializeField]
    private GameObject boxPreab;
    [SerializeField]
    private GameObject[] boxes;
    [SerializeField]
    private Transform position;
    [SerializeField]
    private bool[] posIsOccupied;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { /*
            if (posIsOccupied[3] == true)
            {
                Destroy(boxes[3].gameObject);
                boxes[2].gameObject.transform.position = Vector3.MoveTowards(pos[2].transform.position, pos[3].transform.position, 1);
                boxes[3] = boxes[2];
                boxes[1].gameObject.transform.position = Vector3.MoveTowards(pos[1].transform.position, pos[2].transform.position, 1);
                boxes[2] = boxes[1];

                box.transform.SetParent(this.gameObject.transform);
            }
            */


           boxes[1] = Instantiate(boxPreab, position.position, Quaternion.identity);
            boxes[1].transform.SetParent(this.gameObject.transform);


        }
    }
}
