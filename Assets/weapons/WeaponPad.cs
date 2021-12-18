using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponPad : NetworkBehaviour
{

    public WeaponClass myWeapon;

    public float respawnDelay;

    public float respawnDistance = 3;

    public float stabilizingForce = 2;

    [SerializeField]
    private Collider spawnZone;

    [SerializeField]
    private Transform targetPos;

    private float respawnTimer;

    private GameObject spawnedW;

    private bool counting;


    [ServerCallback]
    void Start()
    {
        StartTimer();
    }

    [ServerCallback]
    void Update()
    {
        if(spawnedW != null && !counting)
        {
            if (Vector3.Distance(spawnedW.transform.position, targetPos.position) > respawnDistance)
            {
                StartTimer();
            }
        }

        if(counting)
        {
            respawnTimer -= Time.fixedDeltaTime;
        }

        if(counting && respawnTimer <= 0)
        {
            spawnObject();
            StopTimer();
        }
    }

    private void FixedUpdate()
    {
        if(spawnedW != null && !counting)
        {
            Rigidbody rb = spawnedW.GetComponent<Rigidbody>();
            Vector3 direction = (spawnedW.transform.position - targetPos.position).normalized;
            float distance = Vector3.Distance(spawnedW.transform.position, targetPos.position);
            float finalForce = stabilizingForce * distance;
            finalForce = Mathf.Clamp(finalForce, 0, stabilizingForce);

            rb.AddForce(-finalForce * direction);
        }
    }

    [ServerCallback]
    public void spawnObject()
    {
        spawnedW = Instantiate(myWeapon.gameObject, targetPos.position, targetPos.rotation);
        NetworkServer.Spawn(spawnedW);
    }

    [ServerCallback]
    private void StartTimer()
    {
        counting = true;
        respawnTimer = respawnDelay;
    }

    [ServerCallback]
    private void StopTimer()
    {
        counting = false;
    }
}
