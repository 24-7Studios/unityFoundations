using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public interface Ipickup
{
    GameObject getObject();

    void pickup(PlayerScript p);

    void serverPickup(PlayerScript p);

    void drop();

    void serverDrop();

}
