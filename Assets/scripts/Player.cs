using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.IO;


public class Player : MonoBehaviour
{

    public Text healthDisplay;
    public Text coinDisplay;
    public Text scoreDisplay;

    public int coins = 0;
    public float score = 0;
    public int scoreInt = 0;
    public int highScore = 0;
    public int prevScore = 0;
    public int health;
    public float speed = 7; 
    public float input;

    bool speedcollected;

    Rigidbody2D rb; 
    Animator anim;
    AudioSource source;

    bool healthPot = false;
    bool speedPot = false;


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

       
        
        
        //if (!File.Exists(Application.persistentDataPath + "/PlayerSave.json"))
        //{
        //    print("Save Found" );
        //}
        //else
        //{
        //    print("no save file found!");

        //    JSONObject playerJson = new JSONObject();

        //    playerJson.Add("coins", coins);
        //    playerJson.Add("highScore", highScore);
        //    playerJson.Add("prevScore", prevScore);
        //    playerJson.Add("healthPot", healthPot);
        //    playerJson.Add("speedPot", speedPot);

        //    print(playerJson.ToString());

        //    string path = Application.persistentDataPath + "/PlayerSave.json";
        //    File.WriteAllText(path, playerJson.ToString());
        //}

        Load(); 



        //        print(Application.persistentDataPath + "/PlayerSave.json");


        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();

        healthDisplay.text = health.ToString();
        coinDisplay.text = coins.ToString();
        scoreDisplay.text = score.ToString();
 

    }

    void Save()
    {
        JSONObject playerJson = new JSONObject();

        playerJson.Add("coins", coins);
        playerJson.Add("highScore", highScore);
        playerJson.Add("prevScore", prevScore);
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

        coins = playerJson["coins"];
        highScore = playerJson["highScore"];
        prevScore = playerJson["prevScore "];
        healthPot = playerJson["healthPot"];
        speedPot = playerJson["speedPot"];

    }

    void Update()
    {

        if (input != 0){
            anim.SetBool("PlayerIsRunning", true);
        } else {
            anim.SetBool("PlayerIsRunning", false);
        }

        score += Time.deltaTime;
        scoreInt = (int)Mathf.Round(score);
        scoreDisplay.text = scoreInt.ToString();

        if (input > 0){
            transform.eulerAngles = new Vector3(0, 180, 0);
        } else if (input < 0){
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

    }


    
    void FixedUpdate()
    {
        input = SimpleInput.GetAxisRaw("Horizontal");


        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }

    public void GetCoin(int coinAmmount)
    {
        source.Play();

        coins += coinAmmount;
        score += 5;
        coinDisplay.text = coins.ToString();

    }

    public void speedBoost()
    {
        StartCoroutine(EnableTimedBonus());
    }

    IEnumerator EnableTimedBonus()
    {
        print("speedboost");
        speed = 14;
        yield return new WaitForSeconds(5.0f);
        speed = 7;
        print("speedboost over");
    }


    public void TakeDamage(int damageAmmount) {


        
        health -= damageAmmount;

        if (health >= 0){
            healthDisplay.text = health.ToString();
        } else {healthDisplay.text = 0.ToString();}

        if (health <= 0)
        {
            //destroy player
            prevScore = (int)Mathf.Round(score);
            if (prevScore > highScore)
            {
                highScore = prevScore;
            }
            Save();
            Destroy(gameObject);
            SceneManager.LoadScene("EndScreen");
        }
    }

    public void Heal(int damageAmmount)
    {


        if (health < 10)
        {
            health += damageAmmount;
            healthDisplay.text = health.ToString();
        }



    }



}
       