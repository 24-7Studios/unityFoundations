using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class myNetworkManager : NetworkManager
{
    // Start is called before the first frame update
    public override void Start()
    {
        transport = GetComponent<newNetworkHUD>().getCurrentTranport();
        base.Start();
    }
}
