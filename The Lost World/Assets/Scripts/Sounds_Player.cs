using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sounds_Player : MonoBehaviour
{
    public AudioSource wind_sound;
    
    public AudioSource collect_item_sound;
    public AudioSource chest_open_sound;
    public AudioSource jetpack_sound;
    public AudioSource place_building_prefab_sound;
    public AudioSource heart_beat_sound;    //cand ai viata low

    public float normal_wind_volume = 1;
    public float normal_furnace_volume = 1;
    public float normal_bee_volume = 0.4f;
    public float normal_collect_item_volume = 0.1f;
    public float normal_chest_open_volume = 0.5f;
    public float normal_jetpack_volume = 0.1f;
    public float normal_place_building_prefab_sound_volume = 1f;
    public float normal_heart_beat_sound_volume = 1;

    void Start()
    {
       
    }

    
    void Update()
    {
        
            
    }
}
