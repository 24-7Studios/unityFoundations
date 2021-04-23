using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public double RespawnTime = 5;
    private double timer = 0;
    public float strength = 2;

    public Vector3 offSet;

    public Rigidbody offSpring;
    






    // Start is called before the first frame update
    void Start()
    {


        
        


    }

    // Update is called once per frame
    void Update()
    {


        Spawn();
        timer -= Time.deltaTime;

    }

    void Spawn()
	{



        if(timer <= 0)
		{
            Rigidbody clone;
            
            clone = Instantiate(offSpring, transform.position, transform.rotation, transform) ;

            clone.velocity = (Vector3.forward * strength);

            timer = RespawnTime;
		}
        

	}
}
