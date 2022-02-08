using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : gunClass
{

    [SerializeField]
    protected int shots = 10;

    [SerializeField]
    protected float altFireSpread = 0.5f;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(viewmodel.activeSelf)
        {
            if (fire2Down && !hasShot)
            {
                if (fireTimer <= 0)
                {
                    if (loadedAmmo > 0)
                    {
                        AltFire();
                        hasShot = true;
                    }
                    else if (!reloading)
                    {
                        bufferedReload = true;
                    }
                }
            }
        }
    }

    protected override void AltFire()
    {
        float Tspread = spread;

        spread = altFireSpread;

        if(loadedAmmo > 1)
        {
            Fire();
        }
        Fire();

        spread = Tspread;
    }

    protected override void Fire()
    {
        base.Fire();
        for(int i = 0; i < shots-1; i++)
        {
            Vector3 shootDirection = (player.getCamTransformer().forward + Random.insideUnitSphere * spread).normalized;

            raycastShoot(damage, fleshMultiplier, player.getCamTransformer().position, shootDirection, Shootable);
        }
    }

}