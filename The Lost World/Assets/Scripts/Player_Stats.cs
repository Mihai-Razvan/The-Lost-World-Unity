using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour
{
    public float playerHealth = 100;
    public float playerFood = 100;
    public float playerPoison = 0;
    [SerializeField]
    private float asda;
    [SerializeField]
    private Image healthFill;
    [SerializeField]
    private Image foodFill;
    [SerializeField]
    private Image poisonFill;

    private float poisonDecrease = 1f;    //daca are mancare playeru poisinu scade cu 1 pe sec
    private float foodDecrease = 0.15f;         //cat scade food odata pe sec
    private float healthDecrease = 1;                     //cand nu are mancare deloc scade viara
    void Start()
    {
        
    }

    
    void Update()
    {
        healthFill.fillAmount = playerHealth / 100f;
        foodFill.fillAmount = playerFood / 100f;
        poisonFill.fillAmount = playerPoison / 100f;

        if (playerFood > 0)
        {
            if (playerPoison > 0)
                playerPoison -= poisonDecrease * Time.deltaTime;                                             //daca ai mancare scade otrava

            if (playerHealth < 100)
                playerHealth += playerFood * 1.5f * Time.deltaTime;                                        //daca ai mancare creste viata

            playerFood -= (foodDecrease + foodDecrease * (playerPoison / 25)) * Time.deltaTime;         //daca ai otrava cu cat ai mai multa cu atat scade mancarea mai repede daca nu ai scade normal
        }
        else
            playerHealth -= healthDecrease * Time.deltaTime;
        playerHealth -= (playerPoison / 100) * Time.deltaTime;                                      //daca ai otrava scade viata;


    }
}
