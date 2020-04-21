using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    private GameManager code;
    private Inventory inventory;
    [SerializeField] private Text interactTxt;
    private bool canOpenShop = false;
    //private bool canSteal = false;
    private bool canCollect = false;
    private Dialog_Manager dialog;
    private string _name;
    private string text;
    private bool canTalk = false;
    [SerializeField] private GameObject mazegame;
    [SerializeField] private Text titleTxt;
    //[SerializeField] private Button btnExitShop;
    [SerializeField] private Button btnBack;
    //[SerializeField] private Button btnSell;
    //[SerializeField] private Text quantityTxt;
    //[SerializeField] private Text moneyGained;
    //[SerializeField] private Button btnAdd;
    //[SerializeField] private Button btnRemove;
    [SerializeField] private GameObject shop;
    private bool CanGoToDungeon = false;


    // Start is called before the first frame update
    void Start()
    {
        interactTxt.gameObject.SetActive(false);
        code = GameManager.instance;
        inventory = Inventory.instance;
        dialog = Dialog_Manager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.collider.tag == "Salesperson")
            {
                interactTxt.text = "Press E to open Shop";
                interactTxt.gameObject.SetActive(true);
                canOpenShop = true;
                //canSteal = false;
                canCollect = false;
                canTalk = true;
                //name = hit.collider.GetComponent<DialogInformations>().npcName;
                //text = hit.collider.GetComponent<DialogInformations>().texts;
            }
            //else if (hit.collider.tag == "Enemy")
            //{
            //    interactTxt.text = "Press E to Steal";
            //    interactTxt.gameObject.SetActive(true);
            //    canSteal = true;
            //    canCollect = false;
            //    canOpenShop = false;
            //    canTalk = false;
            //}
            else if (hit.collider.tag == "Coin" || hit.collider.tag == "Food" || hit.collider.tag == "Jewel" || hit.collider.tag == "Potion" || hit.collider.tag == "Key")
            {
                interactTxt.text = "Press E to collect Item";
                interactTxt.gameObject.SetActive(true);
                canCollect = true;
                //canSteal = false;
                canOpenShop = false;
                canTalk = false;
            }
            else if (hit.collider.tag == "FirstDoor")
            {
                interactTxt.text = "You need a key! maybe one of the villagers has it?";
                interactTxt.gameObject.SetActive(true);
                if (inventory.key.Quantity == 3)
                {
                    interactTxt.text = "You have the key press e to enter dungeon";
                    interactTxt.gameObject.SetActive(true);
                    mazegame.SetActive(true);
                    MazeGame.instance.StartMazeGame();
                    canCollect = false;
                    //canSteal = false;
                    canOpenShop = false;
                    canTalk = false;
                    CanGoToDungeon = true;
                }
                //add dialog here
            }
            else if (hit.collider.tag == "FinalDoor")
            {
                hit.collider.GetComponent<OpenDoor>().Opendoor();
            }
        }
        else
        {
            interactTxt.gameObject.SetActive(false);
            canOpenShop = false;
            //canSteal = false;
            canCollect = false;
            canTalk = false;
        }
    }

    public void Interact()
    {
        if (canOpenShop == true)
        {
            OpenShop();
        }
        //else if (canSteal == true)
        //{
        //    Steal();
        //}
        else if (canCollect == true)
        {
            Collect();
            canCollect = false;
        }
        else if (CanGoToDungeon == true)
        {
            MazeGame.instance.StartMazeGame();
            inventory.key.Quantity = 0;
        }
    }

    public void OpenShop()
    {
        code.PanelToggle(2);
        titleTxt.text = "Shop";
        //btnExitShop.gameObject.SetActive(true);
        btnBack.gameObject.SetActive(false);
        //btnSell.gameObject.SetActive(true);
        shop.SetActive(true);
    }
    public void CloseShop()
    {
        //btnExitShop.gameObject.SetActive(false);
        btnBack.gameObject.SetActive(true);
        //btnSell.gameObject.SetActive(false);
        shop.SetActive(false);
        titleTxt.text = "Inventory";
        code.PanelToggle(0);
    }

    //public void Steal()
    //{
    //    Debug.Log("Steal");
    //}

    public void Collect()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f))
        {
            if (hit.collider.tag == "Coin" || hit.collider.tag == "Food" || hit.collider.tag == "Jewel" || hit.collider.tag == "Potion" || hit.collider.tag == "Key")
            {
                switch (hit.collider.tag)
                {
                    case "Coin":
                        inventory.coins.Quantity++;
                        break;
                    case "Food":
                        inventory.food.Quantity++;
                        break;
                    case "Jewel":
                        inventory.jewel.Quantity++;
                        break;
                    case "Potion":
                        inventory.potion.Quantity++;
                        break;
                    case "Key":
                        inventory.key.Quantity++;
                        break;
                }
                Destroy(hit.collider.gameObject);
            }
        }
        Debug.Log("Collect");
    }

    public void Talk()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f))
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Talking>().TriggerNPC();
            }
            //FindObjectOfType<Talking>().TriggerNPC();
        }
    }
}