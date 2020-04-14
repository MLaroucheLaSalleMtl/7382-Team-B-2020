using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter1))]
public class AIControl : MonoBehaviour
{
    private NavMeshAgent agent;
    private ThirdPersonCharacter1 character;
    public Transform target;

    [SerializeField] private float fieldOfView = 90f;
    [SerializeField] private float visionDistance = 100f;
    [SerializeField] private float closeRange = 5f;
    private float angleToPlayer;
    private Vector3 direction; //Vector from AI to player;
    private bool canSeePlayer;
    private float adjustedView;
    public float distancePlayer;
    [SerializeField] private Vector3 destination;

    [SerializeField] public float range = 10f;

    //private Vector3 validLocation;

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter1>();
        agent.updateRotation = false;
        agent.updatePosition = true;
        RandomizePath();
    }


    private void Update()
    {

        direction = target.position - transform.position;
        angleToPlayer = Vector3.Angle(direction, transform.forward);
        distancePlayer = direction.magnitude;

        if (distancePlayer <= closeRange)
        {
            adjustedView = 2 * fieldOfView;
        }
        else
        {
            adjustedView = fieldOfView;
        }

        if (angleToPlayer <= adjustedView * 0.5f && distancePlayer <= visionDistance)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
        }


        //if (canSeePlayer == true)
        //{
        //    agent.speed = 8f;
        //    agent.SetDestination(target.position);
        //    agent.transform.LookAt(target);
        //}
        //else
        //{
            //Debug.Log("Player Hidden");
            if (agent.hasPath == false)
            {
                RandomizePath();
            }
        //}

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false, false);
        }
    }

    private void RandomizePath()
    {

        //agent.speed = 0.5f;
        destination = new Vector3(transform.position.x + Random.Range(-15f, 15f),transform.position.y, transform.position.z + Random.Range(-15f, 15f));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(destination, out hit, range, NavMesh.AllAreas))
        {
            if (hit.position.x >= -50 && hit.position.x <= 50 && hit.position.z >= -50 && hit.position.z <= 50)
            {
                agent.SetDestination(hit.position);
            }
        }
        //Debug.Log(agent.destination);
    }

    public void SetTarget(Vector3 target)
    {
        //agent.speed = 8f;
        agent.SetDestination(target);
    }
}
