using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using Mirror;

public class MapLimit : NetworkBehaviour
{

    public List<Transform> RespawnPoints;



    private void OnTriggerExit(Collider other)
    {


        other.transform.position = RespawnPoints[Random.Range(0, RespawnPoints.Count - 1)].position;


    }


}
