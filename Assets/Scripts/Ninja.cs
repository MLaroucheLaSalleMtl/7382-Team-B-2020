﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ninja : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Animator animator_Progress;
    [SerializeField] private List<GameObject> pannels;
    [SerializeField] private GameObject progress_Pannel;
    [SerializeField] private Slider progress_Slider;
    [SerializeField] private Gottem gottem;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject panelintro;
    public DifficultyNPC difficulty; // the difficulty of the minigame
    private string preText = "Health: ";
    private float health = 100;
    float start = 1f;
    private float end = 0.535f; // minimum TimeScale when in range of an npc
    [Range(1, 2)] [SerializeField] private float timeChangeSpeedMult; // the speed at wich the timescale changes
    private float grabCD = 0f; // timer for grabbing
    private bool grab = false;
    private float grabtime = 3f; // time need to start minigame
    public bool minigameStarted = false; // has the minigame started?
    private bool inRange = false; //are we close enough to grab?
    private float damageInterval = .35f; // delay beetween every trap damage
    [SerializeField] private ParticleSystem particle; // blood
    [SerializeField] private AudioSource audio1; 
    [SerializeField] private AudioClip[] clip;
    private float nextDamage = 0f; // the counter for damage ticking

    public float End { get => end; set => end = value; }
    public bool InRange { get => inRange; set => inRange = value; }

    void Start()
    {
        animator = GetComponent<Animator>();
        timeChangeSpeedMult = 1.5f;
        progress_Pannel.SetActive(false);
        GameManager.instance.PanelToggle(7);
    }

    void Update()
    {
        Debug.Log(Time.timeScale);
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
            if (animator_Progress.isActiveAndEnabled == true)
            {
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
            StartCoroutine("ChangeTimescale");
            grab = false;
            progress_Pannel.SetActive(false);
            foreach (GameObject pannel in pannels)
            {
                pannel.SetActive(false);
            }
            minigameStarted = false;
        }
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy Easy" || other.gameObject.tag == "King")
        {
            difficulty = DifficultyNPC.easy;
        }
        if (other.gameObject.tag == "Enemy Medium" || other.gameObject.tag == "King")
        {
            difficulty = DifficultyNPC.medium;
        }
        if (other.gameObject.tag == "Enemy Medium-Easy" || other.gameObject.tag == "King")
        {
            difficulty = DifficultyNPC.medium;
        }
        if (other.gameObject.tag == "Enemy Medium-Hard" || other.gameObject.tag == "King")
        {
            difficulty = DifficultyNPC.hard;
        }
        if (other.gameObject.tag == "Enemy Hard" || other.gameObject.tag == "King")
        {
            difficulty = DifficultyNPC.hard;
        }
        if (other.gameObject.tag == "TrapSpikes" || other.gameObject.tag == "TrapSaw")
        {
            Vector3 location = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            nextDamage += Time.fixedDeltaTime;
            if (nextDamage >= damageInterval)
            {
                DealDamage(location, 15f);
                audio1.PlayOneShot(clip[0], 0.9f);
                audio1.PlayOneShot(clip[1], 0.5f);
                nextDamage = 0;
            }
        }

        //calculate distance 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy Easy" || other.gameObject.tag == "Enemy Medium" || other.gameObject.tag == "Enemy Medium-Easy" || other.gameObject.tag == "Enemy Hard" || other.gameObject.tag == "Enemy Easy" || other.gameObject.tag == "King")
        {
            animator_Progress.SetBool("Grab", false);
            gottem.Init();
            grabCD = 0;
        }
        if (other.gameObject.tag == "Trap")
        {
            nextDamage = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TrapAxe")
        {
            Vector3 location = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            DealDamage(location, 30f);
            audio1.PlayOneShot(clip[0], 0.9f);
            audio1.PlayOneShot(clip[2], 0.5f);
        }
    }
    void Minigame()
    {
        gottem.TogglePanel();
        progress_Pannel.SetActive(false);
        minigameStarted = true;
        end = 0f;
    }
    void DealDamage(Vector3 location, float damage)
    {
        ParticleSystem.EmitParams emit = new ParticleSystem.EmitParams();
        emit.position = location;
        particle.Emit(emit, 15);
        health -= damage;
        text.faceColor = Color.red;
        text.text = preText + health.ToString("0");
        if (health <= 0)
        {
            health = 0;
            Death();
        }
    }
    private IEnumerator ChangeTimescale()
    {
        while (Time.timeScale < 0.99f)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, start, Time.unscaledDeltaTime * timeChangeSpeedMult);
            yield return new WaitForFixedUpdate();
        }
        Time.timeScale = 1;
        yield return true;
    }
    void Death()
    {
        MazeGame.instance.StartMazeGame();
        health = 100f;
        GetComponent<Inventory>().key.Quantity = 0;
        text.faceColor = Color.green;
        text.text = preText + health.ToString("0");
        SpawnItems.instance.DeSpawn();
        SpawnItems.instance.Spawn(MazeGame.instance.Level);
    }
    public enum DifficultyNPC
    {
        easy, medium, hard
    }
}

