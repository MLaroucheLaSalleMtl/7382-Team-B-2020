using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ninja : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Animator animator_Progress;
    [SerializeField] private List<GameObject> pannels;
    [SerializeField] private GameObject progress_Pannel;
    [SerializeField] private Slider progress_Slider;
    [SerializeField] private Gottem gottem;
    private float distance;
    float start = 1f;
    float end = .5f;
    [Range(1,2)] [SerializeField] private float timeChangeSpeedMult;
    private float grabCD = 0f;
    private bool grab = false;
    private float grabtime = 3f;
    public bool minigameStarted = false;
    private bool inRange = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        timeChangeSpeedMult = 1.5f;
        progress_Pannel.SetActive(false);
    }

    void Update()
    {
        if (grab)
        {
            animator_Progress.SetBool("Grab", true);
            grabCD += Time.unscaledDeltaTime;
            if (grabCD > grabtime)
                grabCD = grabtime;
        }
        else
        {
            grabCD = 0;
            if (animator_Progress.isActiveAndEnabled == true) { 
                animator_Progress.SetBool("Grab", false);
            }
        }
        if (inRange)
        {
            if (!minigameStarted)
            {
                progress_Pannel.SetActive(true);
            }
            Time.timeScale = Mathf.Lerp(Time.timeScale, end, Time.unscaledDeltaTime * timeChangeSpeedMult);
            if (animator.GetBool("Grab"))
            {
                grab = true;
                if (grabCD >= grabtime && minigameStarted == false)
                {
                    Debug.Log("IN");
                    Minigame();
                }
            }
            else
            {
                grab = false;
            }
        }
        else
        {                    
            grab = false;
            progress_Pannel.SetActive(false);
            foreach (GameObject pannel in pannels)
            {
                pannel.SetActive(false);
            }
            Time.timeScale = Mathf.Lerp(Time.timeScale, start, Time.unscaledDeltaTime * timeChangeSpeedMult);
            minigameStarted = false;
        }
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "King")
        {
            inRange = true;
        }        
        //calculate distance 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "King")
        {
            animator_Progress.SetBool("Grab", false);
            gottem.Init();
            grabCD = 0;
            inRange = false;          
        }
    }

    void Minigame()
    {
        gottem.TogglePanel();
        progress_Pannel.SetActive(false);
        minigameStarted = true;
    }
}