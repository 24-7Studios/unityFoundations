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


}
    
