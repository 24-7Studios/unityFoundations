using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{
    public GameObject effect;
    public AudioClip sound;

    public float fuse = 2;
    public float overFuse = 8;
    public bool startOnContact = false;
    public float explosionRadius = 5;
    public float explosionForce = 10;

    float timer = 0;
    float overTimer = 0;
    bool flag = true;

    // Start is called before the first frame update
    void Start()
    {

        
        timer = fuse;
        overTimer = overFuse;
        if (startOnContact == true)
        {


            flag = false;


        }

    }

    // Update is called once per frame
    void Update()
    {


        overTimer -= Time.deltaTime;

        

        if (flag == true)
        {
            timer -= Time.deltaTime;
        }

        if ((flag == true && timer <= 0) || overTimer <= 0)
        {
            explode();
        }



    }


    private void OnCollisionEnter(Collision collision)
    {

        if (startOnContact == true)
        {


            flag = true;


        }



    }

    void explode()
    {

        
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in colliders)
        {

            if (hit.attachedRigidbody)
            {
                hit.attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

        }

        AudioSource.PlayClipAtPoint(sound, transform.position, 100);
        Destroy(Instantiate(effect, transform.position, transform.rotation), 2);

        Destroy(gameObject);

    }


}