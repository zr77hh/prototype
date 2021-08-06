using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeCollector : MonoBehaviour
{
    [SerializeField]
    GameObject cubePrefab,collectableCubePrefab;
    [SerializeField]
    Transform cubePos;
    [SerializeField]
    LayerMask bridgeCubeMask;
    [SerializeField]
    Material color;
    List<GameObject> collectedCubes = new List<GameObject>();
    private void Update() 
    {
        if(Physics.Raycast(transform.position,Vector3.down,out RaycastHit hit,1,bridgeCubeMask))
        {
            if(hit.transform.tag != transform.tag)
            {
                GameObject g = takeCube();
                if(g!= null)
                {
                    hit.transform.tag = transform.tag;
                    hit.transform.GetComponent<Renderer>().material = color;
                    Destroy(g);
                }
                
            }
        }    
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("collectable")&&other.tag == transform.tag)
        {
            collectCube(other.gameObject);
        }
        else
        {
            cubeCollector collector = other.GetComponent<cubeCollector>();
            if(collector != null)
            {
                knockDownThePlayer(collector.getCubeCount());
            }
        }
    }
    void collectCube(GameObject _cube)
    {
        GameObject cube = Instantiate(cubePrefab,cubePos.position,cubePos.rotation);
        cube.transform.SetParent(transform);
        collectedCubes.Add(cube);
        cubePos.position = new Vector3(cubePos.position.x,cubePos.position.y+0.3f,cubePos.position.z);
        Destroy(_cube);
    }
    public GameObject takeCube()
    {
        if(collectedCubes.Count > 0)
        {
            GameObject lastCube = collectedCubes[collectedCubes.Count-1];
            collectedCubes.RemoveAt(collectedCubes.Count-1);
            cubePos.position = new Vector3(cubePos.position.x,cubePos.position.y-0.3f,cubePos.position.z);
            return lastCube;
        }
        else
        {
            return null;
        }
    }
    void knockDownThePlayer(int cubeCountOfOther)
    {
        if(cubeCountOfOther > getCubeCount())
        {
            int count = collectedCubes.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject collectedCube = takeCube();
                GameObject rbCollectable = Instantiate(collectableCubePrefab,collectedCube.transform.position,collectedCube.transform.rotation);
                Rigidbody rb = rbCollectable.AddComponent<Rigidbody>();
                rb.AddForce((-transform.forward+transform.up)*60);
                Destroy(collectedCube);
            }
        }
    }
    public int getCubeCount()
    {
        return collectedCubes.Count;
    }


}
