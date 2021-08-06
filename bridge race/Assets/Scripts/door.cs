using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class door : MonoBehaviour
{
    [SerializeField]
    cubeSpawner spawner;


    private void OnTriggerEnter(Collider other) 
    {
    
        bot AI = other.GetComponent<bot>();
        if(AI != null)
        {
            AI.updateTargetFloor(spawner.transform,spawner.getRandomAıTarget());
        }    
        spawner.activeCubesOfColor(other.tag);

    }

    public void openDoor()
    {
        GetComponent<Animator>().Play("openingTheDoor");
        Destroy(GetComponent<NavMeshObstacle>());
    }
}
