using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Stats : MonoBehaviour
{
    [Range(0, 100)]
    public float playerHealth = 100;
    [Range(0, 100)]
    public float playerFood = 100;
    [Range(0, 100)]
    public float playerPoison = 0;
    [SerializeField]
    private float asda;
    [SerializeField]
    private Image healthFill;
    [SerializeField]
    private Image foodFill;
    [SerializeField]
    private Image poisonFill;
    public float falling_damage;

    private float poisonDecrease = 1f;    //daca are mancare playeru poisinu scade cu 1 pe sec
    private float foodDecrease = 0.15f;         //cat scade food odata pe sec
    private float healthDecrease = 1;                     //cand nu are mancare deloc scade viara
    private float poison_damage = 5; //cat scade cand ai poision din viata
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
                playerHealth += playerFood * 0.02f * Time.deltaTime;                                        //daca ai mancare creste viata

            playerFood -= (foodDecrease + foodDecrease * (playerPoison / 10)) * Time.deltaTime;         //daca ai otrava cu cat ai mai multa cu atat scade mancarea mai repede daca nu ai scade normal
        }
        else
            playerHealth -= healthDecrease * Time.deltaTime;

        playerHealth -= (playerPoison / 100) * poison_damage * Time.deltaTime;                                      //daca ai otrava scade viata;    

        Limits();

        if (playerHealth <= 0)
            PlayerDie();


        if (playerHealth <= 20)
        {
            if (FindObjectOfType<Sounds_Player>().heart_beat_sound.isPlaying == false)
                FindObjectOfType<Sounds_Player>().heart_beat_sound.Play();
        }
        else
            FindObjectOfType<Sounds_Player>().heart_beat_sound.Stop();
    }

    void PlayerDie()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("World_Created", 0);
        SceneManager.LoadScene("Dead");
    }

    void Limits()
    {
        if (playerHealth < 0)
            playerHealth = 0;
        else if (playerHealth > 100)
            playerHealth = 100;

        if (playerFood < 0)
            playerFood = 0;
        else if (playerFood > 100)
            playerFood = 100;

        if (playerPoison < 0)
            playerPoison = 0;
        else if (playerPoison > 100)
            playerPoison = 100;
    }
}
