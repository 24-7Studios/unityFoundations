using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : being
{

	bool isDead;


	public override void die()
	{

		isDead = true;
		gameObject.SetActive(false);

	}





}
