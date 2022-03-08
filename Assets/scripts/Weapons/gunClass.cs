using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class gunClass : raycastWeapon
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
    protected int ammo;

    [SerializeField]
    protected int loadedAmmo;

    [SerializeField]
    protected float reloadDelay;

    protected float reloadTimer;

    protected bool reloading;
    protected bool bufferedReload;

    protected bool flash;


    protected override void Start()
    {
        base.Start();

        sounds.Add(fireSound);
        sounds.Add(reloadSound);

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

            if(reloadDown && loadedAmmo < ammo && !reloading)
            {
                bufferedReload = true;
            }

            if(!reloading)
            {
                if (fullAuto)
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
                    if (fire1Down && !hasShot)
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
                    if (!fire1Down && !fire2Down)
                    {
                        hasShot = false;
                    }
                }
            }
        }
    }

    protected virtual void LateUpdate()
    {
        if(flash)
        {
            if(ViewmodelFlash != null)
            {
                GameObject.Instantiate(ViewmodelFlash, ViewmodelflashPos.position, ViewmodelflashPos.rotation);
                flash = false;
            }
        }
    }

    protected override void Fire()
    {
        Vector3 shootDirection = (player.getCamTransformer().forward + Random.insideUnitSphere * spread).normalized;

        raycastShoot(damage, fleshMultiplier, player.getCamTransformer().position, shootDirection, Shootable);
        

        ViewAnim.Rebind();
        ViewAnim.Play(fireAnim);
        playsound(sounds.IndexOf(fireSound));
        player.viewPunch(viewpunch, viewpunchAttack, viewpunchRecovery);
        player.recoil(recoilAmount, recoilAttack, recoilSmoothing);
        loadedAmmo--;
        fireTimer = fireDelay;

        flash = true;

        if(WorldmodelFlash != null)
        {
            GameObject.Instantiate(WorldmodelFlash, WorldmodelFlashPos.position, WorldmodelFlashPos.rotation).layer = 6;
        }

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
    protected override void rpcFire()
    {
        if (isLocalPlayer) return;
        if (WorldmodelFlash != null)
        {
            GameObject.Instantiate(WorldmodelFlash, WorldmodelFlashPos.position, WorldmodelFlashPos.rotation).layer = 7; ;       
        }
    }

    [Command]
    protected override void cmdFire()
    {
        rpcFire();
    }

    protected override void AltFire()
    {
        
    }


    protected virtual void reload()
    {
        bufferedReload = false;
        ViewAnim.Play(reloadAnim);
        playsound(sounds.IndexOf(reloadSound));
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
        
    }

    [Command]
    protected virtual void cmdSyncLoadedAmmo(int a)
    {
        loadedAmmo = a;
    }

}
