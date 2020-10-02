using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject game_menu_panel;
    public bool game_menu_isactive;
    void Start()
    {
        
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

            FindObjectOfType<Acces_Building>().AccesedBuilding = null;
            FindObjectOfType<Inventory>().Item_004_Inventory_Panel.SetActive(false);
            FindObjectOfType<Inventory>().Item_030_Inventory_Panel.SetActive(false);
            FindObjectOfType<Acces_Building>().Building_Inventory_Opened = false;
        }
        
        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
