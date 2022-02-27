using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChautShot : gunClass
{

    [SerializeField]
    protected int shots = 8;

    [SerializeField]
    private AudioClip openSound;

    [SerializeField]
    private AudioClip closeSound;

    [SerializeField]
    private AudioClip magInSound;

    [SerializeField]
    private AudioClip magOutSound;

    protected override void Start()
    {
        base.Start();

        sounds.Add(openSound);
        sounds.Add(closeSound);
        sounds.Add(magInSound);
        sounds.Add(magOutSound);

    }

    protected override void reload()
    {
        bufferedReload = false;
        ViewAnim.Play(reloadAnim);
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
        base.Fire();
        for (int i = 0; i < shots - 1; i++)
        {
            Vector3 shootDirection = (player.getCamTransformer().forward + Random.insideUnitSphere * spread).normalized;

            raycastShoot(damage, fleshMultiplier, player.getCamTransformer().position, shootDirection, Shootable);
        }
    }

    public void open()
    {
        playsound(sounds.IndexOf(openSound));
    }

    public void close()
    {
        playsound(sounds.IndexOf(closeSound));
    }

    public void magIn()
    {
        playsound(sounds.IndexOf(magInSound));
    }

    public void magOut()
    {
        playsound(sounds.IndexOf(magOutSound));
    }


}
