﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Button : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            TurnOn();
        }
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
