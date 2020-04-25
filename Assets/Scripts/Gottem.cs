using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Gottem : MonoBehaviour //inherit from stats
{
    public AudioSource source;
    [SerializeField] private List<AudioClip> clips;
    [SerializeField] private Slider slider_Select;
    [SerializeField] private Slider slider_Object;
    public float respawnCD = 0f;
    private bool respawnOnce = false;
    private float guess;
    public int win_count;
    [Range(1f, 2f)][SerializeField] private float spawnInterval = 1.25f;
    public int tries = 3;
    [Range(0.02f,0.08f)][SerializeField] private float rangeValue = .05f;
    private float itemchanceMult;
    private Animator anim;
    [SerializeField] private Ninja ninja;
    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        anim = slider_Object.GetComponent<Animator>();
        //slider_Select.gameObject.SetActive(false);
        //slider_Object.gameObject.SetActive(false);
        inventory = Inventory.instance;
        slider_Object.value = Random.Range(0f, 1f);
        slider_Select.enabled = false;
        slider_Object.enabled = false;
    }

    void Update()
    {
        respawnCD -= Time.unscaledDeltaTime;
        if (respawnCD < 0)
            respawnCD = 0;
        if (respawnCD <= 0 && respawnOnce)
        {
            slider_Object.value = Random.Range(0f, 1f);
            anim.SetBool("visible", false);
            respawnCD += spawnInterval;
            respawnOnce = false;
        }
    }
    public void OnGuess(InputAction.CallbackContext context)
    {
        if (context.started && ninja.minigameStarted)
        {
            guess = slider_Select.value;
            if (guess > (slider_Object.value - rangeValue) && guess < (slider_Object.value + rangeValue))
                Win();
            else
                Lose();
        }
    }

    void Win()
    {
        source.PlayOneShot(clips[0]);
        anim.SetBool("visible", true);
        win_count++;
        respawnCD += spawnInterval;
        respawnOnce = true;
        if (win_count == 3)
        {
            TogglePanel();
            GetRandomItem(1);
            Init();
        }
        Debug.Log("win");
    }
    void Lose()
    {
        source.PlayOneShot(clips[1]);
        tries--;
        if (tries == 0)
        {
            Inventory.instance.CollectItem(new Coin_Item());
            RemoveRandomItem();
            TogglePanel();
            Init();
        }
        Debug.Log("lose");
    }
    public void TogglePanel()
    {
        slider_Select.gameObject.SetActive(!slider_Select.gameObject.activeInHierarchy);
        slider_Object.gameObject.SetActive(!slider_Object.gameObject.activeInHierarchy);
        if (ninja.difficulty == Ninja.DifficultyNPC.easy)
        {
            rangeValue = 0.085f;
            itemchanceMult = 1f;
        }
        else if (ninja.difficulty == Ninja.DifficultyNPC.medium)
        {
            rangeValue = 0.082f;
            itemchanceMult = 1.1f;
        }
        if (ninja.difficulty == Ninja.DifficultyNPC.hard)
        {
            rangeValue = 0.08f;
            itemchanceMult = 1.35f;
        }
    }
    public void Init()
    {
        if (tries != 3)
        {
            tries = 3;
        }
        if (win_count != 0)
        {
            win_count = 0;
        }
        Time.timeScale = 1;
        ninja.End = 0.535f;
        
    }
    public void GetRandomItem(int mult)
    {
        int choice = Random.Range(0, 100);
        if (choice <= (80 * itemchanceMult))
        {
            inventory.coins.Quantity++;
            Debug.Log("You got a coin");
        }
        if (choice <= (50 * itemchanceMult))
        {
            inventory.food.Quantity++;
            Debug.Log("You got some food");
        }
        if (choice <= (10 * itemchanceMult))
        {
            inventory.potion.Quantity++;
            Debug.Log("You got a potion");
        }
        if (choice <= (5 * itemchanceMult))
        {
            inventory.jewel.Quantity++;
            Debug.Log("You got a rare jewel !");
        }
        if (choice <= (4 * itemchanceMult))
        {
            inventory.key.Quantity++;
            Debug.Log("You got a super rare key! what could be its use??");
        }

    }
    public void RemoveRandomItem()
    {
        int choice = Random.Range(0, 101);

        if (choice <= 60)
        {
            inventory.coins.Quantity--;
            Debug.Log("You lost a coin");
        }
        if (choice <= 40)
        {
            inventory.food.Quantity--;
            Debug.Log("You lost some food");
        }
        if (choice <= 10)
        {
            inventory.potion.Quantity--;
            Debug.Log("You lost a potion");
        }

    }
}

