using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;

public class Maze_Game : MonoBehaviour
{
    [SerializeField] private GameObject light1;
    [SerializeField] private Camera cam;
    public PostProcessVolume layer;
    private Vignette vig = null;
    public Texture2D text;
    public Transform trans;
    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        layer.profile.TryGetSettings(out vig);
        SwitchCursor();

    }
    void FixedUpdate()
    {
        Ray ray;
        ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 point = ray.GetPoint(ray.origin.y - light1.transform.position.y);
        point.y = light1.transform.position.y;  
        Vector3 vigPoint = cam.ScreenToViewportPoint(Input.mousePosition);
        vig.center.value = new Vector2(vigPoint.x, vigPoint.y);
        Vector3 mousepos = cam.ScreenToViewportPoint(Input.mousePosition);
        Debug.Log(point);
        light1.transform.position = point;
        anim.SetFloat("PosY", mousepos.y);
    }
    void SwitchCursor()
    {
        Cursor.SetCursor(text, Vector2.zero, CursorMode.ForceSoftware);
    }
}
