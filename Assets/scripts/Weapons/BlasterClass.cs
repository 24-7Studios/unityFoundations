using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BlasterClass : gunClass
{
    [SerializeField]
    protected float coolRate;

    [SerializeField]
    protected float heatPerShot;

    [SerializeField]
    protected string coolAnim;

    [SyncVar]
    protected float heat;

    protected override void Update()
    {
        fireTimer -= Time.deltaTime;
        heat -= Time.deltaTime * coolRate;
        if (heat < 0)
        {
            heat = 0;
        }

        if (viewmodel.activeSelf)
        {
            if(heat == 0)
            {
                ViewAnim.SetBool("isCooling", false);
            }

            if(!ViewAnim.GetBool("isCooling"))
            {
                if (heat >= 100)
                {
                    ViewAnim.SetBool("isCooling", true);
                    ViewAnim.Play(coolAnim);
                }
                if (fullAuto)
                {
                    if (fire1Down)
                    {
                        if (fireTimer <= 0)
                        {
                            Fire();
                        }
                    }
                }
                else
                {
                    if (fire1Down && !hasShot)
                    {
                        if (fireTimer <= 0)
                        {
                            Fire();
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

           
    }


    protected override void Fire()
    {
        base.Fire();
        ViewAnim.Rebind();
        ViewAnim.Play(fireAnim);
        playsound(sounds.IndexOf(fireSound));
        player.viewPunch(viewpunch);
        heat += heatPerShot;
        fireTimer = fireDelay;

        if (!isServer)
        {
            cmdFire();
            cmdSyncHeat(heat);
        }
        else
        {
            rpcFire();
        }
    }


    [Command]
    protected void cmdSyncHeat(float h)
    {
        heat = h;
    }


    [ClientRpc]
    protected override void rpcFire()
    {

    }

    [Command]
    protected override void cmdFire()
    {
        rpcFire();
    }

}
