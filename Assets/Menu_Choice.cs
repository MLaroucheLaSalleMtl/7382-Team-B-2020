using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Choice : MonoBehaviour
{
    [SerializeField] private GameObject[] panels; //list of all my panels
    [SerializeField] private Selectable[] defaultButtons; // A list of the button selected by default


    public void PanelToggle()
    {
        PanelToggle(0); //activate the main menu (position 0)
    }
    public void PanelToggle(int position)
    {
        //we call this function when we want to open/close a menu.
        Input.ResetInputAxes();
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(position == i);
            if (position == i)
            {
                defaultButtons[i].Select(); //we select the button from that coresponding panel.
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("PanelToggle", 0.01f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
