using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string name;
    private int quantity;
    private int strength; //damage when thrown
    private int weight; //inventory space
    private int value; //money
    private int slot;

    public string Name { get => name;}
    public int Quantity { get => quantity; set => quantity = value; }
    public int Strength { get => strength;}
    public int Weight { get => weight;}
    public int Value { get => value;}
    public int Slot { get => slot;}

    public Item()
    {
        name = "Unidentified";
        Quantity = 0;
        strength = 0;
        weight = 0;
        value = 0;
        slot = 0;
    }

    public Item(string name, int quantity, int strength, int weight, int value, int slot)
    {
        this.name = name;
        Quantity = quantity;
        this.strength = strength;
        this.weight = weight;
        this.value = value;
        this.slot = slot;
    }

    //public enum ItemType
    //{
    //    Coin,
    //    Food,
    //    Jewlels,
    //    Potion,
    //}

    //public ItemType itemType;
    //public int amount;
}
