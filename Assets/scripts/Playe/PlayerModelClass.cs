using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerModelClass : NetworkBehaviour
{

    public string ModelName;
    public List<GameObject> models;

    [SerializeField]
    protected Transform RightHoldPos;

    [SerializeField]
    protected Transform LeftHoldPos;

    [SerializeField]
    protected PlayerModelDoll ragdoll;

    protected PlayerScript player;

    public virtual void setPlayer(PlayerScript p)
    {
        player = p;
        PlayerScript.died += onPlayerDeath;
        PlayerScript.spawned += onPlayerSpawn;
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

    protected void onPlayerSpawn(PlayerScript p)
    {

    }

    protected void onPlayerDeath(PlayerScript p)
    {
        if(player == p)
            cmdOnPlayerDeath();
    }

    [Command]
    protected void cmdOnPlayerDeath()
    {
        GameObject doll = Instantiate(ragdoll.gameObject, this.transform.position, this.transform.rotation);
        NetworkServer.Spawn(doll);
        NetworkServer.Destroy(this.gameObject);
    }

}
