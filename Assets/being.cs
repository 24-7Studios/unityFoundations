using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class being : MonoBehaviour
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

	public virtual void takeDamagefromHit(float d, float percentSheild)
	{
		hit();
		float shieldDamage = d * percentSheild;
		float healthDamage = d - (d * percentSheild);

		if(shields !> 0)
		{
			shields -= shieldDamage;

			if(shields == 0)
			{
				health -= healthDamage;
			}
		}
		else
		{
			health -= healthDamage;
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
