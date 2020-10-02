using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead_Menu : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Cursor.visible = true;
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");       
    }

    public void Exit()
    {
        Application.Quit();
    }
}
