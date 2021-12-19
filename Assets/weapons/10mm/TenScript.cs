using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenScript : gunClass
{
    [SerializeField]
    private string emptyReloadAnim;

    [SerializeField]
    private AudioClip emptyReload;

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
}
