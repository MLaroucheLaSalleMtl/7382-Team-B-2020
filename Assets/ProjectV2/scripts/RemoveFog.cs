using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFog : MonoBehaviour
{
    [SerializeField] private MazeGame game;
    // disable fog on this camera
    private void OnPreRender()
    {
        RenderSettings.fog = MazeGame.instance.Fog;
    }
    private void OnPostRender()
    {
        RenderSettings.fog = MazeGame.instance.Fog;
    }
    // source https://gamedev.stackexchange.com/questions/138311/how-do-i-disable-fog-on-specific-camera-s
}
