using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    //GAME MANAGER VARIABLES
    public static GameManager instance = null; //Declaration of singleton


    //UI VARIABLES
    [SerializeField] private GameObject[] panels;


    //LeaderBoard
    [SerializeField] private InputField leaderBoardName;
    private string[] highScoresString = new string[10]; //The Highest scores in string
    private int[] highScoresInt = new int[10]; //the Highest scores in string
    private string[] highScoresFull = new string[10];
    private int posScoreBeaten = 10; //position of the score that is beaten
    private string postName = "";
    [SerializeField] public Text leaderBoardTxt;
    [SerializeField] private Text gameOverTxt;
    [SerializeField] private Text gameOverHSTxT;

    private Inventory inventory;

    [SerializeField] private Text timerTxt;
    private float timer;

    [SerializeField] private GameObject enemyE;
    [SerializeField] private GameObject enemyME;
    [SerializeField] private GameObject enemyM;
    [SerializeField] private GameObject enemyMH;
    [SerializeField] private GameObject enemyH;
    [SerializeField] private int nbEnemy = 50;
    private Vector3 posEnemy;
    [SerializeField] private GameObject player;
    private GameObject[] clone;
    private float range = 10f;

    // Start is called before the first frame update
    void Start()
    {
        clone = new GameObject[nbEnemy];
        enemyE.GetComponent<AIControl>().target = player.transform;
        enemyME.GetComponent<AIControl>().target = player.transform;
        enemyM.GetComponent<AIControl>().target = player.transform;
        enemyMH.GetComponent<AIControl>().target = player.transform;
        enemyH.GetComponent<AIControl>().target = player.transform;
        AddEnemies();

        timer = 30f;
        inventory = Inventory.instance;
        PanelToggle(0);
        for (int i = 0; i < 10; i++)
        {
            highScoresFull[i] = PlayerPrefs.GetString("HighScoreFull" + i.ToString(), "ND  0");//Read all HighScores, if there is none write "ND 0"
            highScoresInt[i] = PlayerPrefs.GetInt("HighScoreInt" + i.ToString(), highScoresInt[i]);
            highScoresString[i] = PlayerPrefs.GetString("HighScoreString" + i.ToString(), highScoresString[i]);
        }

        ResetLeaderBoard(); //Uncomment to reset LeaderBoard at start

        posScoreBeaten = 10;
        UpdateInGameLeaderBoard();
    }

    bool gameover = false;
    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (gameover == false)
        {
            gameover = true;
            GameOver();
        }

        timerTxt.text = Mathf.RoundToInt(timer).ToString() + "sec";
    }



    //GAME MANAGER METHODS
    private void Awake()
    {
        //We make sure only one instance exists
        if (instance == null) //if no instance is defined
        {
            instance = this; //this is the instance
        }
        else if (instance != this)
        {
            Destroy(gameObject); //destroy yourself!
        }
    }



    //UI METHODS
    public void PanelToggle(int position) //Used to switch from a pannel to another
    {
        Input.ResetInputAxes();
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(position == i);
            //if (position == i)
            //{
            //    defaultButtons[i].Select();
            //}
        }
    }

    public void doPause()
    {
        Time.timeScale = 0;
        PanelToggle(1);
    }

    public void doPlay()
    {
        Time.timeScale = 1;
        PanelToggle(0);
    }


    //Save High Score to LeaderBoard
    public void SaveHighScore()
    {
        if (leaderBoardName.text.Length == 1)
        {
            postName = "   ";
        }
        else if (leaderBoardName.text.Length == 2)
        {
            postName = "  ";
        }
        if (leaderBoardName.text.Length == 3)
        {
            postName = " ";
        }

        if (inventory.money > highScoresInt[9])
        {
            for (int i = 0; i < 10; i++)
            {
                if (inventory.money > highScoresInt[i])
                {
                    posScoreBeaten = i;
                    break;
                }
            }

            for (int i = 9; i >= posScoreBeaten; i--)
            {
                if (i == posScoreBeaten)
                {
                    highScoresInt[i] = inventory.money;
                    highScoresString[i] = leaderBoardName.text;
                }
                else
                {
                    highScoresInt[i] = highScoresInt[i - 1];
                    highScoresString[i] = highScoresString[i - 1];
                }
                PlayerPrefs.SetInt("HighScoreInt" + i.ToString(), highScoresInt[i]);
                PlayerPrefs.SetString("HighScoreString" + i.ToString(), highScoresString[i]);
                PlayerPrefs.SetString("HighScoreFull" + i.ToString(), highScoresString[i] + postName + highScoresInt[i]);
                PlayerPrefs.Save();
                highScoresFull[i] = PlayerPrefs.GetString("HighScoreFull" + i.ToString(), "ND  00000000");
            }
        }
        UpdateInGameLeaderBoard();
    }

    public void UpdateInGameLeaderBoard()
    {
        leaderBoardTxt.text = "1.  " + highScoresFull[0] + "$" + "\n" + "2.  " + highScoresFull[1] + "$" + "\n" + "3.  " + highScoresFull[2] + "$" + "\n" + "4.  " + highScoresFull[3] + "$" + "\n" + "5.  " + highScoresFull[4] + "$" + "\n" + "6.  " + highScoresFull[5] + "$" + "\n" + "7.  " + highScoresFull[6] + "$" + "\n" + "8.  " + highScoresFull[7] + "$" + "\n" + "9.  " + highScoresFull[8] + "$" + "\n" + "10. " + highScoresFull[9] + "$";
    }

    public void ResetLeaderBoard() //Can be used to rested leaderBoard
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt("HighScoreInt" + i.ToString(), 00000000);
            PlayerPrefs.SetString("HighScoreString" + i.ToString(), "ND");
            PlayerPrefs.SetString("HighScoreFull" + i.ToString(), "ND  00000000");
            PlayerPrefs.Save();
        }
    }

    public void GameOver()
    {
        for (int i = 0; i < nbEnemy; i++)
        {
            clone[i].SetActive(false);
        }
        Time.timeScale = 0;
        gameOverTxt.text = "GAME OVER! \n" + inventory.moneyTxt.text;
        gameOverHSTxT.text = gameOverTxt.text;
        if (inventory.money > highScoresInt[9])
        {
            PanelToggle(6);
        }
        else
        {
            PanelToggle(4);
        }
    }

    public void DoRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void AddEnemies()
    {

        for (int i = 0; i < nbEnemy; i++)
        {

            posEnemy = new Vector3(Random.Range(-75f, 75f), 32f, Random.Range(-100f, 100f));
            NavMeshHit hit;
            if (NavMesh.SamplePosition(posEnemy, out hit, range, NavMesh.AllAreas))
            {
                posEnemy = hit.position;
                int type = Random.Range(0, 100);
                if (type <= 100 && type > 65)
                {
                    clone[i] = Instantiate(enemyE, posEnemy, Quaternion.identity);
                    enemyE.GetComponent<AIControl>().target = player.transform;
                }
                else if (type <= 65 && type > 35)
                {
                    clone[i] = Instantiate(enemyME, posEnemy, Quaternion.identity);
                    enemyME.GetComponent<AIControl>().target = player.transform;
                }
                else if (type <= 35 && type > 15)
                {
                    clone[i] = Instantiate(enemyM, posEnemy, Quaternion.identity);
                    enemyM.GetComponent<AIControl>().target = player.transform;
                }
                else if (type <= 15 && type > 5)
                {
                    clone[i] = Instantiate(enemyMH, posEnemy, Quaternion.identity);
                    enemyMH.GetComponent<AIControl>().target = player.transform;
                }
                else
                {
                    clone[i] = Instantiate(enemyH, posEnemy, Quaternion.identity);
                    enemyH.GetComponent<AIControl>().target = player.transform;
                }
                posEnemy = hit.position;
                Debug.Log("Enemy Created");
            }

        }
    }

}
