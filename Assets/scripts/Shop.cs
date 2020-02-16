using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text healthPotDisplay;
    public Text speedPotDisplay;
    public Text coinDisplay;

    int highScore;
    int coins;
    int prevScore;

    bool healthPot;
    bool speedPot;

    void Save()
    {
        JSONObject playerJson = new JSONObject();
        playerJson.Add("coins", coins);
        playerJson.Add("healthPot", healthPot);
        playerJson.Add("speedPot", speedPot);

        Debug.Log(playerJson.ToString());

        string path = Application.persistentDataPath + "/PlayerSave.json";
        File.WriteAllText(path, playerJson.ToString());
    }

    void Load()
    {
        string path = Application.persistentDataPath + "/PlayerSave.json";
        string JsonString = File.ReadAllText(path);
        JSONObject playerJson = (JSONObject)JSON.Parse(JsonString);

        coins = playerJson["coins"];
        healthPot = playerJson["healthPot"];
        speedPot = playerJson["speedPot"];
    }

    // Start is called before the first frame update
    void Start()
    {
        string savefile = Application.persistentDataPath + "/PlayerSave.json";
        print(File.Exists(savefile) ? "File exists." : "File does not exist.");

        if (File.Exists(savefile))
        {
            print("yes");
        }
        else
        {
            Save();
            Load();
        }

        Load();

        coinDisplay.text = coins.ToString();

        if (healthPot == true)
            {
            healthPotDisplay.text = "Purchased";
            }

        if  (speedPot == true)
            {
            speedPotDisplay.text = "Purchased";
            }


    }

    public void purchasehealthPot(){
        Load();
        if (coins >= 60 & healthPot != true){
            coins -= 60;
            healthPot = true;
            Save();
            healthPotDisplay.text = "Purchased";
            coinDisplay.text = coins.ToString();
        }
    }

    public void purchasespeedPot()
    {
        Load();
        if (coins >= 25 & speedPot != true){
            coins -= 25;
            speedPot = true;
            Save();
            speedPotDisplay.text = "Purchased";
            coinDisplay.text = coins.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
