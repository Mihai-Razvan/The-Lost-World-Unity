using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerMask;
    private float scale;
    private float speed;
    [SerializeField]
    Light halloweenLight;
    [SerializeField]
    Material[] materials;

    void Start()
    {
        scale = Random.Range(0.5f, 5);
        transform.localScale = new Vector3(scale, scale, scale);
        speed = 0.4f / scale;    // cu cat e mai mare noru cu atat e mai incet
        int randMat = Random.Range(1, materials.Length - 1);
        transform.GetChild(0).GetComponent<Renderer>().material = materials[randMat];
    }

    
    void Update()
    {
        transform.Translate(transform.forward * speed);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1000, playerMask);
        if (colliders.Length == 0)
            Destroy(this.gameObject);
    }
}
