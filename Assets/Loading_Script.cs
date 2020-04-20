using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class Loading_Script : MonoBehaviour
{
    private AsyncOperation async; //Hold all the scene information.
    [SerializeField] private Image progressbar; //image of the progressbar
    [SerializeField] private Text txtPercent; //textfield for percentage.
    [SerializeField] private bool waitForUserInput = false; //if enabled we need to press a key before moving to next scene
    private bool ready = false;//ready must be true to go to the next scene.
    [SerializeField] private float delay = 0;//wait for X seconds before moving to the next scene.
    [SerializeField] private int sceneToLoad = -1; //the number of the scene to load.

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        Input.ResetInputAxes();
        System.GC.Collect(); //Call the garbage collector to get the trash.
        Scene currentScene = SceneManager.GetActiveScene();
        if (sceneToLoad < 0) //if scene to load is negative load next scene
        {
            async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
        }
        else //if scenetoload is possitive load that scenes number.
        {
            async = SceneManager.LoadSceneAsync(sceneToLoad);
        }
        async.allowSceneActivation = false; //Dont go to the next scene right away.
        if (waitForUserInput == false)
        {
            Invoke("Activate", delay);
        }
    }

    public void Activate()
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForUserInput && Input.anyKey)
        {
            ready = true;
        }
        if (progressbar)
        {
            progressbar.fillAmount = async.progress + 0.1f;
        }
        if (txtPercent)
        {
            txtPercent.text = ((async.progress + 0.1f) * 100).ToString("f2") + " %";
        }
        if (async.progress > 0.89f && SplashScreen.isFinished && ready)
        {
            async.allowSceneActivation = true;
        }
    }
}