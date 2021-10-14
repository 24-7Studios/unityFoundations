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
    protected float knockbackForce;

    [SerializeField]
    protected float spread;

    [SerializeField]
    protected bool fullAuto;

    [SerializeField]
    protected int ammo;

    [SerializeField]
    protected int loadedAmmo;

    [SerializeField]
    protected float fireDelay;

    [SerializeField]
    protected float reloadDelay;

    protected float fireTimer;
    protected float reloadTimer;

    protected bool hasShot;

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


        if(viewmodel.activeSelf)
        {
            if (reloading && ViewAnim.GetCurrentAnimatorStateInfo(0).IsName(idleAnim) && reloadTimer <= 0)
            {
                loadedAmmo = ammo;
                if(!isServer)
                {
                    cmdSyncLoadedAmmo(loadedAmmo);
                }
                bufferedReload = false;
                reloading = false;
            }

            if (bufferedReload && ViewAnim.GetCurrentAnimatorStateInfo(0).IsName(idleAnim))
            {
                reload();
            }

            if(fullAuto)
            {
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
            else
            {
                if(fire1Down && !hasShot)
                {
                    if (fireTimer <= 0)
                    {
                        if (loadedAmmo > 0)
                        {
                            Fire();
                            hasShot = true;
                        }
                        else if (!reloading)
                        {
                            bufferedReload = true;
                        }
                    }
                }
                if(!fire1Down)
                {
                    hasShot = false;
                }
            }
        }

    }


 
    protected virtual void Fire()
    {
        raycastShoot(damage, spread);
        ViewAnim.Rebind();
        ViewAnim.Play(fireAnim);
        aud.PlayOneShot(fireSound);
        player.viewPunch(viewpunch);
        loadedAmmo--;
        fireTimer = fireDelay;

        if (!isServer)
        {
            cmdFire();
            cmdSyncLoadedAmmo(loadedAmmo);
        }
        else
        {
            rpcFire();
        }
    }

    [ClientRpc]
    protected virtual void rpcFire()
    {
        aud.PlayOneShot(fireSound);
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
        ViewAnim.Play(reloadAnim);
        aud.PlayOneShot(reloadSound);
        reloadTimer = reloadDelay;
        reloading = true;
        if (isServer)
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
        aud.PlayOneShot(reloadSound);
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

        if (Physics.Raycast(player.getCamTransformer().position, shootDirection, out hit, Mathf.Infinity, Shootable))
        {

            IDamage iD = hit.collider.GetComponent<IDamage>();
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (iD != null)
            {
                cmdHitDamageable(hit.collider.gameObject, damage, fleshMultiplier);
            }

            if(rb != null)
            {
                rb.AddForceAtPosition(shootDirection, hit.point, ForceMode.Impulse);
                pushObject(hit.collider.gameObject, shootDirection, hit.point);
            }

        }
    }

    [Command]
    protected virtual void cmdHitDamageable(GameObject thing, float damage, float fleshMultiplier)
    {
        IDamage id = thing.GetComponent<IDamage>();

        id.takeDamagefromHit(damage, fleshMultiplier);
    }

    [Command]
    protected virtual void pushObject(GameObject thing, Vector3 Direction, Vector3 Position)
    {
        Rigidbody rb = thing.GetComponent<Rigidbody>();

        rb.AddForceAtPosition(Direction, Position, ForceMode.Impulse);
    }

}
