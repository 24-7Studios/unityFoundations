using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooty : weaponClass
{
	// Start is called before the first frame update





	public string fireAnim;
	public string pumpAnim;

	public Transform firePosition;
	public GameObject fireEffect;
	public float viewPunch = 20;

	public bool hasFired;
	public bool pumpBack;
	public bool pumpForward;
	private int pumps = 0;










	private float effectTimer = 0.1f;




	public override void PrimaryAction()
	{

		if (anim.GetCurrentAnimatorStateInfo(0).IsName("draw"))
		{

			anim.Play(fireAnim);
			
			body.GetComponent<lookHandler_fly>().ymov -= viewPunch;
			GameObject c = Instantiate(fireEffect, firePosition);
			Destroy(c, effectTimer);

						
			
			if(pumps > 0)
			{
				anim.SetBool("Pump", false);
				
				


			}
			else
			{
				anim.SetBool("Pump", true);
			}
			



			


		}
	}


	public override void SecondaryAction()
	{
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("draw"))

		{



			anim.Play(pumpAnim);
			


		}



	}

	public override string hud_ammo()
	{

		return "Ammo: " + (pumps + 1);

	}

	public override void StartCommands()
	{

		fireEffect.SetActive(false);
		






	}


	public override void Update()
	{

		base.Update();

		if(InputFire1Down)
		{
			PrimaryAction();
		}
		if(InputFire2Down)
		{
			SecondaryAction();
		}



		if(hasFired)
		{
			pumps--;
			hasFired = false;
		}

		if(pumpForward && pumpBack)
		{
			pumps++;
			pumpBack = false;
			pumpForward = false;
		}

		if((pumps < 0 && gameObject.activeInHierarchy) && anim.GetCurrentAnimatorStateInfo(0).IsName("draw"))
		{
			anim.Play(pumpAnim);
		}



	}


	





}
