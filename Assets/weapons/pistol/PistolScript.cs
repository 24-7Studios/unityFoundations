using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : weaponClass
{





	public string fireAnim;
	public string idleAnim;
	public string overheatPull;
	public string overheatLoop;
	public Transform firePosition;
	public GameObject fireEffect;
	public Transform smokePosition;
	public ParticleSystem smokeEffect;
	public GameObject impactEffect;

	public GameObject DebugObject;

	public float baseDamage = 15;
	public float fleshMulitplier = 1f;
	public float hitForce = 10;
	public float spread = 2;


	public float heatupPerShot = 6;
	public float cooldownSpeed = 2;
	public float fireDelay = 0.5f;
	public float burst = 3;
	public float viewPunch = 5;
	public float heat = 0;

	private bool bursting;
	private bool isBurst;
	private float burstNum = 0;
	//private float cooldownTimer;
	private bool isCooling;
	private float fireTimer;

	private float effectTimer = 0.1f;


	public override void PrimaryAction()
	{

		if ((isBurst && anim.GetCurrentAnimatorStateInfo(0).IsName(idleAnim)) && !isCooling)
		{


			bursting = true;


		}
		else
		{
			if ((fireTimer < 0) && !isBurst && (!isCooling) && (anim.GetCurrentAnimatorStateInfo(0).IsName(idleAnim) || anim.GetCurrentAnimatorStateInfo(0).IsName(fireAnim)))
			{
				fire();
			}


		}






	}


	public override void SecondaryAction()
	{

		isBurst = !isBurst;



	}



	





	public void fire()
	{

		anim.Rebind();
		anim.Play(fireAnim);
		player.looker.viewPunch(viewPunch);
		GameObject c = Instantiate(fireEffect, firePosition);
		Destroy(c, effectTimer);
		heat += heatupPerShot;
		fireTimer = fireDelay;

		RaycastHit hit;
		
		
		Vector3 shootDirection = (player.camTransformer.forward + Random.insideUnitSphere * spread).normalized;

		if(Physics.Raycast(player.camTransformer.position ,shootDirection , out hit, Mathf.Infinity, Shootable))
		{
			if (debugMode)
			{
				Debug.Log(hit.collider.gameObject.name);
				Instantiate(DebugObject, hit.point, Quaternion.Euler(hit.normal));
			}


			
			
			if(hit.collider.GetComponentInParent<being>())
			{

				being b = hit.collider.GetComponentInParent<being>();

				b.takeDamagefromHit(baseDamage, fleshMulitplier);
			}
			if(hit.collider.GetComponent<Rigidbody>())
			{
				Rigidbody r = hit.collider.GetComponent<Rigidbody>();

				r.AddForceAtPosition(player.camTransformer.forward * hitForce, hit.point, ForceMode.Impulse);
			}

			GameObject fx = Instantiate(impactEffect);
			fx.transform.position = hit.point;
			fx.transform.forward = hit.normal;


		}

	}


	public override void StartCommands()
	{

		smokeEffect.Play();

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

		fireTimer -= Time.deltaTime;
		
		var em = smokeEffect.emission;

		anim.SetBool("isCooling", isCooling);

		if (isCooling && anim.GetCurrentAnimatorStateInfo(0).IsName(idleAnim))
		{

			anim.Play(overheatPull);
			

		}

		if (anim.GetCurrentAnimatorStateInfo(0).IsName(overheatLoop))
		{

			em.enabled = true;
			
		}
		else
		{
			em.enabled = false;

		}


		
			heat -= Time.deltaTime * cooldownSpeed;
			heat = Mathf.Clamp(heat, 0, 100);
		



		if ((bursting && fireTimer < 0))
		{

			fire();
			burstNum++;


		}
		else if (!(burstNum < burst))
		{
			bursting = false;
			burstNum = 0;

		}



		if (heat == 100)
		{

			isCooling = true;

		}


		if(isCooling)
		{

			



			if(heat == 0)
			{

				isCooling = false;

			}


		}









	}



	

	


}
