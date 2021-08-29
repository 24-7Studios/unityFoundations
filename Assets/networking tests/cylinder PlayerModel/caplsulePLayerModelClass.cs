using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caplsulePLayerModelClass : PlayerModelClass
{

    [SerializeField]
    private Transform visor;


    private void Start()
    {
        visor.SetParent(player.camTransformer);
    }


    void Update()
    {
        
    }
}
