using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer circle;
    int numberofpoints = 30;
    float radius = .2f;
    float angle = 0f;
    float angleIncrease = 0f;
    [SerializeField][Range(0,1)] float initHeight = .5f;
    private float distance;
    private GameObject player;

    // Start is called before the first frame update
    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if ( distance <= 5f)
        {
            circle.enabled = true;
        }
        else
        {
            circle.enabled = false;
        }
    }
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        circle = GetComponent<LineRenderer>();
        circle.positionCount = 0;
        DrawCircle();
        circle.enabled = false;
    }
    public void DrawCircle()
    {
        circle.positionCount = numberofpoints;
        angleIncrease = (2f * Mathf.PI) / numberofpoints;
        Vector3 pos = new Vector3(0f,-1.3f,initHeight);
        for (int i = 0; i < numberofpoints; i++)
        {
            pos.x += radius * Mathf.Cos(angle);
            pos.y += radius * Mathf.Sin(angle);
            circle.SetPosition(i, pos);
            angle += angleIncrease;
        }
    }
}
