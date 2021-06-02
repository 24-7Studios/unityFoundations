using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    public float respawnTimer;

    GameObject theThing;
    target Target;
    float health;
    float shields;
    float timer;


    // Start is called before the first frame update
    void Start()
    {

        Target = GetComponentInChildren<target>();
        health = Target.health;
        shields = Target.shields;
        theThing = Target.gameObject;
        

    }

    // Update is called once per frame
    void Update()
    {
        

        if(Target.isDead)
		{

            timer -= Time.deltaTime;
            if (timer <= 0)
			{
                spawn();

			}

        }
        else
		{
            timer = respawnTimer;
		}

        



    }



    void spawn()
	{

        Target.isDead = false;
        Target.health = health;
        Target.shields = shields;
        theThing.SetActive(true);


	}



}
