using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioListenerEnable : MonoBehaviour
{

    public AudioListener aud;

    private void Awake()
    {
        PlayerScript.standby += EnableAud;
        PlayerScript.Renable += DisableAud;
    }

    public void EnableAud(PlayerScript p)
    {
        if (p.isLocalPlayer)
        {
            aud.enabled = true;
        }
    }

    public void DisableAud(PlayerScript p)
    {
        if(p.isLocalPlayer)
        {
            aud.enabled = false;
        }
    }

}
