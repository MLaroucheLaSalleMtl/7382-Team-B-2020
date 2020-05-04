using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steal_Vision : MonoBehaviour
{
    [SerializeField] private Ninja ninja;
    [SerializeField] private GameObject postVol;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ( ninja.InRange == true)
        { 
            postVol.SetActive(true);
        }
        else
        {
            postVol.SetActive(false);
        }
    }
}
