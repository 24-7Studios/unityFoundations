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

    [SyncVar]
    protected float heat;

    protected override void Update()
    {
        fireTimer -= Time.deltaTime;
        heat -= Time.deltaTime * coolRate;
        if (heat < 0)
            heat = 0;

        if (viewmodel.activeSelf)
        {
            if (heat == 100)
            {

            }
        }

           
    }

}
