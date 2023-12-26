using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using Mirror;

public class networkPlayerObject :  NetworkBehaviour
{
    static networkPlayerObject localPlayer;
    private List<NetworkIdentity> childPlayables;

    // Start is called before the first frame update
    void Start()
    {
        childPlayables = new List<NetworkIdentity>();
    }


    public void requestPlayable(int PIndex)
    {
        cmdAddPlayable(PIndex);
    }

    [Command]
    public void cmdAddPlayable(int PIndex)
    {
        GameObject pl = Instantiate(PlayerManager.getInstance().getPlayable());
        pl.transform.position = ((myNetworkManager)myNetworkManager.singleton).GetStartPosition().position;
        NetworkServer.Spawn(pl);
        pl.GetComponent<networkPlayable>().setNetworkPlayer(this);
        NetworkIdentity ObId = pl.GetComponent<NetworkIdentity>();
        ObId.AssignClientAuthority(connectionToClient);
        rpcAddPlayble(PIndex, ObId);

    }

    [ClientRpc]
    public void rpcAddPlayble(int PIndex, NetworkIdentity ObId)
    {
        networkPlayable obPlay = ObId.gameObject.GetComponent<networkPlayable>();
        obPlay.setNetworkPlayer(this);
        childPlayables.Add(ObId);
        if(isLocalPlayer)
        {
            PlayerManager.getInstance().getPlayer(PIndex).addPlayable(obPlay);
            PlayerManager.getInstance().getPlayer(PIndex).activate(obPlay);
        }
    }

    /*
    [Command]
    public void cmdGetPlayable()
    {

    }


    public void removePlayer(NetworkIdentity netId)
    {
        childPlayables.Remove(netId);
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
    */

}
