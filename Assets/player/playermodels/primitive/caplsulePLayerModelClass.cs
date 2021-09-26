using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caplsulePLayerModelClass : PlayerModelClass
{

    [SerializeField]
    private Transform head;

    [SerializeField]
    private Transform RightArm;

    [SerializeField]
    private Transform LeftArm;


    private void Start()
    {
        
    }


    void Update()
    {
        head.rotation = player.camTransformer.rotation;
        RightArm.rotation = player.camTransformer.rotation;
        LeftArm.rotation = player.camTransformer.rotation;
    }

    

}
