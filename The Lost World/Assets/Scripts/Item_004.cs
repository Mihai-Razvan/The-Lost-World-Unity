using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Item_004 : MonoBehaviour
{                                                                   ////////////// FURNACE \\\\\\\\\\\\\\\\\

    public bool BuildingAccessed;
    [SerializeField]
    private GameObject fire_animation;
    [SerializeField]
    private GameObject smoke_animation;

    public int[] Slot_Item_Code;             //codu itemului din fiecare slot din inventar
    public int[] Slot_Item_Quantity;          //cantitatea
    public int quantityToAdd;
    private int itemCodeToAdd;
    [SerializeField]
    public float fuel;
    public float Transforming_Time_Left;
    public int produced_item_code;

    private int woodFuel = 10;      //cat fuel da wood
    private int logFuel = 100;

    private int iron_ore_transform_time= 5;
    private int rich_ore_transform_time = 5;
    private int copper_ore_transform_time = 5;


    [SerializeField]
    private AudioSource fireSound;
    
    void Start()
    {
        
    }

    
    void Update()
    {

        Fuel();    
        TransformItems();
        Animations();
        fuel -= Time.deltaTime;
        if (fuel < 0)
            fuel = 0;

        Transforming_Time_Left -= Time.deltaTime;

        /*
         if(Transforming_Time_Left > 0)   // produce ceva
            for (int i = 1; i <= 24; i++)
                if (Slot_Item_Quantity[i] == 0)
                    Slot_Item_Code[i] = 0;
                    */
    }

    

    void Fuel()
    {
        if (fuel <= 0)
        {
            for (int i = 1; i <= 20; i++)
                if (Slot_Item_Code[i] == 1 && Slot_Item_Quantity[i] < FindObjectOfType<List_Of_Items>().Item_Stack_Number[i])  //wood
                {
                    fuel = woodFuel;
                    Slot_Item_Quantity[i]--;
                    if (Slot_Item_Quantity[i] == 0)
                        Slot_Item_Code[i] = 0;
                }
                else if (Slot_Item_Code[i] == 18 && Slot_Item_Quantity[i] < FindObjectOfType<List_Of_Items>().Item_Stack_Number[i])  //log
                {
                    fuel = logFuel;
                    Slot_Item_Quantity[i]--;
                    if (Slot_Item_Quantity[i] == 0)
                        Slot_Item_Code[i] = 0;
                }


            if (fuel <= 0)  // nu a gasit un stack necomplet
                for (int i = 1; i <= 20; i++)
                    if (Slot_Item_Code[i] == 1)
                    {
                        fuel = woodFuel;
                        Slot_Item_Quantity[i]--;
                        if (Slot_Item_Quantity[i] == 0)
                            Slot_Item_Code[i] = 0;
                    }
                    else if (Slot_Item_Code[i] == 18)
                    {
                        fuel = logFuel;
                        Slot_Item_Quantity[i]--;
                        if (Slot_Item_Quantity[i] == 0)
                            Slot_Item_Code[i] = 0;
                    }
        }

        
    }

    void TransformItems()
    {
        if (Transforming_Time_Left <= 0)
        {
            itemCodeToAdd = produced_item_code;    // s a terrminat timpu de producere si adauga itemu produs
            if(produced_item_code != 0)
            AddToInventory();

            if (fuel > 0)         // gaseste alt item sa produca
            {
                produced_item_code = 0;
                for (int i = 1; i <= 20; i++)
                    if (Slot_Item_Code[i] == 3)  //iron_ore
                    {

                        Slot_Item_Quantity[i]--;
                        if (Slot_Item_Quantity[i] == 0)
                            Slot_Item_Code[i] = 0;
                        Transforming_Time_Left = iron_ore_transform_time;
                        produced_item_code = 6;  //iron ingot
                        quantityToAdd = 1;
                    }
                    else if (Slot_Item_Code[i] == 19)  //copper_ore
                    {

                        Slot_Item_Quantity[i]--;
                        if (Slot_Item_Quantity[i] == 0)
                            Slot_Item_Code[i] = 0;
                        Transforming_Time_Left = copper_ore_transform_time;
                        produced_item_code = 20;   //copper ingot
                        quantityToAdd = 1;
                    }
                    else if (Slot_Item_Code[i] == 23)  //rich iron ore
                    {

                        Slot_Item_Quantity[i]--;
                        if (Slot_Item_Quantity[i] == 0)
                            Slot_Item_Code[i] = 0;
                        Transforming_Time_Left = rich_ore_transform_time;
                        produced_item_code = 6;   //iron ingot
                        quantityToAdd = 2;
                    }



                if (Transforming_Time_Left <= 0)   //nu a gasit un stack incomplet cu ceva de transormat si cauta unu plin
                    for (int i = 1; i <= 20; i++)
                    {
                        if (Slot_Item_Code[i] == 3)
                        {
                            Slot_Item_Quantity[i]--;
                            if (Slot_Item_Quantity[i] == 0)
                                Slot_Item_Code[i] = 0;
                            Transforming_Time_Left = iron_ore_transform_time;
                            produced_item_code = 6;
                            quantityToAdd = 1;
                        }
                        else if (Slot_Item_Code[i] == 19)
                        {
                            Slot_Item_Quantity[i]--;
                            if (Slot_Item_Quantity[i] == 0)
                                Slot_Item_Code[i] = 0;
                            Transforming_Time_Left = copper_ore_transform_time;
                            produced_item_code = 20;
                            quantityToAdd = 1;
                        }
                        else if (Slot_Item_Code[i] == 23)
                        {
                            Slot_Item_Quantity[i]--;
                            if (Slot_Item_Quantity[i] == 0)
                                Slot_Item_Code[i] = 0;
                            Transforming_Time_Left = rich_ore_transform_time;
                            produced_item_code = 6;
                            quantityToAdd = 2;
                        }
                    }
            }
        }
        
    }

    void Animations()
    {
        if (fuel <= 0)
        {
            fire_animation.SetActive(false);
            smoke_animation.SetActive(false);
            fireSound.Stop();
        }
        else
        {
            fire_animation.SetActive(true);
            smoke_animation.SetActive(true);
            if (fireSound.isPlaying == false)
                fireSound.Play();
        }
    }


    public void AddToInventory()
    {
       
        for (int i = 1; i <= 20 && quantityToAdd != 0; i++)
            if (Slot_Item_Code[i] == itemCodeToAdd)
            {
                if (Slot_Item_Quantity[i] + quantityToAdd <= FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCodeToAdd])
                {
                    Slot_Item_Quantity[i] += quantityToAdd;
                    quantityToAdd = 0;
                }
                else
                {
                    quantityToAdd -= FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCodeToAdd] - Slot_Item_Quantity[i];
                    Slot_Item_Quantity[i] = FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCodeToAdd];
                }

            }


        if (quantityToAdd != 0)                                                                                      //s-a adaugat  in  sloturile unde eradeja  acum cauta un slot   gol
            for (int i = 1; i <= 20 && quantityToAdd != 0; i++)
            {
                if (Slot_Item_Code[i] == 0)                                                                          //SLOTU E GOL
                {
                    Slot_Item_Code[i] = itemCodeToAdd;

                    if (quantityToAdd <= FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCodeToAdd])
                    {
                        Slot_Item_Quantity[i] = quantityToAdd;
                        quantityToAdd = 0;
                    }
                    else
                    {
                        Slot_Item_Quantity[i] = FindObjectOfType<List_Of_Items>().Item_Stack_Number[itemCodeToAdd];
                        quantityToAdd -= Slot_Item_Quantity[i];
                    }
                }
            }

    }

    
}
