using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
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
    protected float viewpunch;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected int ammo;

    [SerializeField, SyncVar]
    protected int loadedAmmo;

    [SerializeField]
    protected float fireDelay;

    [SerializeField]
    protected float reloadDelay;

    protected float fireTimer;
    protected float reloadTimer;

    protected bool reloading;
    protected bool bufferedReload;


    protected override void Start()
    {
        base.Start();

        loadedAmmo = ammo;
    }

    protected override void Update()
    {
        if(viewmodel.activeInHierarchy)
        {
            fireTimer -= Time.deltaTime;
            reloadTimer -= Time.deltaTime;

            if (fire1Down)
            {
                if (loadedAmmo > 0)
                {
                    if (fireTimer <= 0)
                    {
                        Fire();
                    }
                }
                else if (!reloading)
                {
                    bufferedReload = true;
                }
            }

            if (reloading && anim.animator.GetCurrentAnimatorStateInfo(0).IsName(idleAnim) && reloadTimer <= 0)
            {
                loadedAmmo = ammo;
                bufferedReload = false;
                reloading = false;
            }

            if (bufferedReload && anim.animator.GetCurrentAnimatorStateInfo(0).IsName(idleAnim))
            {
                reload();
            }
        }
    }


 
    protected virtual void Fire()
    {
        if(isServer)
        {
            rpcFire();
        }
        else
        {
            cmdFire();
        }
    }

    [ClientRpc]
    protected virtual void rpcFire()
    {
        anim.animator.Rebind();
        anim.animator.Play(fireAnim);
        aud.PlayOneShot(fireSound);
        player.viewPunch(viewpunch);
        loadedAmmo--;
        fireTimer = fireDelay;
    }

    [Command]
    protected virtual void cmdFire()
    {
        rpcFire();
    }

    protected virtual void AltFire()
    {
        
    }


    protected virtual void reload()
    {
        if(isServer)
        {
            rpcReload();
        }
        else
        {
            cmdReload();
        }
    }

    [Command]
    protected virtual void cmdReload()
    {
        rpcReload();
    }

    [ClientRpc]
    protected virtual void rpcReload()
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
