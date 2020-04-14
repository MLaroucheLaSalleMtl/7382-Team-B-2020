using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_Manager : MonoBehaviour
{
    public Text textName;
    public Text textDialog;
    public static Dialog_Manager instance = null; //Declaration of singleton
    private GameManager code;

    private Queue<string> talking;
    private int i;
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

    void Start()
    {
        talking = new Queue<string>();
        code = GameManager.instance;
    }

    void Update()
    {
        
    }

    public void StartTalking(NPC npc)
    {
        i = 0;
        code.PanelToggle(4);

        textName.text = npc.name;

        talking.Clear();
        foreach(string sentence in npc.sentences)
        {
            talking.Enqueue(sentence);
        }

        NextSentence();
    }

    public void NextSentence()
    {
        if(talking.Count == 0)
        {
            EndTalking();
            return;
        }

        string sentence = talking.Dequeue();
        textDialog.text = sentence;
        //textDialog.text = talking.Dequeue();
    }

    public void EndTalking()
    {
        code.PanelToggle(0);
    }
}
