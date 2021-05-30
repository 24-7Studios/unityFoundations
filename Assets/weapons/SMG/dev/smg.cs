using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class smg : weaponClass
{

    public Text invAmmo;
    public Slider ammoSlide;
    public string fireAnim;
    public string reloadAnim;
    public string idleAnim;

    public float baseDamage;
    public float fleshMulitplier;
    public float spread;
    public float hitForce;

    public GameObject DebugObject;

    public float reloadTime;
    public float fireDelay;
    public float maxAmmo;
    public float spareAmmo;
    public float magSize;
    public float loadedAmmo;
    public Transform muzzleFlashPos;
    public GameObject muzzleFlash;
    public GameObject casing;
    public Transform ejection;
    public float ejectionForce;
    public float ejectionForceSpread;

    public float viewPunch = 3;


    float effectTimer = 0.01f;
    float fireTimer;
    float reloadTimer;
    public bool doneReloading;




	public override void Update()
	{
		base.Update();

        fireTimer -= Time.deltaTime;
        reloadTimer -= Time.deltaTime;
        
        anim.SetBool("doneReloading", doneReloading);

        if(InputFire1Hold)
		{
            PrimaryAction();
		}

        if(InputReloadDown && loadedAmmo < magSize)
		{
            reload();
   		}

        if(anim.GetCurrentAnimatorStateInfo(0).IsName(reloadAnim))
		{

            if(reloadTimer <= 0)
			{
                doneReloading = true;
                loadedAmmo = magSize;
			}

		}

      //  invAmmo.text = (spareAmmo + "/" + maxAmmo);
			

    }



	public override void PrimaryAction()
	{
		
        if((anim.GetCurrentAnimatorStateInfo(0).IsName(idleAnim) || anim.GetCurrentAnimatorStateInfo(0).IsName(fireAnim)) && loadedAmmo > 0 && fireTimer <= 0 )
		{
            fire();
		}
        else if(anim.GetCurrentAnimatorStateInfo(0).IsName(idleAnim) && loadedAmmo == 0)
		{
            reload();
		}



	}

    public void reload()
	{

        anim.Play(reloadAnim);
        reloadTimer = reloadTime;
        doneReloading = false;

	}


	public void fire()
    {
        

        anim.Rebind();
        anim.Play(fireAnim);
        player.looker.viewPunch(viewPunch);
        GameObject c = Instantiate(muzzleFlash, muzzleFlashPos.position, muzzleFlashPos.rotation);
        c.transform.position = muzzleFlashPos.position;
        c.transform.rotation = muzzleFlashPos.rotation;
        GameObject b = Instantiate(casing, ejection.position, ejection.rotation);
        b.transform.position = ejection.position;
        b.transform.rotation = ejection.rotation;
        b.GetComponent<Rigidbody>().velocity = ejection.up * (Random.value * ejectionForceSpread + ejectionForce);
        Destroy(c, effectTimer);
        loadedAmmo--;
        fireTimer = fireDelay;

        RaycastHit hit;


        Vector3 shootDirection = (player.camTransformer.forward + Random.insideUnitSphere * spread).normalized;

        if (Physics.Raycast(player.camTransformer.position, shootDirection, out hit, Mathf.Infinity, Shootable))
        {
            if (debugMode)
            {
                Debug.Log(hit.collider.gameObject.name);
                Instantiate(DebugObject, hit.point, Quaternion.Euler(hit.normal));
            }




            if (hit.collider.GetComponentInParent<being>())
            {

                being be = hit.collider.GetComponentInParent<being>();

                be.takeDamagefromHit(baseDamage, fleshMulitplier);
            }
            if (hit.collider.GetComponent<Rigidbody>())
            {
                Rigidbody r = hit.collider.GetComponent<Rigidbody>();

                r.AddForceAtPosition(player.camTransformer.forward * hitForce, hit.point, ForceMode.Impulse);
            }


        }


    }



	


	public override void StartCommands()
	{

        loadedAmmo = magSize;

	}




}
