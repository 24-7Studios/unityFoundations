using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{

	public float force = 20;

    void OnTriggerEnter(Collider other)
    {

        
        if(other.GetComponent<movement_fly>())
		{
            other.GetComponent<movement_fly>().body.velocity += transform.up * force;
        }

    }



}


