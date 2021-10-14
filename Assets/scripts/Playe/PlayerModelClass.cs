using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerModelClass : NetworkBehaviour
{

    public string ModelName;
    public GameObject baseCont;
    public GameObject ModelPrefab;
    public List<GameObject> models;

    [SerializeField]
    protected Transform RightHoldPos;

    [SerializeField]
    protected Transform LeftHoldPos;

    protected PlayerScript player;

    public virtual void setPlayer(PlayerScript p)
    {
        player = p;
        Debug.Log("player Set!" + player);
    }

    public virtual void equipWeapon(WeaponClass w, bool hand)
    {

        Transform targetPos;

        if(hand)
        {
            targetPos = LeftHoldPos;
        }
        else
        {
            targetPos = RightHoldPos;
        }

        GameObject wm = w.getWorldModelOb();

        wm.transform.SetParent(targetPos, false);
        wm.transform.localPosition = w.WModelPosOffset;
        wm.transform.localRotation = Quaternion.Euler(w.WModelRotOffset);

        wm.transform.SetParent(null, true);
        wm.transform.localScale = w.WModelScaOffset;
        wm.transform.SetParent(targetPos, true);

    }

}
