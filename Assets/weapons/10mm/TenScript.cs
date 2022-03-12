using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenScript : gunClass
{
    [SerializeField]
    private string emptyReloadAnim;

    [SerializeField]
    private AudioClip emptyReload;

    [SerializeField]
    protected float aimRadius = 2;

    protected override void Start()
    {
        base.Start();

        sounds.Add(emptyReload);
    }

    protected override void reload()
    {
        if(loadedAmmo > 0)
        {
            ViewAnim.Play(reloadAnim);
            playsound(sounds.IndexOf(reloadSound));
        }
        else
        {
            ViewAnim.Play(emptyReloadAnim);
            playsound(sounds.IndexOf(emptyReload));
        }

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

        if (WorldmodelFlash != null)
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
}
