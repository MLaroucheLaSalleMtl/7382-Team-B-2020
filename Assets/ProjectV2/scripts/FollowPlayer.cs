using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    bool doWeHaveFogInScene;
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, player.eulerAngles.y, transform.rotation.z);
        //transform.rotation = Quaternion.Euler(transform.rotation.x, player.rotation.y, transform.rotation.z);
    }
    // disable fog on this camera
    private void Start()
    {
        doWeHaveFogInScene = RenderSettings.fog;
    }
    private void OnPreRender()
    {
        RenderSettings.fog = false;
    }
    private void OnPostRender()
    {
        RenderSettings.fog = doWeHaveFogInScene;
    }
    // source https://gamedev.stackexchange.com/questions/138311/how-do-i-disable-fog-on-specific-camera-s
}

