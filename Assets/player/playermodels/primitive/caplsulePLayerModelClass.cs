using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caplsulePLayerModelClass : PlayerModelClass
{

    [SerializeField]
    private Transform head;

    [SerializeField]
    private Transform arm;



    private void Start()
    {
        
    }


    void Update()
    {
        head.rotation = player.camTransformer.rotation;
        arm.rotation = player.camTransformer.rotation;
    }

    

}
