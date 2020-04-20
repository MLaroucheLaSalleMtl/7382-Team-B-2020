using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Button : MonoBehaviour
{
    private AsyncOperation async; //This is to load a scene in the background

    public void BtnLoadScene()
    {
        if (async == null)
        {
            Scene currScene = SceneManager.GetActiveScene();
            async = SceneManager.LoadSceneAsync(currScene.buildIndex + 1);
            async.allowSceneActivation = true;
        }
    }

    public void BtnLoadScene(int i)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(i);
            async.allowSceneActivation = true;
        }
    }

    public void BtnLoadScene(string s)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(s);
            async.allowSceneActivation = true;
        }
    }
}
