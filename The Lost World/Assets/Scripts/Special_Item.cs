using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special_Item : MonoBehaviour
{
    /// Jetpack ///
    [SerializeField]
    private float flySpeed = 10;              //cu cat zboara
    public bool useJetpack;     //sa nu poti merge normal daca folosesti jetpacku pt ca se aduna vitezele si se poate exploata
    [SerializeField]
    private LayerMask islandMask;


    [SerializeField]
    private GameObject special_slot;
    void Start()
    {
  
    }

    
    void Update()
    {
        if (FindObjectOfType<Inventory>().Slot_Item_Code[25] == 7)
            Jetpack();
    }

    void Jetpack()
    {

        if (Input.GetKey(KeyCode.LeftControl) && FindObjectOfType<PlayerMovement>().MovementFrozen == false && special_slot.GetComponent<Inventory_slots>().batteryCharge > 0)
        {
            useJetpack = true;

            if (FindObjectOfType<PlayerMovement>().isGrounded == false) //sa nu abuzeze sa mearga pe insula mai repede cu jetu
            {
                FindObjectOfType<PlayerMovement>().controller.Move(Camera.main.transform.forward * flySpeed * 5 * Time.deltaTime);
            }
            else
                FindObjectOfType<PlayerMovement>().controller.Move(Camera.main.transform.forward * flySpeed * Time.deltaTime);


            FindObjectOfType<PlayerMovement>().velocity.y = -2f;
            

            special_slot.GetComponent<Inventory_slots>().batteryCharge -= Time.deltaTime;
          
        }
        else     
        {          
            useJetpack = false;
        }


        JetpackSound();
    }

    void JetpackSound()
    {
        if(useJetpack == true)
        {
            if (FindObjectOfType<Sounds_Player>().jetpack_sound.isPlaying == false)
                FindObjectOfType<Sounds_Player>().jetpack_sound.Play();
        }
        else
            FindObjectOfType<Sounds_Player>().jetpack_sound.Stop();
    }
}
