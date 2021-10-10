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
    protected float damage;

    [SerializeField]
    protected float fleshMultiplier;

    [SerializeField]
    protected float spread;

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
        fireTimer -= Time.deltaTime;
        reloadTimer -= Time.deltaTime;

        if(viewmodel.activeInHierarchy)
        {
            if (reloading && anim.animator.GetCurrentAnimatorStateInfo(0).IsName(idleAnim) && reloadTimer <= 0)
            {
                if(!isServer)
                {
                    cmdSyncLoadedAmmo(ammo);
                }
                else
                {
                    loadedAmmo = ammo;
                }
                bufferedReload = false;
                reloading = false;
            }

            if (bufferedReload && anim.animator.GetCurrentAnimatorStateInfo(0).IsName(idleAnim))
            {
                reload();
            }

            if (fire1Down)
            {
                if (fireTimer <= 0)
                {
                    if (loadedAmmo > 0)
                    {
                        Fire();
                    }
                    else if (!reloading)
                    {
                        bufferedReload = true;
                    }
                }
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
        if(viewmodel.activeInHierarchy)
        {
            anim.animator.Rebind();
            anim.animator.Play(fireAnim);
        }
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
        if(viewmodel.activeInHierarchy)
        {
            anim.animator.Play(reloadAnim);
        }
        aud.PlayOneShot(reloadSound);
        reloadTimer = reloadDelay;
        reloading = true;
    }

    [Command]
    protected virtual void cmdSyncLoadedAmmo(int a)
    {
        loadedAmmo = a;
    }

    protected virtual void raycastShoot(float dam, float sp)
    {
        RaycastHit hit;

        Vector3 shootDirection = (player.getCamTransformer().forward + Random.insideUnitSphere * sp).normalized;

        if (Physics.Raycast(player.getCamTransformer().position, shootDirection, out hit, Mathf.Infinity, Layermasks.Shoot))
        {

            IDamage iD = hit.collider.GetComponent<IDamage>();
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (iD != null)
            {
                iD.takeDamagefromHit(damage, fleshMultiplier);
            }

            if(rb != null)
            {

            }

        }
    }

}
