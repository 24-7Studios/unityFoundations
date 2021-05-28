using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : weaponClass
{

    public string anim1;
    public string anim2;



	public override void Update()
	{
		base.Update();


		if(InputFire1Down)
		{
			anim.Play(anim1);
		}

		if(InputFire2Down)
		{
			anim.Play(anim2);
		}



	}



}
