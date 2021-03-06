﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    private bool onlyOnce;
    [SerializeField] private Animator[] anim;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private GameObject pannelDeath;
    public static OpenDoor instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (inventory.key.Quantity == 4 && onlyOnce)
        {
            foreach (Animator element in anim)
            {
                element.SetBool("Open", true);
            }
            onlyOnce = false;
        }
    }
    public void Opendoor()
    {
        playerInventory.money += 10000;
        pannelDeath.SetActive(true);
    }

}
