using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] cubes;
    [SerializeField]
    Transform[] cubeParents;
    [SerializeField]
    Transform[] aıTargets;

    [SerializeField]
    int width,height;
    [SerializeField]
    float offset = 1;
    [SerializeField]
    Vector3 origen = Vector3.zero;

    void Start()
    {
        genCubes();
    }

    void genCubes()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {   
                int randomİndex = Random.Range(0,cubes.Length);
                
                Vector3 spawnPos = new Vector3(x * offset, 0, y * offset) + origen +transform.position;
                
                GameObject cube = Instantiate(cubes[randomİndex],spawnPos,Quaternion.identity);
                foreach (Transform cubeParent in cubeParents)
                {
                    if(cube.tag == cubeParent.tag)
                    {
                        cube.transform.SetParent((cubeParent));
                        break;
                    }
                }
            }
        }
    }
    public void activeCubesOfColor(string colorTag)
    {
        foreach (Transform cubeParent in cubeParents)
        {
            if(cubeParent.tag == colorTag)
            {
                cubeParent.gameObject.SetActive(true);
                break;
            }
        }
    }
    public Transform getRandomAıTarget()
    {
        return aıTargets[Random.Range(0,aıTargets.Length)];
    }
}


