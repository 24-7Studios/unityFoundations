using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Announcer : NetworkBehaviour
{

    private void Start()
    {
        PlayerScript.died += onPlayerDied;
    }

    protected void onPlayerDied(PlayerScript p)
    {
        cmdBroadcast(p + "had died!"); 
    }

    [Command]
    private void cmdBroadcast(string message)
    {
        rpcBroadcast(message);
    }

    private void rpcBroadcast(string message)
    {
        Debug.Log(message);
    }
}
