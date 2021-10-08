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
    protected string playerModelReload;

    [SerializeField]
    protected string playerModelFire;

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

    protected bool reloading;


    protected override void Start()
    {
        base.Start();

        loadedAmmo = ammo;
    }

    protected override void Update()
    {
        fireTimer -= Time.deltaTime;

        if(reloading && !anim.animator.GetCurrentAnimatorStateInfo(0).IsName(reloadAnim))
        {
            loadedAmmo = ammo;
            reloading = false;
        }
    }


    protected override void onFire(InputAction.CallbackContext ctx)
    {

        if(loadedAmmo > 0)
        {
            if (fireTimer <= 0)
            {
                anim.animator.Play(fireAnim);
                aud.PlayOneShot(fireSound);
                loadedAmmo--;
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
        reloading = true;

    }

    protected virtual void raycastHit()
    {

    }

}
