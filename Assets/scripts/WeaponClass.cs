using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class WeaponClass : NetworkBehaviour, Ipickup
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
    protected GameObject worldModel;

    [SerializeField]
    protected GameObject viewmodel;

    [SerializeField]
    protected NetworkAnimator anim;

    [SerializeField]
    protected AudioSource aud;


    public Vector3 basePosOffset = new Vector3();

    public Vector3 baseRotOffset = new Vector3();

    public Vector3 baseScaOffset = new Vector3();

    public Vector3 WModelPosOffset = new Vector3();

    public Vector3 WModelRotOffset = new Vector3();

    public Vector3 WModelScaOffset = new Vector3();




    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        if(player != null && transform.parent == null)
        {
            forcedPickup(player, index, hand);
        }

    }

    // Update is called once per frame
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

        slot = player.getEquipedSlot();
        slot.getIndex();

        slot.setWeapon(this, hand);

        setControls(hand);

        player.PlayerModel.equipWeapon(this, hand);

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

    }

    public virtual void forcedPickup(PlayerScript p, int s, bool h)
    {

        Debug.Log("player has picked up: " + this);


        player = p;

        hand = h;

        index = s;

        player.getSlotAtIndex(s).setWeapon(this, hand);

        setControls(hand);

        player.PlayerModel.equipWeapon(this, hand);

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

        item.transform.SetParent(null);
        worldModel.transform.SetParent(transform);
        viewmodel.transform.SetParent(transform);
        viewmodel.SetActive(false);
        worldModel.SetActive(false);

        hand = false;

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
            player.getInputMaster().Player.Fire_1.performed += ctx => onFire(ctx);
            player.getInputMaster().Player.Fire_2Zoom1.performed += ctx => onAltFire(ctx);
            //player.getInputMaster().Player
        }
        else
        {
            player.getInputMaster().Player.Fire_2Zoom1.performed += ctx => onFire(ctx);
        }
    }

    protected virtual void unsetControls(bool hand)
    {
        if (!hand)
        {
            player.getInputMaster().Player.Fire_1.performed -= ctx => onFire(ctx);
            player.getInputMaster().Player.Fire_2Zoom1.performed -= ctx => onAltFire(ctx);
        }
        else
        {
            player.getInputMaster().Player.Fire_2Zoom1.performed -= ctx => onFire(ctx);
        }
    }

    protected virtual void onFire(InputAction.CallbackContext ctx)
    {
        
    }

    protected virtual void onAltFire(InputAction.CallbackContext ctx)
    {

    }

    protected virtual void reload(InputAction.CallbackContext ctx)
    {

    }


}
