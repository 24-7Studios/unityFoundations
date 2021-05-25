using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointtopoint : MonoBehaviour
{

    public Transform source;

    public Vector3 target;

  

    // Update is called once per frame
    void Update()
    {

        source.position = target;
        
    }
}
