using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InvinsibilityZone : MonoBehaviour
{
    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();

        if (player != null)
        {
            player.setInv(true);
        }

    }

    [ServerCallback]
    private void OnTriggerExit(Collider other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();

        if (player != null)
        {
            player.setInv(false);
        }
    }
}
