using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using Mirror;

public class networkPlayerObject :  NetworkBehaviour
{

    static networkPlayerObject networkPlayerInstance;
    private List<NetworkIdentity> childPlayers;

    // Start is called before the first frame update
    void Start()
    {
        if (networkPlayerInstance != null)
            Debug.LogError("There is already a player for this connection!");
        else if(isLocalPlayer)
            networkPlayerInstance = this;
        
    }

    public void addPlayer(NetworkIdentity netId)
    {
        childPlayers.Add(netId);
    }

    public void removePlayer(NetworkIdentity netId)
    {
        childPlayers.Remove(netId);
    }

    public static networkPlayerObject getInstance()
    {
        return networkPlayerInstance;
    }

}
