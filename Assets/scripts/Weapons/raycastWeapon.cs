using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class raycastWeapon : WeaponClass
{
    [SerializeField]
    protected float viewpunch;

    [SerializeField]
    protected float viewpunchAttack = 5f;

    [SerializeField]
    protected float viewpunchRecovery = 0.05f;

    [SerializeField]
    protected float damage;

    [SerializeField]
    protected float fleshMultiplier;

    [SerializeField]
    protected float knockbackForce;

    [SerializeField]
    protected float spread;

    [SerializeField]
    protected bool fullAuto;

    [SerializeField]
    protected float fireDelay;

    protected float fireTimer;

    protected bool hasShot;



    protected override void Update()
    {

    }



    protected virtual void Fire()
    {
        Vector3 shootDirection = (player.getCamTransformer().forward + Random.insideUnitSphere * spread).normalized;

        raycastShoot(damage, fleshMultiplier, player.getCamTransformer().position, shootDirection, Shootable);
    }

    [ClientRpc]
    protected virtual void rpcFire()
    {
    }

    [Command]
    protected virtual void cmdFire()
    {
        rpcFire();
    }

    protected virtual void AltFire()
    {

    }

    [Command]
    protected virtual void cmdAltFire()
    {
        rpcAltFire();
    }

    [ClientRpc]
    protected virtual void rpcAltFire()
    {

    }
}
