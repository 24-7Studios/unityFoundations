using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class gunClass : WeaponClass
{

    [SerializeField]
    protected string fireAnim;

    [SerializeField]
    protected AudioClip fireSound;

    [SerializeField]
    protected string reloadAnim;

    [SerializeField]
    protected AudioClip reloadSound;

    [SerializeField]
    protected string idleAnim;

    [SerializeField]
    protected double viewpunch;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected int ammo;

    [SerializeField]
    protected int loadedAmmo;

    [SerializeField]
    protected double fireDelay;

    protected double fireTimer;





    protected virtual void Update()
    {
        fireTimer -= Time.deltaTime;

        if(anim.animator.GetBool("doneReloading"))
        {
            loadedAmmo = ammo;
            anim.animator.SetBool("doneRealoading", false);
        }
    }


    protected override void onFire(InputAction.CallbackContext ctx)
    {

        if(ammo > 0)
        {
            if (fireTimer <= 0)
            {
                anim.animator.Play(fireAnim);
                aud.PlayOneShot(fireSound);
                ammo--;
                fireTimer = fireDelay;
            }
        }
        else
        {
            reload(ctx);
        }

    }

    protected override void onAltFire(InputAction.CallbackContext ctx)
    {
        
    }

    protected override void reload(InputAction.CallbackContext ctx)
    {
        anim.animator.Play(reloadAnim);
        aud.PlayOneShot(reloadSound);

    }

    protected virtual void raycastHit()
    {

    }

}
