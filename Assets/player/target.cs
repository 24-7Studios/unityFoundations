using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : being
{

	public bool isDead;


	public override void die()
	{

		isDead = true;
		gameObject.SetActive(false);

	}





}
