using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class bot : MonoBehaviour
{
    [SerializeField]
    float searchRadius;
    [SerializeField]
    LayerMask collectableMask;
    [SerializeField]
    float MaxSearchTime;

    [SerializeField]
    Transform target,currentFloor;
    NavMeshAgent agent;
    Vector3 cubePos;
    float t;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        reseSerchTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(t > 0)
        {
            collactCubes();
            t -= Time.deltaTime;
        }
        else
        {
            buildBridge();
        }
        
    }
    void buildBridge()
    {
        agent.SetDestination(target.position);
    }
    void collactCubes()
    {   
        if(Vector3.Distance(transform.position,cubePos) <= 0.2f)
        {
            cubePos = findCubeİnRange();
            agent.SetDestination(cubePos);   
        }

        
    }
    Vector3 getRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * searchRadius;
        randomDirection += currentFloor.position;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomDirection, out hit, searchRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }else
        {
            return currentFloor.position;
        }
    }
    Vector3 findCubeİnRange()
    {
        Collider[] collectables = Physics.OverlapSphere(transform.position,searchRadius,collectableMask);
        foreach (Collider cube in collectables)
        {
            if(cube.tag == transform.tag)
            {
                if(NavMesh.SamplePosition(cube.transform.position,out NavMeshHit hit,0.15f,NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
        }
        return getRandomPoint();
        
    }

    public void reseSerchTime()
    {
        t = Random.Range(6,MaxSearchTime);
        cubePos = transform.position;
    }

    public void updateTargetFloor(Transform newFloor,Transform newTarget)
    {
        currentFloor = newFloor;
        target = newTarget;
        
    }
}
