using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_035 : MonoBehaviour
{
    public bool BuildingAccessed;
    public int[] cooking_pot_incredients_item_code;     //incredientele de lee are cooking potu
    public int[] cooking_pot_incredients_item_quantity;
    public int quantityToAdd;
    public int itemCodeToAdd;
    public float energy_left;
    public int stick_energy;
    public int log_energy;

    public int item_code_to_cook;
    public float cooking_time_remained;
    public bool cooking;

    [SerializeField]
    private GameObject fire_animation;
    [SerializeField]
    private GameObject food_place_1;
    [SerializeField]
    private GameObject food_place_2;
    public bool food_place_1_occupied;
    public bool food_place_2_occupied;

    [SerializeField]
    private GameObject[] food_models;

    [SerializeField]
    private LayerMask collectables_mask;

    [SerializeField]
    private GameObject full;

    void Start()
    {
        fire_animation.SetActive(false);
    }

    
    void Update()
    {
        AddItem();
        Cook();

        if(cooking == true)
        {
            full.SetActive(true);
            if (energy_left > 0)
                fire_animation.SetActive(true);
            else
                fire_animation.SetActive(false);
        }
        else
        {
            full.SetActive(false);
            fire_animation.SetActive(false);
        }
       
    }


    public void AddItem()
    {
        if(quantityToAdd > 0)
        {
            for(int i = 1; i <= 5; i ++)
                if(cooking_pot_incredients_item_code[i] == itemCodeToAdd)
                {
                    cooking_pot_incredients_item_quantity[i] += quantityToAdd;
                    quantityToAdd = 0;
                    break;
                }

            if(quantityToAdd > 0)
            {
                for (int i = 1; i <= 5; i++)
                    if (cooking_pot_incredients_item_quantity[i] == 0)
                    {
                        cooking_pot_incredients_item_code[i] = itemCodeToAdd;
                        cooking_pot_incredients_item_quantity[i] = quantityToAdd;
                        quantityToAdd = 0;
                        break;
                    }
            }
            
            itemCodeToAdd = 0;
        }
    }


    void Cook()
    {
        if(cooking == true)
        {
            energy_left -= Time.deltaTime;

            cooking_time_remained -= Time.deltaTime;
            if(cooking_time_remained <= 0)
            {                
                if (food_place_1_occupied == false)
                {
                    Instantiate(food_models[item_code_to_cook], food_place_1.transform.position, Quaternion.identity);
                    food_place_1_occupied = true;
                }
                else if (food_place_2_occupied == false)
                {
                    Instantiate(food_models[item_code_to_cook], food_place_2.transform.position, Quaternion.identity);
                    food_place_2_occupied = true;                    
                }

                item_code_to_cook = 0;
                cooking = false;
            }
        }
    }

    public void Occupied()
    {
        Collider[] colliders = Physics.OverlapSphere(food_place_1.transform.position, 0.2f, collectables_mask);

        if (colliders.Length == 0)
            food_place_1_occupied = false;
        else
            food_place_1_occupied = true;

        colliders = Physics.OverlapSphere(food_place_2.transform.position, 0.2f, collectables_mask);

        if (colliders.Length == 0)
            food_place_2_occupied = false;
        else
            food_place_2_occupied = true;
    }
}
