using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
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
    private Transform rightHand;

    [SerializeField]
    private Transform rightArmTarget;

    [SerializeField]
    private ChainIKConstraint rightChainConstraint;

    [SerializeField]
    private MultiAimConstraint rightAimConstriant;

    [SerializeField]
    private Transform leftHand;

    [SerializeField]
    private Transform leftArmTarget;

    [SerializeField]
    private ChainIKConstraint leftChainConstraint;

    [SerializeField]
    private MultiAimConstraint leftAimConstraint;

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


        if(player.getEquipedSlot().getWeapon().leftHoldPos != null && player.getEquipedSlot().getWeapon().rightHoldPos != null)
        {
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 1);
            rightAimConstriant.weight = 1;
            leftAimConstraint.weight = 0;
            rightChainConstraint.weight = 0;
            leftChainConstraint.weight = 1;
            rightArmTarget.position = player.getEquipedSlot().getWeapon().rightHoldPos.position;
            leftArmTarget.position = player.getEquipedSlot().getWeapon().leftHoldPos.position;
        }
        else if(player.getEquipedSlot().getWeapon().rightHoldPos != null)
        {
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            rightAimConstriant.weight = 1;
            leftAimConstraint.weight = 0;
            rightChainConstraint.weight = 0;
            leftChainConstraint.weight = 0;
            rightArmTarget.position = player.getEquipedSlot().getWeapon().rightHoldPos.position;
            leftArmTarget.position = leftHand.position;
        }
        else
        {
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            rightAimConstriant.weight = 0;
            leftAimConstraint.weight = 0;
            rightChainConstraint.weight = 0;
            leftChainConstraint.weight = 0;
            rightArmTarget.position = rightHand.position;
            leftArmTarget.position = leftHand.position;
        }

    }
}
