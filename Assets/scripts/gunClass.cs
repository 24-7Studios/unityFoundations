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

    [SerializeField]
    protected double reloadDelay;

    protected double fireTimer;
    protected double reloadTimer;

    protected bool reloading;
    protected bool bufferedReload;


    protected override void Start()
    {
        base.Start();

        loadedAmmo = ammo;
    }

    protected override void Update()
    {
        fireTimer -= Time.deltaTime;
        reloadTimer -= Time.deltaTime;

        if(fire1Down)
        {
            Fire();
        }

        if(reloading && anim.animator.GetCurrentAnimatorStateInfo(0).IsName(idleAnim) && reloadTimer <= 0)
        {
            loadedAmmo = ammo;
            bufferedReload = false;
            reloading = false;
        }

        if(bufferedReload && anim.animator.GetCurrentAnimatorStateInfo(0).IsName(idleAnim))
        {
            reload();
        }
    }


    protected virtual void Fire()
    {

        if(loadedAmmo > 0)
        {
            if (fireTimer <= 0)
            {
                anim.animator.Rebind();
                anim.animator.Play(fireAnim);
                aud.PlayOneShot(fireSound);
                loadedAmmo--;
                fireTimer = fireDelay;
            }
        }
        else if(!reloading)
        {
            bufferedReload = true;
        }

        


    }

    protected virtual void AltFire()
    {
        
    }

    protected virtual void reload()
    {
        anim.animator.Play(reloadAnim);
        aud.PlayOneShot(reloadSound);
        reloadTimer = reloadDelay;
        reloading = true;

    }

    protected virtual void raycastHit()
    {

    }

}
