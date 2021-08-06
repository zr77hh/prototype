using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bridgeBulder : MonoBehaviour
{
    [SerializeField]
    door door;
    [SerializeField]
    Transform AıTarget;

    int cubesCount=0;

    private void OnTriggerEnter(Collider other) 
    {
       build(other.gameObject);
    }
    private void OnTriggerStay(Collider other) 
    {
       build(other.gameObject);
    }
    void build(GameObject player)
    {
        if(cubesCount < 33)
        {
            cubeCollector collector = player.GetComponent<cubeCollector>();
            if(collector != null)
            {
                GameObject cubeToBuildWith = collector.takeCube();
                if(cubeToBuildWith != null)
                {
                    cubeToBuildWith.transform.SetParent(transform.parent);
                    cubeToBuildWith.transform.position = transform.position;
                    cubeToBuildWith.transform.rotation = transform.rotation;
                    transform.position = new Vector3(transform.position.x,transform.position.y+0.3f,transform.position.z+0.3f);
                    AıTarget.position = transform.position;
                    cubesCount+=1;
                }
                else
                {
                    bot AI = player.GetComponent<bot>();
                    if(AI != null)
                    {
                        AI.reseSerchTime();
                    }
                }
            }    
        }
        else
        {
            door.openDoor();
            Destroy(gameObject);
        }
        
       
    }
}

