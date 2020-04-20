using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private int keys = 0;
    private bool onlyOnce;
    [SerializeField] private Animator[] anim;
    public int Keys { get => keys; set => keys = value; }
    public static OpenDoor instance;
    // Start is called before the first frame update
    void Start()
    {

    }
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
        if (Keys == 4 && onlyOnce)
        {
            foreach (Animator element in anim)
            {
                element.SetBool("Open", true);
            }
            onlyOnce = false;
        }
    }


}
