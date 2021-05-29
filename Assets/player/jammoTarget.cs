using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class jammoTarget : being
{

    public List<Rigidbody> rigidbodies;

    void Start()
    {

        rigidbodies = GetComponentsInChildren<Rigidbody>(false).ToList();


    }

   public override void die()
	{
        foreach(Rigidbody r in rigidbodies)
		{

            r.

		}

	}








}
