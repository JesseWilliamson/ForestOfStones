using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    int highScore;
    int coins;
    int prevScore;
    public Text scoreDisplay;
    public Text highScoreDisplay;
    public Text coinDisplay;

    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/PlayerSave.json";
        string JsonString = File.ReadAllText(path);
        JSONObject playerJson = (JSONObject)JSON.Parse(JsonString);

        coins = playerJson["coins"];
        highScore = playerJson["highScore"];
        prevScore = playerJson["prevScore"];

        scoreDisplay.text = prevScore.ToString();
        highScoreDisplay.text = highScore.ToString();
        coinDisplay.text = coins.ToString();

    }

  
}
