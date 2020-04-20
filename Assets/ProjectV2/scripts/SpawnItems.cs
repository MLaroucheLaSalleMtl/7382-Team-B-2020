using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabItems;
    private Transform[] spawnPoints;
    private List<Transform> listSpawnPoints = new List<Transform>();
    private List<Transform> listSpawnPointsCopy = new List<Transform>();
    public static SpawnItems instance;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        //objectspawnPoints = GetComponentsInChildren<GameObject>();
        //objectspawnPoints[0] = null;
        listSpawnPoints.AddRange(spawnPoints);
        listSpawnPoints.RemoveAt(0);//delete parent
        listSpawnPointsCopy.AddRange(listSpawnPoints);
        Spawn(MazeGame.DifficultyLevel.final);
    }
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
    // Update is called once per frame
    void Update()
    {

    }
    public void Spawn(MazeGame.DifficultyLevel level)
    {
        switch (level)
        {
            case MazeGame.DifficultyLevel.easy:
                Debug.Log("test");
                break;
            case MazeGame.DifficultyLevel.medium:
                Debug.Log("test");
                break;
            case MazeGame.DifficultyLevel.hard:
                Debug.Log("test");
                break;
            case MazeGame.DifficultyLevel.final:
                foreach (GameObject element in prefabItems) // instantiate items
                {
                    int i = Random.Range(0, listSpawnPoints.Count);
                    Instantiate(element, listSpawnPoints[i]);
                    listSpawnPoints.RemoveAt(i);
                }
                break;
        }
    }
    public void DeSpawn()
    {
        // i want the items to respawn in random spots
        listSpawnPoints = new List<Transform>();
        listSpawnPoints.AddRange(listSpawnPointsCopy); // reset list
        foreach (Transform element in listSpawnPoints) // remove all items already instantiated
        {
            for (int i = 0; i < element.childCount; i++)
            {
                Transform child = element.GetChild(i);
                Destroy(child.gameObject);
            }
        }
    }

}
