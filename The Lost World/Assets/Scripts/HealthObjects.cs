using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObjects : MonoBehaviour
{
    public float health;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (health <= 0)
            Destroy(this.gameObject);
    }
}
