using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerModelDoll : NetworkBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private PlayerModelClass myModel;

}
