using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using Mirror;

public class networkPlayerObject :  NetworkBehaviour
{
    static networkPlayerObject localPlayer;
    private List<NetworkIdentity> childPlayers;

    // Start is called before the first frame update
    void Start()
    {

    }


    public void addPlayer(NetworkIdentity netId)
    {
        childPlayers.Add(netId);
    }

    public GameObject requestPlayable()
    {
        return null;

        if(!isServer)
        {
            cmdGetPlayable();
        }
        else
        {

        }
    }



    [Command]
    public void cmdGetPlayable()
    {

    }


    public void removePlayer(NetworkIdentity netId)
    {
        childPlayers.Remove(netId);
    }

    public void addPlayer(GameObject plObject)
    {
        cmdAddPlayer(plObject);
    }

    [Command]
    private void cmdAddPlayer(GameObject plObject)
    {
        GameObject pl = Instantiate(PlayerManager.getInstance().getPlayable());
        pl.transform.position = ((myNetworkManager)myNetworkManager.singleton).GetStartPosition().position;
        NetworkServer.Spawn(pl);
        pl.GetComponent<networkPlayable>().setNetworkPlayer(this);
        pl.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        rpcAddPlayer(pl.GetComponent<NetworkIdentity>());

    }

    [ClientRpc]
    private void rpcAddPlayer(NetworkIdentity pl)
    {
        pl.GetComponent<networkPlayable>().setNetworkPlayer(this);
        if (isLocalPlayer && !isServer)
        {
            Player playerOb = pl.GetComponent<Player>();
            //iPlayable playable
            playerOb.addPlayable(pl.GetComponent<iPlayable>());
        }
    }

}
