using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerMovement : MonoBehaviour
{
    [SerializeField]    
    float speed;
    [SerializeField]
    float turnSmoothTime = 0.1f;
    float smoothTurn;
    // Update is called once per frame
    void Update()
    {
        move();
    }
    void move()
    {
        
        Vector3 Inputs = getInputs();
        if(Inputs.sqrMagnitude >= 0.01f)
        {
            Vector3 newPos = transform.position+Inputs*Time.deltaTime*speed;
            NavMeshHit hit;
            
            // rotate player
            float targetAngle = Mathf.Atan2(Inputs.x,Inputs.z)*Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref smoothTurn,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0,angle,0);

            if(NavMesh.SamplePosition(newPos,out hit,0.3f,NavMesh.AllAreas))
            {
                if((transform.position - hit.position).magnitude >=0.02f)
                {   
                    transform.position = hit.position;
                }
            }
         
        }
    }
    Vector3 getInputs()
    {
        Vector3 Inputs = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        
        return Inputs.normalized;
    }
}
