﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special_Item : MonoBehaviour
{
    /// Jetpack ///
    [SerializeField]
    private float flySpeed = 100;              //cu cat zboara
    [SerializeField]
    private int MaximumjetpackMoveSpeed = 1000;     //viteza maxima //100 normal
    private int MinimumjetpackMoveSpeed;
    [SerializeField]
    private float acceleration;
    private bool isLanded;
    public bool useJetpack;     //sa nu poti merge normal daca folosesti jetpacku pt ca se aduna vitezele si se poate exploata
    [SerializeField]
    private LayerMask islandMask;


    [SerializeField]
    private GameObject special_slot;
    void Start()
    {
        MinimumjetpackMoveSpeed = 100;//1 FindObjectOfType<PlayerMovement>().moveSpeed;  // viteza minima de zbor e viteza normala de mers   
    }

    
    void Update()
    {
        if (FindObjectOfType<Inventory>().Slot_Item_Code[25] == 7)
            Jetpack();
    }

    void Jetpack()
    {
        isLanded = Physics.Raycast(FindObjectOfType<PlayerMovement>().player.transform.position, -transform.up, 5, islandMask);  // are ceva sub

        if (Input.GetKey(KeyCode.LeftControl) && FindObjectOfType<PlayerMovement>().MovementFrozen == false && special_slot.GetComponent<Inventory_slots>().batteryCharge > 0)
        {
            useJetpack = true;

            if (isLanded == false) //sa nu abuzeze sa mearga pe insula mai repede cu jetu
            {
                if (flySpeed <= MaximumjetpackMoveSpeed)
                    flySpeed += acceleration * 50 *Time.deltaTime;     //scoate * 5 e pt teste
            }
            else if (flySpeed > MinimumjetpackMoveSpeed)
                flySpeed -= acceleration * 5 * Time.deltaTime;


            FindObjectOfType<PlayerMovement>().velocity.y = -2f;
            FindObjectOfType<PlayerMovement>().controller.Move(Camera.main.transform.forward * flySpeed * Time.deltaTime);

            special_slot.GetComponent<Inventory_slots>().batteryCharge -= Time.deltaTime;
          
        }
        else     
        {
            if (flySpeed > MinimumjetpackMoveSpeed)   // cand nu mai folosesti jetu viteza scade in timp
                flySpeed -= acceleration * 5 * Time.deltaTime;

            useJetpack = false;
        }

        flySpeed = Mathf.Max(flySpeed, MinimumjetpackMoveSpeed);        //daca scade prea mult sub cand ai ceva sub

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
