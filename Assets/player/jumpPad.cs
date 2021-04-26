using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{

	public float force = 20;
    Rigidbody player;
    bool launch;


    void OnTriggerEnter(Collider other)
    {

        
        if(other.GetComponent<movement_fly>())
		{
            player = other.GetComponent<movement_fly>().body;
            launch = true;
        }

    }

	private void FixedUpdate()
	{


        if(launch)
		{
            player.velocity += transform.up * force;
            launch = false;
        }
        

    }




}


