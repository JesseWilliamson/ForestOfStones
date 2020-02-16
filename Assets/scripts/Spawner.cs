using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.IO;

public class Spawner : MonoBehaviour
{


    bool healthPot;
    bool speedPot;

    public Transform[] spawnPoints;
    public GameObject[] hazards;

    public GameObject healthPotGameObject;
    public GameObject speedPotGameObject;

    private float timeBtwSpawns;
    public float startTimeBtwSpawns;


    void PlayerDeath()
    {
        SceneManager.LoadScene("EndScreen");
    }

    private void Start()
    {

        string savefile = Application.persistentDataPath + "/PlayerSave.json";
        print(File.Exists(savefile) ? "File exists for spawner." : "File does not exist for spawner.");

        if (File.Exists(savefile))
        {
            print("yesfor spawner");
        }
        else
        {
            Save();
            Load();
        }

        if (healthPot == true)
        {
            hazards[8] = healthPotGameObject;
        }

        if (speedPot == true)
        {
            hazards[9] = speedPotGameObject;
        }

    }

    void Save()
    {
        JSONObject playerJson = new JSONObject();

        playerJson.Add("healthPot", healthPot);
        playerJson.Add("speedPot", speedPot);

        print(playerJson.ToString());

        string path = Application.persistentDataPath + "/PlayerSave.json";
        File.WriteAllText(path, playerJson.ToString());
    }

    void Load()
    {
        string path = Application.persistentDataPath + "/PlayerSave.json";
        string JsonString = File.ReadAllText(path);
        JSONObject playerJson = (JSONObject)JSON.Parse(JsonString);

        healthPot = playerJson["healthPot"];
        speedPot = playerJson["speedPot"];

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") != null){
            
                if (timeBtwSpawns <= 0){

                    Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                    GameObject randomHazard = hazards[Random.Range(0, hazards.Length)];

                    Instantiate(randomHazard, randomSpawnPoint.position, Quaternion.identity);

                    timeBtwSpawns = startTimeBtwSpawns;

                }else {

                    timeBtwSpawns -= Time.deltaTime;

                }
                
        } else
        {
            Invoke("PlayerDeath", 2);
        }



    }
}
