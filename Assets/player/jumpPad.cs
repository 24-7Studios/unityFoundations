using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{

	public float force = 20;
    Rigidbody r;
    bool launch;


    void OnTriggerEnter(Collider other)
    {

        
        if(other.GetComponent<movement_fly>())
		{
            r = other.GetComponent<Rigidbody>(); ;
            launch = true;
        }

    }

	private void FixedUpdate()
	{


        if(launch)
		{
            r.velocity += transform.up * force;
            launch = false;
        }
        

    }




}


