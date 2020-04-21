using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    // Start is called before the first frame update
    void Start()
    {
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

}
