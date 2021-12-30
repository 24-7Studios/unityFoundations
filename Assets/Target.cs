using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Target : NetworkBehaviour, IDamage
{

    public AudioClip sound;
    public AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamagefromHit(float dam, float mul)
    {
        rpcPlayHitSound();
    }

    [ClientRpc]
    protected void rpcPlayHitSound()
    {
        aud.PlayOneShot(sound);
    }

    public void hit()
    {

    }

    public void die()
    {

    }
}
