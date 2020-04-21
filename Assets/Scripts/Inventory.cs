using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public static Inventory instance = null; //Declaration of singleton
    [SerializeField] private int size = 10000000; //Size of inventory
    private int totalWeight; //Total weight of Items present in the inventory
    [SerializeField] private GameObject[] itemSlots;
    public Item coins;
    public Item food;
    public Item jewel;
    public Item potion;
    public Item key;
    [SerializeField] private Text coinsQtyTxt;
    [SerializeField] private Text foodQtyTxt;
    [SerializeField] private Text jewelQtyTxt;
    [SerializeField] private Text potionQtyTxt;
    [SerializeField] private Text keyQtyTxt;

    public int money;
    private Item itemToSell;
    private int quantityToSell;
    [SerializeField] private Text quantityTxt;
    [SerializeField] private Text moneyGained;
    public Text moneyTxt;

    // Start is called before the first frame update
    void Start()
    {
        quantityToSell = 0;
        coins = new Item("Coin", 0, 1, 0, 1, 0);
        food = new Item("Food", 0, 2, 1, 1, 1);
        jewel = new Item("Jewel", 0, 5, 3, 10, 2);
        potion = new Item("Potion", 0, 3, 1, 5, 3);
        key = new Item("key", 0, 3, 1, 5, 3);

        itemToSell = coins;

        //for (int i = 0; i < itemSlots.Length; i++)
        //{
        //    itemSlots[i].SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        coinsQtyTxt.text = coins.Quantity.ToString();
        foodQtyTxt.text = food.Quantity.ToString();
        jewelQtyTxt.text = jewel.Quantity.ToString();
        potionQtyTxt.text = potion.Quantity.ToString();
        keyQtyTxt.text = key.Quantity.ToString();
    }

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

    public void CollectItem(Item item)
    {
        if ((size - totalWeight) > item.Weight)
        {
            itemSlots[item.Slot].SetActive(true);
            item.Quantity++;
            totalWeight += item.Weight;
            switch (item.Name)
            {
                case "Coin":
                    coinsQtyTxt.text = item.Quantity.ToString();
                    break;
                case "Food":
                    foodQtyTxt.text = item.Quantity.ToString();
                    break;
                case "Jewel":
                    jewelQtyTxt.text = item.Quantity.ToString();
                    break;
                case "Potion":
                    potionQtyTxt.text = item.Quantity.ToString();
                    break;
                case "Key":
                    potionQtyTxt.text = item.Quantity.ToString();
                    break;
            }
        }
    }

    public void DiscardItem(Item item)
    {
        if (item.Quantity > 0)
        {
            item.Quantity--;
        }
        if (item.Quantity <= 0)
        {
            itemSlots[item.Slot].SetActive(false);
        }
        totalWeight -= item.Weight;
    }

    public void SellItem()
    {
        if (quantityToSell > itemToSell.Quantity)
        {
            quantityToSell = 0;
        }
        else
        {
            itemToSell.Quantity -= quantityToSell;
            money += itemToSell.Value * quantityToSell;
            moneyGained.text = "0$";
            moneyTxt.text = money.ToString() + "$";
        }
    }

    public void AddQuantity()
    {
        if (quantityToSell < itemToSell.Quantity)
        {
            quantityToSell++;
            quantityTxt.text = quantityToSell.ToString();
            moneyGained.text = (quantityToSell * itemToSell.Value).ToString() + "$";
        }
    }
    public void RemoveQuantity()
    {
        if (quantityToSell > 0)
        {
            quantityToSell--;
            quantityTxt.text = quantityToSell.ToString();
            moneyGained.text = (quantityToSell * itemToSell.Value).ToString() + "$";
        }
    }

    public void SelectCoins()
    {
        itemToSell = coins;
        quantityToSell = 0;
        quantityTxt.text = quantityToSell.ToString();
        moneyGained.text = (quantityToSell * itemToSell.Value).ToString() + "$";
    }
    public void SelectFood()
    {
        itemToSell = food;
        quantityToSell = 0;
        quantityTxt.text = quantityToSell.ToString();
        moneyGained.text = (quantityToSell * itemToSell.Value).ToString() + "$";
    }
    public void SelectJewels()
    {
        itemToSell = jewel;
        quantityToSell = 0;
        quantityTxt.text = quantityToSell.ToString();
        moneyGained.text = (quantityToSell * itemToSell.Value).ToString() + "$";
    }
    public void SelectPotion()
    {
        itemToSell = potion;
        quantityToSell = 0;
        quantityTxt.text = quantityToSell.ToString();
        moneyGained.text = (quantityToSell * itemToSell.Value).ToString() + "$";
    }
}