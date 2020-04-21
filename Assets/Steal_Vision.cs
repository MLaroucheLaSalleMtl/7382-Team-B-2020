using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steal_Vision : MonoBehaviour
{
    private Animator ninja;
    [SerializeField] private GameObject postVol;
    // Start is called before the first frame update
    void Start()
    {
        ninja = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (ninja.GetBool("Grab") == true)
        { 
            postVol.SetActive(true);
        }
        else
        {
            postVol.SetActive(false);
        }
    }
}
