using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;

public class MazeGame : MonoBehaviour
{
    [SerializeField] private GameObject light1;
    [SerializeField] private Transform spawn;
    [SerializeField] private Transform originalPos;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    public PostProcessVolume layer;
    private Vignette vig = null;
    public Texture2D text;
    public Transform trans;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject minimap;
    private PostProcessVolume volumeTopCam;
    private CinemachineVirtualCamera currentCam;
    private CinemachineFreeLook playerCam;
    private bool doWeHaveFogInScene;
    private DifficultyLevel level;
    private SpawnItems ItemSpawner;
    public static MazeGame instance;
    private bool fog = false;
    public bool disableFog { get; private set; }
    public DifficultyLevel Level { get => level; set => level = value; }
    public bool Fog { get => fog; set => fog = value; }


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        layer.profile.TryGetSettings(out vig);
        currentCam = GameObject.FindGameObjectWithTag("MiniMapCam").GetComponentInChildren<CinemachineVirtualCamera>();
        playerCam = GameObject.FindGameObjectWithTag("Freelook Cam").GetComponent<CinemachineFreeLook>();
        volumeTopCam = GameObject.FindGameObjectWithTag("TopViewVig").GetComponent<PostProcessVolume>();
        level = DifficultyLevel.final;

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
        light1.transform.position = point;
        anim.SetFloat("PosY", mousepos.y);
        if (Input.GetKeyDown(KeyCode.L))
        {
            SwitchCam();
            Debug.Log("test");
        }
    }
    void SwitchCursor()
    {
        Cursor.SetCursor(text, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void StartMazeGame()
    {
        player.transform.position = spawn.position;
        player.transform.rotation = spawn.rotation;
        Fog = true;
        RenderSettings.fog = Fog;
        minimap.SetActive(true);
        player.GetComponentInChildren<Light>().enabled = true;
        SpawnItems.instance.Spawn(level);
        SwitchCursor();
    }
    void SwitchCam()
    {
        if (currentCam.Priority < playerCam.Priority)
        {
            currentCam.Priority = playerCam.Priority;
            playerCam.Priority = 0;
        }
        else
        {
            playerCam.Priority = currentCam.Priority;
            currentCam.Priority = 0;
        }
        light1.SetActive(!light1.activeInHierarchy);
        volumeTopCam.enabled = !volumeTopCam.enabled;
        Fog = !Fog;
    }
    public enum DifficultyLevel
    {
        easy, medium, hard, final
    }
}

