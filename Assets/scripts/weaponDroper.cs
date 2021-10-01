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
        WeaponClass weapon = thing.getBackpack().GetComponentInChildren<WeaponClass>(true);

        if (thing != null && weapon != null)
        {

            

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
