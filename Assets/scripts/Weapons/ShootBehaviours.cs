using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public static class ShootBehaviours
{

    public static void raycastShoot(float baseDamage, float multiplier, Vector3 position, Vector3 direction, LayerMask Shootable)
    {
        RaycastHit hit;

        if (Physics.Raycast(position, direction, out hit, Mathf.Infinity, Shootable))
        {

            IDamage iD = hit.collider.GetComponent<IDamage>();
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (iD != null)
            {
                cmdHitDamageable(hit.collider.gameObject, baseDamage, multiplier);
            }

            if (rb != null)
            {
                rb.AddForceAtPosition(direction, hit.point, ForceMode.Impulse);
                pushObject(hit.collider.gameObject, direction, hit.point);
            }

        }
    }

    [Command]
    private static void cmdHitDamageable(GameObject thing, float damage, float fleshMultiplier)
    {
        IDamage id = thing.GetComponent<IDamage>();
        
        id.takeDamagefromHit(damage, fleshMultiplier);
    }

    [Command]
    private static void pushObject(GameObject thing, Vector3 Direction, Vector3 Position)
    {
        Rigidbody rb = thing.GetComponent<Rigidbody>();

        rb.AddForceAtPosition(Direction, Position, ForceMode.Impulse);
    }

}
