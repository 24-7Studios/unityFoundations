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

	public float baseDamage;
	public float fleshMulitplier;
	public float hitForce;
	public float spread;
	public int pellets;

	public GameObject impactEffect;

	public GameObject DebugObject;

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

			player.looker.viewPunch(viewPunch);
			GameObject c = Instantiate(fireEffect, firePosition);
			Destroy(c, effectTimer);

			for (int i = 0; i < pellets; i++)
			{
				RaycastHit hit;


				Vector3 shootDirection = (player.camTransformer.forward + Random.insideUnitSphere * spread).normalized;

				if (Physics.Raycast(player.camTransformer.position, shootDirection, out hit, Mathf.Infinity, Shootable))
				{
					if (debugMode)
					{
						Debug.Log(hit.collider.gameObject.name);
						Instantiate(DebugObject, hit.point, Quaternion.Euler(hit.normal));
					}


					IDamage b = hit.collider.GetComponentInParent<IDamage>();

					Rigidbody r = hit.collider.GetComponent<Rigidbody>();

					if (b != null)
					{

						b.takeDamagefromHit(baseDamage, fleshMulitplier);
					}
					if (r != null)
					{
						

						r.AddForceAtPosition(player.camTransformer.forward * hitForce, hit.point, ForceMode.Impulse);
					}

					GameObject fx = Instantiate(impactEffect);
					fx.transform.position = hit.point;
					fx.transform.forward = hit.normal;


				}
			}
						
			
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
