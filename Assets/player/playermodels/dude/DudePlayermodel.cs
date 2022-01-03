using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DudePlayermodel : PlayerModelClass
{

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float animationDamping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("x", player.getBasicInputMovement().normalized.x, animationDamping, Time.fixedDeltaTime);
        anim.SetFloat("y", player.getBasicInputMovement().normalized.z, animationDamping, Time.fixedDeltaTime);
    }
}
