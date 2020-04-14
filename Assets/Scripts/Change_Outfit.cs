using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Outfit : MonoBehaviour
{
    [SerializeField] private List<GameObject> suits;
    [SerializeField] private List<GameObject> cams;
    [SerializeField] private List<GameObject> inputs;

    private void Start()
    {
        suits[0].SetActive(true);
        suits[1].SetActive(false);
    }
    public void Change()
    {
        suits[0].SetActive(!suits[0].activeInHierarchy);
        suits[1].SetActive(!suits[1].activeInHierarchy);
        cams[0].SetActive(!cams[0].activeInHierarchy);
        cams[1].SetActive(!cams[1].activeInHierarchy);
        inputs[0].SetActive(!inputs[0].activeInHierarchy);
        inputs[1].SetActive(!inputs[1].activeInHierarchy);
        Debug.Log("test");
    }
}
