using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolver : weaponClass
{






    public string fireAnim;
    public string reloadAnim;
    public string idleAnim;
    public float maxAmmo;
    public float spareAmmo;
    public float magSize;
    public float loadedAmmo;
    public Transform muzzleFlashPos;
    public GameObject muzzleFlash;
    public ParticleSystem sparks;
    public Transform sparkPos;
   

    public float viewPunch = 3;
    public bool hasLoaded;
    public bool hasFired;
 
    float effectTimer = 0.05f;
    












	public override void StartCommands()
	{
		
	}

	// Update is called once per frame
	public override void Update()
    {
        base.Update();

        if(InputFire1Hold)
		{
            PrimaryAction();
		}

        if(InputReloadDown)
		{
            reload();
		}

        if(hasFired)
		{

            fire();
            
		}

        if(hasLoaded)
		{
            loadedAmmo = magSize;
            sparks.Play();
            hasLoaded = false;
		}

        

    }



	public override void PrimaryAction()
	{
		

        if(loadedAmmo > 0 && anim.GetCurrentAnimatorStateInfo(0).IsName(idleAnim))
		{
            anim.SetBool("poopoo",true);
		}
        else if(anim.GetCurrentAnimatorStateInfo(0).IsName(idleAnim))
		{
            reload();
		}


	}

    public void reload()
	{

        if (loadedAmmo < magSize && anim.GetCurrentAnimatorStateInfo(0).IsName(idleAnim))
        {
            anim.Play(reloadAnim);
        }

	}

	public void fire()
    {


        anim.SetBool("poopoo",false);
        loadedAmmo--;


        GameObject c = Instantiate(muzzleFlash, muzzleFlashPos.position, muzzleFlashPos.rotation);
        Destroy(c, effectTimer);
        body.GetComponent<lookHandler_fly>().ymov -= viewPunch;

        hasFired = false;

    }
	
}







