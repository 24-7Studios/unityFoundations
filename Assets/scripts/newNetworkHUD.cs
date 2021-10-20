using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class newNetworkHUD : NetworkManagerHUD
{

    [SerializeField]
    List<Transport> transports;

    [SerializeField]
    private int index = 0;

    

    public Transport getCurrentTranport()
    {
        return transports[index];
    }

}
