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

    [SyncVar]
    protected PlayerScript player;


    protected virtual void Start()
    {
        if(player != null)
        {
            player.clientApplyPlayermodel(gameObject);
        }
    }

    public virtual void setPlayer(PlayerScript p)
    {
        player = p;

        if (!player.isLocalPlayer)
        {
            foreach (hitbox part in hitBoxes)
            {
                part.gameObject.layer = 7;
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
                part.layer = 7;
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
        player.clearLivePlayermodel();
        transform.parent = null;
        player = null;

        foreach (GameObject part in models)
        {
            part.layer = 0;
        }

        foreach (hitbox hitB in hitBoxes)
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

        wm.transform.SetParent(targetPos, true);
        Vector3 aScale = wm.transform.localScale;
        wm.transform.SetParent(null, true);

        wm.transform.SetParent(targetPos, false);
        wm.transform.localPosition = w.WModelPosOffset;
        wm.transform.localRotation = Quaternion.Euler(w.WModelRotOffset);
        wm.transform.localScale = aScale;
    }

    protected void onPlayerSpawn(PlayerScript p)
    {

    }

    protected virtual void onPlayerDeath(PlayerScript p)
    {
        if(player == p)
        {
            unsetPlayer();
        }
    }


}
