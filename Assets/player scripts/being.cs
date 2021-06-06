using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class being : NetworkBehaviour
{


	public string thisName;

		
	public float health = 100;
	public float shields = 100;
	public bool instantDeath = false;
		


	public virtual void Update()
	{
		
		if(health == 0)
		{
			die();
		}



		if(shields < 0)
		{
			shields = 0;
		}

		if (health < 0)
		{
			health = 0;
		}

	}

	public virtual void takeDamagefromHit(float d, float s)
	{
		hit();

		

		if(d > shields)
		{
			float leftOver = d - shields;
			shields -= d;
			health -= leftOver * s;
		}
		else
		{
			shields -= d;
		}

	}


	public virtual void hit()
	{
		if(instantDeath)
		{
			die();
		}





	}

	
	public virtual void die()
	{
		Destroy(this.gameObject);
	}



}
