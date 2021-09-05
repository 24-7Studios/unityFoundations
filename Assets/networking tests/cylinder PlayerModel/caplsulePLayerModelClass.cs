using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caplsulePLayerModelClass : PlayerModelClass
{

    [SerializeField]
    private Transform head;


    private void Start()
    {
        
    }


    void Update()
    {
        head.rotation = player.camTransformer.rotation;
    }

    

}
