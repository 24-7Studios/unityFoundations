using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class weaponGiver : NetworkBehaviour
{

    public GameObject myweapon;




    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {

        PlayerScript thing = other.GetComponent<PlayerScript>();

        if(thing != null)
        {

            

            GameObject spawnedW = Instantiate(myweapon, transform);

            
            
            NetworkServer.Spawn(spawnedW);

            if(!isServer)
            {
                thing.pickupWeapon(spawnedW, false);
            }

            rpcGiveWeapon(spawnedW, thing);

        }

    }

    [ClientRpc]
    private void rpcGiveWeapon(GameObject g, PlayerScript p)
    {

        p.pickupWeapon(g, false);

    }

}
