using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class weaponDroper : NetworkBehaviour
{


    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {

        PlayerScript thing = other.GetComponent<PlayerScript>();

        if (thing != null)
        {

            WeaponClass weapon = thing.backpack.GetComponentInChildren<WeaponClass>(true);

            thing.drop(weapon.gameObject);
            rpcDropWeapon(weapon.gameObject, thing);

            
        }

    }

    [ClientRpc]
    private void rpcDropWeapon(GameObject g, PlayerScript p)
    {

        p.drop(g);

    }


}
