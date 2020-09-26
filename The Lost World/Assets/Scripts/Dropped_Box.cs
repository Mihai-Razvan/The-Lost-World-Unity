using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropped_Box : MonoBehaviour
{
    public int itemCode;
    public int itemQuantity;
    private float time_since_spawned;

    void Update()
    {
        time_since_spawned += Time.deltaTime;

        if (time_since_spawned >= 300)    //dupa un timp se despawneaza
            Destroy(this.gameObject);
    }

}
