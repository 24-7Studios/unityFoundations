using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChautShot : gunClass
{

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
