using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raptorScript : gunClass
{

    [SerializeField]
    protected AudioClip ChamberClicksound;


    protected override void Start()
    {
        base.Start();

        sounds.Add(ChamberClicksound);
    }


    protected override void Update()
    {
        fireTimer -= Time.deltaTime;


        if (viewmodel.activeSelf)
        {
            ViewAnim.SetBool("isLoading", reloading);

            if (bufferedReload && ViewAnim.GetCurrentAnimatorStateInfo(0).IsName(idleAnim))
            {
                reload();
            }

            if (reloadDown && loadedAmmo < ammo && !reloading)
            {
                bufferedReload = true;
            }

            if (!reloading)
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
                            else
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
                            else
                            {
                                bufferedReload = true;
                                hasShot = true;
                            }
                        }
                    }
                    if (!fire1Down && !fire2Down)
                    {
                        hasShot = false;
                    }
                }
            }
            else if(loadedAmmo >= ammo)
            {
                reloading = false;
            }
            else if (ammo > 0)
            {
                if (fire1Down && !hasShot)
                {
                    if (fireTimer <= 0)
                    {
                        reloading = false;
                        hasShot = true;
                    }
                }
                if (!fire1Down && !fire2Down)
                {
                    hasShot = false;
                }
            }
        }
    }



    /*
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
    */



    protected override void reload()    
    {
        reloading = true;
        bufferedReload = false;
        if (isServer)
        {
            rpcReload();
        }
        else
        {
            cmdReload();
        }
    }

    
    public virtual void chamberClick()
    {
        playsound(sounds.IndexOf(ChamberClicksound));
    }

    public virtual void shellIn()
    {
        playsound(sounds.IndexOf(reloadSound));
        loadedAmmo++;
        if (!isServer)
        {
            cmdSyncLoadedAmmo(loadedAmmo);
        }
    }


}
