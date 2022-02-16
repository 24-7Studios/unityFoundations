using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerModelClass : NetworkBehaviour
{

    public string ModelName;
    public List<GameObject> models;
    public List<hitbox> hitBoxes;

    [SerializeField]
    public Transform CameraOffset;

    [SerializeField]
    protected Transform RightHoldParent;

    [SerializeField]
    protected Transform LeftHoldParent;

    protected PlayerScript player;

    public virtual void setPlayer(PlayerScript p)
    {
        player = p;

        if (!player.isLocalPlayer)
        {
            foreach (hitbox part in hitBoxes)
            {
                part.gameObject.layer = 0;
            }
        }
        else
        {
            foreach (hitbox part in hitBoxes)
            {
                part.gameObject.layer = 6;
            }
        }

        if (!player.isLocalPlayer)
        {
            foreach (GameObject part in models)
            {
                part.layer = 0;
            }
        }

        if (player.isLocalPlayer)
        {
            foreach (GameObject part in models)
            {
                part.layer = 6;
            }
        }

        foreach (hitbox hitB in hitBoxes)
        {
            hitB.setObject(player.gameObject);
        }

        PlayerScript.died += onPlayerDeath;
        PlayerScript.spawned += onPlayerSpawn;
    }

    public virtual void unsetPlayer()
    {
        transform.parent = null;
        player = null;


        foreach(hitbox hitB in hitBoxes)
        {
            hitB.setObject(null);
            hitB.enabled = false;
        }
    }

    public virtual void equipWeapon(WeaponClass w, bool hand)
    {

        Transform targetPos;

        if (hand)
        {
            targetPos = LeftHoldParent;
        }
        else
        {
            targetPos = RightHoldParent;
        }

        GameObject wm = w.getWorldModelOb();

        wm.transform.SetParent(targetPos, false);
        wm.transform.localPosition = w.WModelPosOffset;
        wm.transform.localRotation = Quaternion.Euler(w.WModelRotOffset);

        wm.transform.SetParent(null, true);
        wm.transform.localScale = w.WModelScaOffset/transform.localScale.y;
        wm.transform.SetParent(targetPos, true);

    }

    protected void onPlayerSpawn(PlayerScript p)
    {

    }

    protected void onPlayerDeath(PlayerScript p)
    {
        if(player == p)
        {
            unsetPlayer();
        }
    }


}
