using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapLimitEnforcer : MonoBehaviour
{

    public Transform respawn;

    
    void OnTriggerExit(Collider other)
    {
        
            other.transform.position = new Vector3(0, Mathf.Abs(other.transform.position.y), 0);//respawn.position;
        
    }

}
