using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Game_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject game_menu_panel;
    public bool game_menu_isactive;
    [SerializeField]
    private GameObject settings_panel;
    public Slider ambience_volume_Slider;
    public Slider master_volume_Slider;
    void Start()
    {
        game_menu_panel.SetActive(false);
        settings_panel.SetActive(false);
    }

    
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if(game_menu_isactive == false)
            {
                game_menu_panel.SetActive(true);
                game_menu_isactive = true;
                CloseOtherMenues();
            }         
        }

        if(settings_panel.activeInHierarchy == true)
        {
            FindObjectOfType<Sounds_Player>().wind_sound.volume = ambience_volume_Slider.value * FindObjectOfType<Sounds_Player>().normal_wind_volume;

            FindObjectOfType<Sounds_Player>().collect_item_sound.volume = master_volume_Slider.value * FindObjectOfType<Sounds_Player>().normal_collect_item_volume;
            FindObjectOfType<Sounds_Player>().chest_open_sound.volume = master_volume_Slider.value * FindObjectOfType<Sounds_Player>().normal_chest_open_volume;
            FindObjectOfType<Sounds_Player>().jetpack_sound.volume = master_volume_Slider.value * FindObjectOfType<Sounds_Player>().normal_jetpack_volume;
            FindObjectOfType<Sounds_Player>().place_building_prefab_sound.volume = master_volume_Slider.value * FindObjectOfType<Sounds_Player>().normal_place_building_prefab_sound_volume;
            FindObjectOfType<Sounds_Player>().heart_beat_sound.volume = master_volume_Slider.value * FindObjectOfType<Sounds_Player>().normal_heart_beat_sound_volume;
        }

    }

    public void Resume()
    {
        game_menu_panel.SetActive(false);
        game_menu_isactive = false;
    }

    public void SaveGame()
    {
        FindObjectOfType<Save>().SaveFunction();
    }

    public void MainMenu()
    {
        FindObjectOfType<Save>().SaveFunction();
        SceneManager.LoadScene("Menu");
    }


    private void CloseOtherMenues()
    {
        FindObjectOfType<Inventory>().inventory_craftingIsActive = false;
        FindObjectOfType<Inventory>().Inventory_Crafting_Panel.SetActive(false);

        if(FindObjectOfType<Acces_Building>().AccesedBuilding != null)
        {
            if (FindObjectOfType<Acces_Building>().AccesedBuilding.tag == "Furnace")
                FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_004>().BuildingAccessed = false;
            else if (FindObjectOfType<Acces_Building>().AccesedBuilding.tag == "Chest")
                FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_030>().BuildingAccessed = false;
            else if (FindObjectOfType<Acces_Building>().AccesedBuilding.tag == "Cooking pot")
                FindObjectOfType<Acces_Building>().AccesedBuilding.GetComponent<Item_035>().BuildingAccessed = false;

            FindObjectOfType<Acces_Building>().AccesedBuilding = null;
            FindObjectOfType<Inventory>().Item_004_Inventory_Panel.SetActive(false);
            FindObjectOfType<Inventory>().Item_030_Inventory_Panel.SetActive(false);
            FindObjectOfType<Inventory>().Item_035_Crafting_Panel.SetActive(false);
            FindObjectOfType<Acces_Building>().Building_Inventory_Opened = false;
        }
        
        
    }


    public void SettingsButton()
    {
        settings_panel.SetActive(true);
    }


    public void Back()           //butonu de back din settings menu
    {
        settings_panel.SetActive(false);
    }

    public void Exit()
    {
        FindObjectOfType<Save>().SaveFunction();
        Application.Quit();
    }


}
