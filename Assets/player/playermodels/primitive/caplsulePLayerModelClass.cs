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
        head.rotation = player.getCamTransformer().rotation;
        RightArm.rotation = player.getCamTransformer().rotation;
        LeftArm.rotation = player.getCamTransformer().rotation;
    }

    

}
