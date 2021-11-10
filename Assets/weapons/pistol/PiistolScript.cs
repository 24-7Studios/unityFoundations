using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiistolScript : BlasterClass
{

    [SerializeField]
    protected int burstAmount;

    [SerializeField]
    protected AudioClip clickSound;

    protected bool burst;
    protected bool inBurst;
    protected int burstCounter;


    protected override void Start()
    {
        base.Start();

        sounds.Add(clickSound);
    }

    protected override void Update()
    {
        base.Update();

        if(viewmodel.activeSelf)
        {
            if(fire2Down && !hasShot)
            {
                AltFire();
                hasShot = true;
            }

            if(burstCounter == 0)
            {
                inBurst = false;
                burstCounter = burstAmount;
            }

            if(inBurst && fireTimer <= 0)
            {
                Fire();
                burstCounter--;
            }
        }

    }




    protected override void AltFire()
    {
        burst = !burst;
        playsound(sounds.IndexOf(clickSound));
    }

    protected override void Fire()
    {
        if(burst)
        {
            inBurst = true;
            return;
        }
        base.Fire();
    }


}
