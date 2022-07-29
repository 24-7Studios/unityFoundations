using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Target : NetworkBehaviour, IDamage
{

    [SerializeField]
    private float health;

    [SerializeField]
    private AudioClip sound;

    [SerializeField]
    private AudioSource aud;

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
        health -= dam;
        rpcPlayHitSound();
    }

    [ClientRpc]
    protected void rpcPlayHitSound()
    {
        if(aud != null && sound != null)
        {
            aud.PlayOneShot(sound);
        }
    }

    public void hit()
    {

    }

    public void die()
    {

    }
}
