using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Island : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.RotateAround(transform.parent.position, Vector3.up ,5 * Time.deltaTime);
    }
}
