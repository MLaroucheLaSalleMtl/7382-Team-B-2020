using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPC
{
    public string name;

    [TextArea(1, 5)]
    public string[] sentences;

    //[SerializeField] public string npcName;

    //[TextArea(1, 5)]
    //[SerializeField] public string texts;
    //private Dialog_Manager dialog;

    //void Start()
    //{
    //    dialog = Dialog_Manager.instance;
    //}

    //public void DialogTrigger()
    //{
    //    dialog.StartTalking(npcName, texts);
    //}
}

//public class DiologueManager : MonoBehaviour
//{
//    //there can only be one!
//    public static DiologueManager instance = null;
//    private List<string> lines = new List<string>();
//    public GameObject pannelOptions;
//    public GameObject pannelDiaologue;
//    public Text currentlines;
//    int i;
//    private string options;

//    // Update is called once per frame
//    void Awake()
//    {
//        pannelDiaologue.SetActive(false);
//        if (instance == null)
//        {
//            instance = this;
//        }
//        else if (instance != this)
//        {
//            Destroy(gameObject);
//        }
//    }
//    void nextline()
//    {
//        if (i == (lines.Count))
//        {
//            Debug.Log("options time babay");
//            CloseDiologue();
//        }
//        else
//        {
//            Debug.Log("not yet");
//            currentlines.text = lines[i];
//            Debug.Log(lines.Count + " " + i);
//        }
//    }
//    public void StartDiologue(string[] sentences)
//    {
//        i = 0;
//        Cursor.visible = true;
//        Cursor.lockState = CursorLockMode.None;
//        if (pannelDiaologue)
//        {
//            pannelDiaologue.SetActive(true);
//        }
//        lines = new List<string>();
//        foreach (string sentence in sentences)
//        {
//            lines.Add(sentence);
//        }
//        currentlines.text = lines[0];
//    }
//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.N))
//        {
//            triggerNextLine();
//        }
//        if (Input.GetKeyDown(KeyCode.B))
//        {
//            CloseDiologue();
//        }
//    }

//    private void CloseDiologue()
//    {
//        Cursor.visible = false;
//        Cursor.lockState = CursorLockMode.Locked;
//        pannelDiaologue.SetActive(false);
//    }

//    public void triggerNextLine()
//    {
//        nextline();
//        i++;
//    }
//}
//public class StartDiaologue : MonoBehaviour
//{
//    public string npcName;
//    [TextArea(3, 10)]
//    public string[] Phrases;
//    [TextArea(3, 10)]
//    public string Options;

//}
//DiologueManager.instance.StartDiologue(hittedTarget.gameObject.GetComponent<StartDiaologue>().Phrases);
