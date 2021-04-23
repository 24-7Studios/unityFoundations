using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapLimitEnforcer : MonoBehaviour
{

    public Transform respawn;

    
    void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<PlayerWeaponFunctions>())
        {
            other.transform.position = respawn.position;
        }
    }

}
