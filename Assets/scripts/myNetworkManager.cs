using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class myNetworkManager : NetworkManager
{

    private newNetworkHUD hud;


    public override void Awake()
    {
        hud = GetComponent<newNetworkHUD>();
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        //PlayerManager.getInputManager().DisableJoining();
    }

    private void Update()
    {
        transport = hud.getCurrentTranport();
        Transport.activeTransport = transport;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        //PlayerManager.getInputManager().EnableJoining();


        //NetworkServer.RegisterHandler<playerStruct>(OnCreateCharacter);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("CONNECTED AND READY!");
        PlayerManager.getInputManager().EnableJoining();
        Debug.Log("Local Player Joining Enabled : " + PlayerManager.getInputManager().joiningEnabled);
    }



    public GameObject addPlayer(GameObject plObject)
    {
        cmdAddPlayer(plObject, NetworkClient.localPlayer);
    }

    [Command]
    private void cmdAddPlayer(GameObject plObject, NetworkIdentity owner)
    {
        GameObject pl = Instantiate(plObject);
        pl.transform.position = GetStartPosition().position;
        NetworkServer.Spawn(pl);
        pl.GetComponent<networkPlayable>().setNetworkPlayer(owner.GetComponent<networkPlayerObject>());
        pl.GetComponent<NetworkIdentity>().AssignClientAuthority(owner.connectionToClient);
        rpcAddPlayer(pl.GetComponent<NetworkIdentity>(), owner);

    }

    [ClientRpc]
    private void rpcAddPlayer(NetworkIdentity pl, NetworkIdentity owner)
    {
        pl.GetComponent<networkPlayable>().setNetworkPlayer(owner.GetComponent<networkPlayerObject>());
        if(NetworkClient.localPlayer == owner)
        {
            Player playerOb = pl.GetComponent<Player>();
            iPlayable playable 
            playerOb.addPlayable(pl.GetComponent<>)
        }
    }

}
    
