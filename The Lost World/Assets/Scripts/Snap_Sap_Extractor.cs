using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap_Sap_Extractor : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Snap_Sap_Extractor_Collider")
        {
            FindObjectOfType<Place_Building>().Building_In_Hand.transform.SetParent(null);
            FindObjectOfType<Place_Building>().Building_In_Hand.transform.rotation = Quaternion.Euler(collider.GetComponentInParent<Transform>().transform.eulerAngles.x, collider.GetComponentInParent<Transform>().transform.eulerAngles.y, collider.GetComponentInParent<Transform>().transform.eulerAngles.z);
            /*
              pun getcompparent<trans> pt ca daca iau datele de la collider collideru e modelu da nu rotatia aluia trebuie ca aia e mereu 0 ca se ia relativa fata de punctu pe
            care e modelu pus si position la ffel si sa iau datele de la punct ca alea trebuie nu de la modelu care e pus ca child la punct
            */
            FindObjectOfType<Place_Building>().Building_In_Hand.transform.position = collider.transform.GetComponentInParent<Transform>().transform.position;
            FindObjectOfType<Place_Building>().isSnapped = true;
            
        }
    }
}
