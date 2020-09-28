using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Island : MonoBehaviour
{
    private float speed;
    void Start()
    {
        speed = Random.Range(5, 30);
    }

    
    void Update()
    {
        transform.RotateAround(transform.parent.position, - Vector3.up , speed * Time.deltaTime);
    }
}
