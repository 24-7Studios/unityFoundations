using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface networkPlayable : iPlayable
{
    public void setNetworkPlayer(networkPlayerObject net);
}
