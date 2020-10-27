using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{                                //dupa ce ffac baterii la jetpack pun inclusiv 25 la save si load player la inventar
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private LayerMask Island_Relief_SaveableMask;     //masku pt insula si relie sa salveze
    [SerializeField]
    private LayerMask Buiding_SaveableMask;
    [SerializeField]
    private LayerMask Collectable_SaveableMask;
    [SerializeField]
    private LayerMask islandMask;        //sa devina child la insula relieu sa se poate despawna

    [SerializeField]
    private int numberOfIslands;
    [SerializeField]
    private int numberOfRelief;
    [SerializeField]
    private int numberOfBuildings;
    [SerializeField]
    private int numberOfCollectables;


    [SerializeField]
    private GameObject island_type_1;      //forest
    [SerializeField]
    private GameObject island_type_2;      //snow
    [SerializeField]
    private GameObject island_type_3;      //desert


    [SerializeField]
    private GameObject[] relief_type;
    [SerializeField]
    private GameObject[] building_type;
    [SerializeField]
    private GameObject[] collectable_type;

    [SerializeField]
    private GameObject cactus;  //cand respawneaza sap collector sa respawneze si un cactus cu el ca altfel e pe nmk sap collectoru

    [SerializeField]
    private GameObject special_slot;

    private void Start()
    {
        LoadFunction();
       
    }
    void Update()
    {

    }

    private void OnApplicationQuit()
    {
       // SaveFunction();

    }

    public void SaveFunction()
    {
        PlayerPrefs.DeleteAll();

        SavePlayer();     
        SaveIslandsANDRelief();
        SaveBuildings();
        SaveCollectables();
    }

    public void LoadFunction()
    {
        LoadPlayer();
        LoadIslandsANDRelief();
        LoadBuildings();
        LoadCollectables();
    }


    void SavePlayer()
    {
        PlayerPrefs.SetFloat("Player_Position_X", player.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_Y", player.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_Z", player.transform.position.z);
        PlayerPrefs.SetFloat("Player_Health", FindObjectOfType<Player_Stats>().playerHealth);
        PlayerPrefs.SetFloat("Player_Food", FindObjectOfType<Player_Stats>().playerFood);
        PlayerPrefs.SetFloat("Player_Poison", FindObjectOfType<Player_Stats>().playerPoison);
        
        for (int i = 1; i < 25; i ++)  //dupa ce ffac baterii la jetpack pun inclusiv 25
        {
            PlayerPrefs.SetInt("Inventory_Item_Code_Slot_" + i.ToString(), FindObjectOfType<Inventory>().Slot_Item_Code[i]);
            PlayerPrefs.SetInt("Inventory_Item_Quantity_Slot_" + i.ToString(), FindObjectOfType<Inventory>().Slot_Item_Quantity[i]);
        }

        PlayerPrefs.SetInt("Inventory_Battery_Code_Special_Slot", special_slot.GetComponent<Inventory_slots>().batteryCode);
        PlayerPrefs.SetFloat("Inventory_Battery_Charge_Special_Slot", special_slot.GetComponent<Inventory_slots>().batteryCharge);
        PlayerPrefs.SetFloat("Inventory_Battery_Max_Charge_Special_Slot", special_slot.GetComponent<Inventory_slots>().maxcharge);

        PlayerPrefs.SetInt("First_Created_World", 1);
    }


    void LoadPlayer()
    {
        float playerX = PlayerPrefs.GetFloat("Player_Position_X");
        float playerY = PlayerPrefs.GetFloat("Player_Position_Y");
        float playerZ = PlayerPrefs.GetFloat("Player_Position_Z");
        player.transform.position = new Vector3(playerX, playerY, playerZ);

        FindObjectOfType<Player_Stats>().playerHealth = PlayerPrefs.GetFloat("Player_Health");
        FindObjectOfType<Player_Stats>().playerFood = PlayerPrefs.GetFloat("Player_Food");
        FindObjectOfType<Player_Stats>().playerPoison = PlayerPrefs.GetFloat("Player_Poison");
        
        for (int i = 1; i < 25; i++) //dupa ce ffac baterii la jetpack pun inclusiv 25
        {
            FindObjectOfType<Inventory>().Slot_Item_Code[i] = PlayerPrefs.GetInt("Inventory_Item_Code_Slot_" + i.ToString());
            FindObjectOfType<Inventory>().Slot_Item_Quantity[i] = PlayerPrefs.GetInt("Inventory_Item_Quantity_Slot_" + i.ToString());
        }

          special_slot.GetComponent<Inventory_slots>().batteryCode = PlayerPrefs.GetInt("Inventory_Battery_Code_Special_Slot");
          special_slot.GetComponent<Inventory_slots>().batteryCharge = PlayerPrefs.GetFloat("Inventory_Battery_Charge_Special_Slot");
          special_slot.GetComponent<Inventory_slots>().maxcharge = PlayerPrefs.GetFloat("Inventory_Battery_Max_Charge_Special_Slot");
    }





    void SaveIslandsANDRelief()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, 7000, Island_Relief_SaveableMask);

        numberOfIslands = 0;
        numberOfRelief = 0;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Island Point Type 1" || colliders[i].tag == "Island Point Type 2" || colliders[i].tag == "Island Point Type 3")
            {
                numberOfIslands++;

                PlayerPrefs.SetInt("String_island_Type_" + numberOfIslands.ToString(), GetIslandType(colliders, i));
                PlayerPrefs.SetFloat("String_island_X_" + numberOfIslands.ToString(), colliders[i].transform.position.x);
                PlayerPrefs.SetFloat("String_island_Y_" + numberOfIslands.ToString(), colliders[i].transform.position.y);
                PlayerPrefs.SetFloat("String_island_Z_" + numberOfIslands.ToString(), colliders[i].transform.position.z);
            }
            else if (colliders[i].tag == "Relief Point Type 1" || colliders[i].tag == "Relief Point Type 2" || colliders[i].tag == "Relief Point Type 3" || colliders[i].tag == "Relief Point Type 4" || colliders[i].tag == "Relief Point Type 5" || colliders[i].tag == "Relief Point Type 6" || colliders[i].tag == "Relief Point Type 7" || colliders[i].tag == "Relief Point Type 8" || colliders[i].tag == "Relief Point Type 9" || colliders[i].tag == "Relief Point Type 10" || colliders[i].tag == "Relief Point Type 11" || colliders[i].tag == "Relief Point Type 12" || colliders[i].tag == "Relief Point Type 13" || colliders[i].tag == "Relief Point Type 14" || colliders[i].tag == "Relief Point Type 15" || colliders[i].tag == "Relief Point Type 16")
            {
                numberOfRelief++;

                PlayerPrefs.SetInt("String_relief_Type_" + numberOfRelief.ToString(), GetReliefType(colliders, i));
                PlayerPrefs.SetFloat("String_relief_X_" + numberOfRelief.ToString(), colliders[i].transform.position.x);
                PlayerPrefs.SetFloat("String_relief_Y_" + numberOfRelief.ToString(), colliders[i].transform.position.y);
                PlayerPrefs.SetFloat("String_relief_Z_" + numberOfRelief.ToString(), colliders[i].transform.position.z);
            }
        }

        PlayerPrefs.SetInt("Number_Of_Islands", numberOfIslands);
        PlayerPrefs.SetInt("Number_Of_Relief", numberOfRelief);
        PlayerPrefs.SetInt("World_Created", 1);
    }

    void LoadIslandsANDRelief()
    {
        numberOfIslands = PlayerPrefs.GetInt("Number_Of_Islands");
        numberOfRelief = PlayerPrefs.GetInt("Number_Of_Relief");

        for (int i = 1; i <= numberOfIslands; i++)
        {

            float xPos = PlayerPrefs.GetFloat("String_island_X_" + i.ToString());
            float yPos = PlayerPrefs.GetFloat("String_island_Y_" + i.ToString());
            float zPos = PlayerPrefs.GetFloat("String_island_Z_" + i.ToString());

            if (PlayerPrefs.GetInt("String_island_Type_" + i.ToString()) == 1)
            {
                GameObject spawned = Instantiate(island_type_1, new Vector3(xPos, yPos, zPos), Quaternion.identity);
                spawned.GetComponent<IslandObjects_Forest>().Respawned = true;
            }
            else if (PlayerPrefs.GetInt("String_island_Type_" + i.ToString()) == 2)
            {
                GameObject spawned = Instantiate(island_type_2, new Vector3(xPos, yPos, zPos), Quaternion.identity);
                spawned.GetComponent<IslandObjects_Snow>().Respawned = true;
            }
            else if (PlayerPrefs.GetInt("String_island_Type_" + i.ToString()) == 3)
            {
                GameObject spawned = Instantiate(island_type_3, new Vector3(xPos, yPos, zPos), Quaternion.identity);
                spawned.GetComponent<IslandObjects_Desert>().Respawned = true;
            }
        }


        for (int i = 1; i <= numberOfRelief; i++)
        {
            // test[i] = PlayerPrefs.GetInt("String_relief_Type_" + i.ToString());
            float xPos = PlayerPrefs.GetFloat("String_relief_X_" + i.ToString());
            float yPos = PlayerPrefs.GetFloat("String_relief_Y_" + i.ToString());
            float zPos = PlayerPrefs.GetFloat("String_relief_Z_" + i.ToString());

            RaycastHit hit;
            if (Physics.Raycast(new Vector3(xPos, yPos + 50, zPos), -transform.up, out hit, 100, islandMask))
            {
                GameObject spawnedrelief = Instantiate(relief_type[PlayerPrefs.GetInt("String_relief_Type_" + i.ToString())], hit.point, Quaternion.identity);
                spawnedrelief.transform.SetParent(hit.collider.transform);
            }


        }
    }




    void SaveBuildings()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, 100000, Buiding_SaveableMask);

        numberOfBuildings = 0;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (GetBuildingType(colliders, i) != 0) //inseamna ca e o cladire
            {
                numberOfBuildings++;

                PlayerPrefs.SetInt("String_building_Type_" + numberOfBuildings.ToString(), GetBuildingType(colliders, i));
                PlayerPrefs.SetFloat("String_building_Position_X_" + numberOfBuildings.ToString(), colliders[i].transform.position.x);
                PlayerPrefs.SetFloat("String_building_Position_Y_" + numberOfBuildings.ToString(), colliders[i].transform.position.y);
                PlayerPrefs.SetFloat("String_building_Position_Z_" + numberOfBuildings.ToString(), colliders[i].transform.position.z);
                PlayerPrefs.SetFloat("String_building_Rotation_X_" + numberOfBuildings.ToString(), colliders[i].transform.eulerAngles.x);
                PlayerPrefs.SetFloat("String_building_Rotation_Y_" + numberOfBuildings.ToString(), colliders[i].transform.eulerAngles.y);
                PlayerPrefs.SetFloat("String_building_Rotation_Z_" + numberOfBuildings.ToString(), colliders[i].transform.eulerAngles.z);

                if (GetBuildingType(colliders, i) == 4)       // furnace
                    SaveFurnaceDetails(colliders, i);
                else if (GetBuildingType(colliders, i) == 26)
                    SaveSapExtractorDetails(colliders, i);
                else if (GetBuildingType(colliders, i) == 30)
                    SaveChestDetails(colliders, i);
                else if (GetBuildingType(colliders, i) == 35)
                    SaveCookingPotDetails(colliders, i);
            }
        }


        PlayerPrefs.SetInt("Number_Of_Buildings", numberOfBuildings);

    }

    void LoadBuildings()
    {
        numberOfBuildings = PlayerPrefs.GetInt("Number_Of_Buildings");

        for (int i = 1; i <= numberOfBuildings; i++)
        {
            float xPos = PlayerPrefs.GetFloat("String_building_Position_X_" + i.ToString());
            float yPos = PlayerPrefs.GetFloat("String_building_Position_Y_" + i.ToString());
            float zPos = PlayerPrefs.GetFloat("String_building_Position_Z_" + i.ToString());
            float xRotation = PlayerPrefs.GetFloat("String_building_Rotation_X_" + i.ToString());
            float yRotation = PlayerPrefs.GetFloat("String_building_Rotation_Y_" + i.ToString());
            float zRotation = PlayerPrefs.GetFloat("String_building_Rotation_Z_" + i.ToString());

            GameObject spawnedBuilding = Instantiate(building_type[PlayerPrefs.GetInt("String_building_Type_" + i.ToString())], new Vector3(xPos, yPos, zPos), Quaternion.Euler(xRotation, yRotation, zRotation));      //CAND FAC CLADIRILE SA IE CHILD LA INSULE SA AC SI ACI CAND SE RESPAWNEAZ       

            if (PlayerPrefs.GetInt("String_building_Type_" + i.ToString()) == 4)   //furnace
                LoadFurnaceDetails(spawnedBuilding, i);
            else if (PlayerPrefs.GetInt("String_building_Type_" + i.ToString()) == 26)  //sap extractor
                LoadSapExtractorDetails(spawnedBuilding, i);
            else if (PlayerPrefs.GetInt("String_building_Type_" + i.ToString()) == 30)  //chest
                LoadChestDetails(spawnedBuilding, i);
            else if (PlayerPrefs.GetInt("String_building_Type_" + i.ToString()) == 35)  //cooking pot
                LoadCookingPotDetails(spawnedBuilding, i);

        }
    }




    void SaveCollectables()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, 100000, Collectable_SaveableMask);

        numberOfCollectables = 0;

        for (int i = 0; i < colliders.Length; i++)
        {
            
            if (GetCollectableType(colliders, i) != 0) //inseamna ca e o collectable
            {
                Debug.Log("colll");
                numberOfCollectables++;

                PlayerPrefs.SetInt("String_collectable_Type_" + numberOfCollectables.ToString(), GetCollectableType(colliders, i));
                PlayerPrefs.SetFloat("String_collectable_Position_X_" + numberOfCollectables.ToString(), colliders[i].transform.position.x);
                PlayerPrefs.SetFloat("String_collectable_Position_Y_" + numberOfCollectables.ToString(), colliders[i].transform.position.y);
                PlayerPrefs.SetFloat("String_collectable_Position_Z_" + numberOfCollectables.ToString(), colliders[i].transform.position.z);
              /*  PlayerPrefs.SetFloat("String_collectable_Rotation_X_" + numberOfCollectables.ToString(), colliders[i].transform.eulerAngles.x);
                PlayerPrefs.SetFloat("String_collectable_Rotation_Y_" + numberOfCollectables.ToString(), colliders[i].transform.eulerAngles.y);
                PlayerPrefs.SetFloat("String_collectable_Rotation_Z_" + numberOfCollectables.ToString(), colliders[i].transform.eulerAngles.z); */

            }
        }


        PlayerPrefs.SetInt("Number_Of_Collectables", numberOfCollectables);

    }


    void LoadCollectables()
    {
        numberOfCollectables = PlayerPrefs.GetInt("Number_Of_Collectables");

        for (int i = 1; i <= numberOfCollectables; i++)
        {
            float xPos = PlayerPrefs.GetFloat("String_collectable_Position_X_" + i.ToString());
            float yPos = PlayerPrefs.GetFloat("String_collectable_Position_Y_" + i.ToString());
            float zPos = PlayerPrefs.GetFloat("String_collectable_Position_Z_" + i.ToString());
           /* float xRotation = PlayerPrefs.GetFloat("String_collectable_Rotation_X_" + i.ToString());
            float yRotation = PlayerPrefs.GetFloat("String_collectable_Rotation_Y_" + i.ToString());
            float zRotation = PlayerPrefs.GetFloat("String_collectable_Rotation_Z_" + i.ToString()); */

            Instantiate(collectable_type[PlayerPrefs.GetInt("String_collectable_Type_" + i.ToString())], new Vector3(xPos, yPos, zPos), Quaternion.identity);      //CAND FAC CLADIRILE SA IE CHILD LA INSULE SA AC SI ACI CAND SE RESPAWNEAZ       

        }
    }


    private int GetIslandType(Collider[] colliders, int i)
    {

        if (colliders[i].tag == "Island Point Type 1")
            return 1;
        else if (colliders[i].tag == "Island Point Type 2")
            return 2;
        else if (colliders[i].tag == "Island Point Type 3")      
            return 3;
        


        return 0;
    }


    private int GetReliefType(Collider[] colliders, int i)
    {
        for (int j = 1; j <= 16; j++)
            if (colliders[i].tag == "Relief Point Type " + j.ToString())
                return j;

        return 0;    
    }


    private int GetBuildingType(Collider[] colliders, int i)
    {
        for (int j = 1; j <= 9; j++)
            if (colliders[i].tag == "Item Point 00" + j.ToString())
                return j;

        for (int j = 10; j <= 99; j++)
            if (colliders[i].tag == "Item Point 0" + j.ToString())
                return j;

        return 0;
    }

    private int GetCollectableType(Collider[] colliders, int i)
    {
        for (int j = 1; j <= 9; j++)
            if (colliders[i].tag == "Collectable Point 00" + j.ToString())
                return j;

        for (int j = 10; j <= 99; j++)
            if (colliders[i].tag == "Collectable Point 0" + j.ToString())
                return j;

        return 0;
    }




    //BUILDINGS DETAILS

    void SaveFurnaceDetails(Collider[] colliders, int i)
    {
        for (int j = 1; j <= 20; j++)
        {
            PlayerPrefs.SetInt("String_building_" + numberOfBuildings.ToString() + "_Inventory_Slot_" + j.ToString() + "_Item_Code", colliders[i].transform.GetChild(0).GetComponent<Item_004>().Slot_Item_Code[j]);
            PlayerPrefs.SetInt("String_building_" + numberOfBuildings.ToString() + "_Inventory_Slot_" + j.ToString() + "_Item_Quantity", colliders[i].transform.GetChild(0).GetComponent<Item_004>().Slot_Item_Quantity[j]);
        }

        PlayerPrefs.SetFloat("String_building_" + numberOfBuildings.ToString() + "_Fuel", colliders[i].transform.GetChild(0).GetComponent<Item_004>().fuel);
        PlayerPrefs.SetFloat("String_building_" + numberOfBuildings.ToString() + "_Transforming_Time_Left", colliders[i].transform.GetChild(0).GetComponent<Item_004>().Transforming_Time_Left);
        PlayerPrefs.SetInt("String_building_" + numberOfBuildings.ToString() + "_Produced_Item_Code", colliders[i].transform.GetChild(0).GetComponent<Item_004>().produced_item_code);
        PlayerPrefs.SetInt("String_building_" + numberOfBuildings.ToString() + "_Quantity_To_Add", colliders[i].transform.GetChild(0).GetComponent<Item_004>().quantityToAdd);
    }

    void LoadFurnaceDetails(GameObject spawnedBuilding, int i)
    {
        for(int j = 1; j <= 20; j ++)
        {
            spawnedBuilding.transform.GetChild(0).GetComponent<Item_004>().Slot_Item_Code[j] = PlayerPrefs.GetInt("String_building_" + i.ToString() + "_Inventory_Slot_" + j.ToString() + "_Item_Code");
            spawnedBuilding.transform.GetChild(0).GetComponent<Item_004>().Slot_Item_Quantity[j] = PlayerPrefs.GetInt("String_building_" + i.ToString() + "_Inventory_Slot_" + j.ToString() + "_Item_Quantity");
        }

        spawnedBuilding.transform.GetChild(0).GetComponent<Item_004>().fuel = PlayerPrefs.GetFloat("String_building_" + i.ToString() + "_Fuel");
        spawnedBuilding.transform.GetChild(0).GetComponent<Item_004>().Transforming_Time_Left = PlayerPrefs.GetFloat("String_building_" + i.ToString() + "_Transforming_Time_Left");
        spawnedBuilding.transform.GetChild(0).GetComponent<Item_004>().produced_item_code = PlayerPrefs.GetInt("String_building_" + i.ToString() + "_Produced_Item_Code");
        spawnedBuilding.transform.GetChild(0).GetComponent<Item_004>().quantityToAdd = PlayerPrefs.GetInt("String_building_" + i.ToString() + "_Quantity_To_Add");
    }



    void SaveSapExtractorDetails(Collider[] colliders, int i)
    {
        PlayerPrefs.SetFloat("String_building_" + numberOfBuildings.ToString() + "_Time_On_This_Round", colliders[i].GetComponent<Item_026>().time_On_This_Round);
        /*
        PlayerPrefs.SetFloat("String_Cactus_X_" + numberOfBuildings.ToString(), colliders[i].GetComponent<Item_026>().attached_cactus_rotation.x);
        PlayerPrefs.SetFloat("String_Cactus_Y_" + numberOfBuildings.ToString(), colliders[i].GetComponent<Item_026>().attached_cactus_rotation.y);
        PlayerPrefs.SetFloat("String_Cactus_Z_" + numberOfBuildings.ToString(), colliders[i].GetComponent<Item_026>().attached_cactus_rotation.z);
        */
    }

    void LoadSapExtractorDetails(GameObject spawnedBuilding, int i)
    {
        spawnedBuilding.GetComponent<Item_026>().time_On_This_Round = PlayerPrefs.GetFloat("String_building_" + i.ToString() + "_Time_On_This_Round");
     // Vector3 cactus_rotation = new Vector3(PlayerPrefs.GetFloat("String_Cactus_X_" + i.ToString()), PlayerPrefs.GetFloat("String_Cactus_Y_" + i.ToString()), PlayerPrefs.GetFloat("String_Cactus_Z_" + i.ToString()));
        GameObject spawnedCactus = Instantiate(cactus, spawnedBuilding.GetComponent<Item_026>().cactusSpawnPoint.transform.position, Quaternion.identity);
        spawnedCactus.gameObject.tag = "Undespawnable Object";
    }



    void SaveChestDetails(Collider[] colliders, int i)
    {
        for (int j = 1; j <= 20; j++)
        {
            PlayerPrefs.SetInt("String_building_" + numberOfBuildings.ToString() + "_Inventory_Slot_" + j.ToString() + "_Item_Code", colliders[i].transform.GetChild(0).GetComponent<Item_030>().Slot_Item_Code[j]);
            PlayerPrefs.SetInt("String_building_" + numberOfBuildings.ToString() + "_Inventory_Slot_" + j.ToString() + "_Item_Quantity", colliders[i].transform.GetChild(0).GetComponent<Item_030>().Slot_Item_Quantity[j]);
        }
    }

    void LoadChestDetails(GameObject spawnedBuilding, int i)
    {
        for (int j = 1; j <= 20; j++)
        {
            spawnedBuilding.transform.GetChild(0).GetComponent<Item_030>().Slot_Item_Code[j] = PlayerPrefs.GetInt("String_building_" + i.ToString() + "_Inventory_Slot_" + j.ToString() + "_Item_Code");
            spawnedBuilding.transform.GetChild(0).GetComponent<Item_030>().Slot_Item_Quantity[j] = PlayerPrefs.GetInt("String_building_" + i.ToString() + "_Inventory_Slot_" + j.ToString() + "_Item_Quantity");
        }
    }


    void SaveCookingPotDetails(Collider[] colliders, int i)
    {
        PlayerPrefs.SetFloat("String_building_" + numberOfBuildings.ToString() + "_Energy_Left", colliders[i].transform.GetChild(0).GetComponent<Item_035>().energy_left);
        
        for (int j = 1; j <= 5; j++)
        {
            PlayerPrefs.SetInt("String_building_" + numberOfBuildings.ToString() + "_Ingredient_Slot_" + j.ToString() + "_Item_Code", colliders[i].transform.GetChild(0).GetComponent<Item_035>().cooking_pot_incredients_item_code[j]);
            PlayerPrefs.SetInt("String_building_" + numberOfBuildings.ToString() + "_Ingredient_Slot_" + j.ToString() + "_Item_Quantity", colliders[i].transform.GetChild(0).GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j]);
        }
            
    }

    void LoadCookingPotDetails(GameObject spawnedBuilding, int i)
    {
        spawnedBuilding.transform.GetChild(0).GetComponent<Item_035>().energy_left = PlayerPrefs.GetFloat("String_building_" + numberOfBuildings.ToString() + "_Energy_Left");

        for (int j = 1; j <= 5; j++)
        {
            spawnedBuilding.transform.GetChild(0).GetComponent<Item_035>().cooking_pot_incredients_item_code[j] = PlayerPrefs.GetInt("String_building_" + numberOfBuildings.ToString() + "_Ingredient_Slot_" + j.ToString() + "_Item_Code");
            spawnedBuilding.transform.GetChild(0).GetComponent<Item_035>().cooking_pot_incredients_item_quantity[j] = PlayerPrefs.GetInt("String_building_" + numberOfBuildings.ToString() + "_Ingredient_Slot_" + j.ToString() + "_Item_Quantity");
        }
    }

}
