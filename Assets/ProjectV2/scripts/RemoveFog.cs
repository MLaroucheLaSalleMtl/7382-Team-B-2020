using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFog : MonoBehaviour
{
    // disable fog on this camera
    private void OnPreRender()
    {
        if (MazeGame.instance.isActiveAndEnabled)
        {
            RenderSettings.fog = MazeGame.instance.Fog;
        }
    }
    private void OnPostRender()
    {
        if (MazeGame.instance.isActiveAndEnabled)
        {
            RenderSettings.fog = MazeGame.instance.Fog;
        }
    }
    // source https://gamedev.stackexchange.com/questions/138311/how-do-i-disable-fog-on-specific-camera-s
}
