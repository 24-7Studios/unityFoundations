using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Mirror;

public abstract class WeaponClass : NetworkBehaviour, Ipickup
{

    [SyncVar]
    protected PlayerScript player;
    
    [SyncVar]
    protected Slot slot;    // this is just to check the index from. Do not acually reference this  for anything else;

    [SyncVar]
    protected int index;

    [SerializeField]
    private bool canDual;

    [SyncVar]
    private bool hand;

    [SyncVar]
    protected bool isitem;

    [SerializeField]
    protected GameObject item;

    [SerializeField]
    protected LayerMask Shootable;

    [SerializeField]
    protected GameObject worldModel;

    [SerializeField]
    protected GameObject viewmodel;

    [SerializeField]
    protected Animator ViewAnim;

    [SerializeField]
    protected AudioSource aud;

    [SerializeField]
    protected bool debugMode;

    [SerializeField]
    protected GameObject debugMarker;

    [SerializeField]
    protected GameObject hitEffect;

    protected List<AudioClip> sounds = new List<AudioClip>();

    protected bool fire1Down;
    protected bool fire2Down;
    protected bool reloadDown;

    public Vector3 basePosOffset = new Vector3();

    public Vector3 baseRotOffset = new Vector3();

    public Vector3 baseScaOffset = new Vector3();

    public Vector3 WModelPosOffset = new Vector3();

    public Vector3 WModelRotOffset = new Vector3();

    public Vector3 WModelScaOffset = new Vector3();




    // Start is called before the first frame update
    protected virtual void Start()
    {

        if (player != null && transform.parent == null)
        {
            forcedPickup(player, index, hand);
        }

    }

    protected virtual void Update()
    {
        
    }

    public GameObject getWorldModelOb()
    {
        return worldModel;
    }

    public GameObject getViewmodelOb()
    {
        return viewmodel;
    }

    public bool isItem()
    {
        return isitem;
    }

    public GameObject getObject()
    {
        return gameObject;
    }

    public bool CanDualWield()
    {
        return canDual;
    }

    public virtual void pickup(PlayerScript p)
    {
        Debug.Log("player has picked up: " + this);

        player = p;

        if(isServer)
            netIdentity.AssignClientAuthority(player.connectionToClient);

        slot = player.getPickupSlot();
        index = slot.getIndex();

        slot.setWeapon(this, hand);

        setControls(hand);

        player.getPlayermodel().equipWeapon(this, hand);

        foreach (MeshRenderer m in item.GetComponents<MeshRenderer>())
        {
            m.enabled = false;
        }

        foreach (Collider c in item.GetComponents<Collider>())
        {
            c.enabled = false;
        }

        item.GetComponent<Rigidbody>().isKinematic = true;

        isitem = false;

        transform.SetParent(player.getBackpack(), false);
        //transform.localPosition = basePosOffset;
        //transform.localRotation = Quaternion.Euler(baseRotOffset);
        //transform.localScale = baseScaOffset;
        viewmodel.transform.SetParent(player.getViewmodelHolder(), false);
        viewmodel.transform.localPosition = Vector3.zero;


        if(player.isLocalPlayer)
        {
            viewmodel.SetActive(true);
            viewmodel.layer = 11;
            worldModel.SetActive(true);
            worldModel.layer = 6;
        }
        else
        {
            viewmodel.SetActive(false);
            worldModel.SetActive(true);
            worldModel.layer = 0;
        }

        if(slot != player.getEquipedSlot() && player.isLocalPlayer)
        {
            player.changeSlot();
        }

    }

    public virtual void forcedPickup(PlayerScript p, int s, bool h)
    {

        Debug.Log("player has picked up: " + this);


        player = p;

        if (isServer)
            netIdentity.AssignClientAuthority(player.connectionToClient);

        hand = h;

        index = s;

        player.getSlotAtIndex(s).setWeapon(this, hand);

        setControls(hand);

        player.getPlayermodel().equipWeapon(this, hand);

        foreach (MeshRenderer m in item.GetComponents<MeshRenderer>())
        {
            m.enabled = false;
        }

        foreach (Collider c in item.GetComponents<Collider>())
        {
            c.enabled = false;
        }

        item.GetComponent<Rigidbody>().isKinematic = true;

        isitem = false;

        transform.SetParent(player.getBackpack());
        transform.localPosition = basePosOffset;
        transform.localRotation = Quaternion.Euler(baseRotOffset);
        transform.localScale = baseScaOffset;
        viewmodel.transform.SetParent(player.getViewmodelHolder());
        viewmodel.transform.localPosition = Vector3.zero;


        if (player.isLocalPlayer)
        {
            viewmodel.SetActive(true);
            viewmodel.layer = 11;
            worldModel.SetActive(true);
            worldModel.layer = 6;
        }
        else
        {
            viewmodel.SetActive(false);
            worldModel.SetActive(true);
            worldModel.layer = 0;
        }

    }

    public virtual void drop()
    {

        unsetControls(hand);

        if(isServer)
            netIdentity.RemoveClientAuthority();

        item.transform.SetParent(null);
        worldModel.transform.SetParent(transform);
        viewmodel.transform.SetParent(transform);
        viewmodel.SetActive(false);
        worldModel.SetActive(false);

        player.getSlotAtIndex(index).removeWeapon(hand);

        foreach (MeshRenderer m in item.GetComponents<MeshRenderer>())
        {
            m.enabled = true;
        }

        foreach (Collider c in item.GetComponents<Collider>())
        {
            c.enabled = true;
        }

        item.GetComponent<Rigidbody>().isKinematic = false;

        player = null;

        slot = null;

        index = -1;

        hand = false;

        isitem = true;
    }

    public bool getHand()
    {
        return hand;
    }

    public void setHand(bool h)
    {
        hand = h;
    }

    protected virtual void setControls(bool hand)
    {
        if(!hand)
        {
            player.getInputMaster().Player.Fire_1.performed += ctx => { fire1Down = true; };
            player.getInputMaster().Player.Fire_1.canceled += ctx => { fire1Down = false; };
            player.getInputMaster().Player.Fire_2Zoom1.performed += ctx => { fire2Down = true; };
            player.getInputMaster().Player.Fire_2Zoom1.canceled += ctx => { fire2Down = false; };
            player.getInputMaster().Player.reload.performed += ctx => { reloadDown = true; };
            player.getInputMaster().Player.reload.canceled += ctx => { reloadDown = false; };
        }
        else
        {
            player.getInputMaster().Player.Fire_2Zoom1.performed += ctx => { fire1Down = true; };
            player.getInputMaster().Player.Fire_2Zoom1.canceled += ctx => { fire1Down = false; };
            player.getInputMaster().Player.reload2.performed += ctx => { reloadDown = true; };
            player.getInputMaster().Player.reload2.canceled += ctx => { reloadDown = false; };
        }
    }

    protected virtual void unsetControls(bool hand)
    {
        if (!hand)
        {
            player.getInputMaster().Player.Fire_1.performed -= ctx => { fire1Down = true; };
            player.getInputMaster().Player.Fire_1.canceled -= ctx => { fire1Down = false; };
            player.getInputMaster().Player.Fire_2Zoom1.performed -= ctx => { fire2Down = true; };
            player.getInputMaster().Player.Fire_2Zoom1.canceled -= ctx => { fire2Down = false; };
            player.getInputMaster().Player.reload.performed -= ctx => { reloadDown = true; };
            player.getInputMaster().Player.reload.canceled -= ctx => { reloadDown = false; };
        }
        else
        {
            player.getInputMaster().Player.Fire_2Zoom1.performed -= ctx => { fire1Down = true; };
            player.getInputMaster().Player.Fire_2Zoom1.canceled -= ctx => { fire1Down = false; };
            player.getInputMaster().Player.reload2.performed -= ctx => { reloadDown = true; };
            player.getInputMaster().Player.reload2.canceled -= ctx => { reloadDown = false; };
        }
    }


    ///////////////////////////////////////////////
    /// common use functions
    /// 


    protected virtual void playsound(int index)
    {
        AudioClip clip = sounds[index];

        aud.PlayOneShot(clip);

        if(!isServer)
        {
            cmdPlaySound(index);
        }
        else
        {
            rpcPlaySound(index);
        }
    }

    [Command]
    protected virtual void cmdPlaySound(int index)
    {
        rpcPlaySound(index);
    }

    [ClientRpc]
    protected virtual void rpcPlaySound(int index)
    {

        AudioClip clip = sounds[index];

        if (player != null && !player.isLocalPlayer)
        {
            aud.PlayOneShot(clip);
        }
        else if(player == null)
        {
            aud.PlayOneShot(clip);
        }
    }

    protected virtual void raycastShoot(float baseDamage, float multiplier, Vector3 position, Vector3 direction, LayerMask Shootable)
    {
        RaycastHit hit;

        if (Physics.Raycast(position, direction, out hit, Mathf.Infinity, Shootable))
        {

            IDamage iD = hit.collider.GetComponent<IDamage>();
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (iD != null)
            {
                cmdHitDamageable(hit.collider.gameObject, baseDamage, multiplier);
            }

            if (rb != null)
            {
                rb.AddForceAtPosition(direction, hit.point, ForceMode.Impulse);
                pushObject(hit.collider.gameObject, direction, hit.point);
            }

            if(hitEffect != null)
            {
                Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

            if (iD == null && rb == null)
            {
                if(debugMode)
                {
                    if(debugMarker != null)
                    {
                        Instantiate(debugMarker, hit.point, Quaternion.Euler(hit.normal));
                    }
                }
            }

        }
    }

    protected virtual void smack(float damage, float multiplier, float radius, LayerMask shootable)
    {

    }

    [Command]
    protected virtual void cmdHitDamageable(GameObject thing, float damage, float fleshMultiplier)
    {
        IDamage id = thing.GetComponent<IDamage>();

        id.takeDamagefromHit(damage, fleshMultiplier);
    }

    [Command]
    protected virtual void pushObject(GameObject thing, Vector3 Direction, Vector3 Position)
    {
        Rigidbody rb = thing.GetComponent<Rigidbody>();

        rb.AddForceAtPosition(Direction, Position, ForceMode.Impulse);
    }

}
