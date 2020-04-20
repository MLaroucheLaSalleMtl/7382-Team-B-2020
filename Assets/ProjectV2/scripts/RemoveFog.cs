using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFog : MonoBehaviour
{
    bool doWeHaveFogInScene;
    [SerializeField] private MazeGame game;
    // disable fog on this camera
    private void Start()
    {
        doWeHaveFogInScene = RenderSettings.fog;
    }
    private void OnPreRender()
    {
        if (game.disableFog == true)
        {
            RenderSettings.fog = false;
        }
    }
    private void OnPostRender()
    {
        RenderSettings.fog = doWeHaveFogInScene;
    }
    // source https://gamedev.stackexchange.com/questions/138311/how-do-i-disable-fog-on-specific-camera-s
}
