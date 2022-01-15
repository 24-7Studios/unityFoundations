using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DudePlayermodel : PlayerModelClass
{

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Transform actualAimTarget;

    [SerializeField]
    private Transform virtualAimTarget;

    [SerializeField]
    private Transform rightArmTarget;

    [SerializeField]
    private Transform leftArmTarget;

    [SerializeField]
    private float animationDamping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void setPlayer(PlayerScript p)
    {
        base.setPlayer(p);

        virtualAimTarget.SetParent(player.getCamTransformer());
        virtualAimTarget.localPosition = Vector3.forward * 5;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("x", player.getBasicInputMovement().normalized.x, animationDamping, Time.fixedDeltaTime);
        anim.SetFloat("y", player.getBasicInputMovement().normalized.z, animationDamping, Time.fixedDeltaTime);

        actualAimTarget.position = virtualAimTarget.position;
        if(player.getEquipedSlot().getWeapon().rightHoldPos != null)
        {
            rightArmTarget.position = player.getEquipedSlot().getWeapon().rightHoldPos.position;
        }

        if (player.getEquipedSlot().getWeapon().leftHoldPos != null)
        {
            leftArmTarget.position = player.getEquipedSlot().getWeapon().leftHoldPos.position;
        }
    }
}
