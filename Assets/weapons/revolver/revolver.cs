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
    public GameObject sparks;
    public Transform sparkPos;
   

    public float viewPunch = 3;


    float effectTimer = 0.01f;













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

        

        

    }



	public override void PrimaryAction()
	{
		

        if(loadedAmmo > 0 && anim.GetCurrentAnimatorStateInfo(0).IsName(idleAnim))
		{
            fire();
		}


	}

	public void fire()
    {

        anim.Play(fireAnim);
        loadedAmmo--;
    
    }
	
}







