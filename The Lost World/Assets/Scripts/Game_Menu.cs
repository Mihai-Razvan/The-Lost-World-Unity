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


    public void Exit()
    {
        Application.Quit();
    }
}
