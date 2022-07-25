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
    
    protected Slot slot;    // this is just to check the index from. Do not acually reference this  for anything else;

    [SyncVar]
    protected int index;

    [SerializeField]
    private bool canDual;

    [SerializeField, SyncVar]
    protected bool isInDual;

    [SyncVar]
    private bool hand;

    [SyncVar]
    private bool isEquiped;

    [SyncVar]
    protected bool isitem;

    [SerializeField]
    protected GameObject item;

    [SerializeField]
    protected LayerMask Shootable;

    [SerializeField]
    protected GameObject worldModel;

    [SerializeField]
    public Transform rightHoldPos;

    [SerializeField]
    public Transform leftHoldPos;

    [SerializeField]
    protected GameObject viewmodel;

    [SerializeField]
    protected SkinnedMeshRenderer armsMesh;

    [SerializeField]
    protected Animator ViewAnim;

    [SerializeField]
    protected string drawAnim;

    [SerializeField]
    protected RuntimeAnimatorController defaultAnims;

    [SerializeField]
    protected AnimatorOverrideController rightHandAnims;

    [SerializeField]
    protected AnimatorOverrideController leftHandAnims;

    [SerializeField]
    protected AudioSource aud;

    [SerializeField]
    protected bool debugMode;

    [SerializeField]
    protected GameObject debugMarker;

    [SerializeField]
    protected GameObject hitEffect;

    [SerializeField]
    protected float pushBack = 0;

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
        PlayerScript.died += onPlayerDeath;
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

        Debug.Log(player);
        
        slot = player.getSlotAtIndex(index);

        slot.setWeapon(this, hand);

        if (slot.getWeapon() != null) slot.getWeapon().setDualStatus();
        if (slot.getOtherHand() != null) slot.getOtherHand().setDualStatus();

        player.getLivePlayerModel().equipWeapon(this, hand);

        foreach (MeshRenderer m in item.GetComponents<MeshRenderer>())
        {
            m.enabled = false;
        }

        foreach (Collider c in item.GetComponents<Collider>())
        {
            c.enabled = false;
        }

        item.GetComponent<Rigidbody>().isKinematic = true;
        transform.SetParent(player.getBackpack(), false);
        transform.localPosition = basePosOffset;
        transform.localRotation = Quaternion.Euler(baseRotOffset);
        transform.localScale = baseScaOffset;
        viewmodel.transform.SetParent(player.getViewmodelHolder(), true);
        viewmodel.transform.localPosition = Vector3.zero;
        viewmodel.transform.localRotation = Quaternion.identity;

        if (player.isLocalPlayer)
        {
            viewmodel.layer = 11;
            worldModel.layer = 6;
            if (slot != player.getEquipedSlot())
            {
                player.changeSlot();
            }
            else
            {
                player.syncSlots();
            }
        }
        else
        {
            worldModel.layer = 0;
        }
        
    }


    public virtual void pickup(PlayerScript p, bool h)
    {

    }

    [Server]
    public  void serverPickup(PlayerScript p)
    {
        player = p;
        index = player.getPickupSlot().getIndex();
        hand = false;
        netIdentity.AssignClientAuthority(p.connectionToClient);
        isitem = false;
        clientPickup(player, index, hand, isitem);
    }

    [Server]
    public void serverPickup(PlayerScript p, bool h)
    {
        player = p;
        index = player.getEquipedSlot().getIndex();
        hand = h;
        netIdentity.AssignClientAuthority(p.connectionToClient);
        isitem = false;

        clientPickup(player, index, hand, isitem);
    }

    [ClientRpc]
    public void clientPickup(PlayerScript p, int i, bool h, bool b)
    {
        player = p;
        index = i;
        hand = h;
        isitem = b;
    }

    public virtual void forcedPickup(PlayerScript p, int s, bool h)
    {

        Debug.Log("player has picked up: " + this);


        player = p;

        if (isServer)
            netIdentity.AssignClientAuthority(player.connectionToClient);

        hand = h;

        index = s;

        slot = player.getSlotAtIndex(s);

        player.getSlotAtIndex(s).setWeapon(this, hand);

        setDualStatus();

        if (player.isLocalPlayer)
        {
            //setControls(hand);
        }

        player.getLivePlayerModel().equipWeapon(this, hand);

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

        player.reSyncSlots();

        onEquip();

    }

    public virtual void drop()
    {

        WeaponClass otherHand;
        if(hand)
        {
            otherHand = slot.getWeapon();
        }
        else
        {
            otherHand = slot.getOtherHand();
        }

        if(player != null)
        {
            if (player.isLocalPlayer)
            {
                unsetControls(hand);
            }
        }

        slot.removeWeapon(hand);

        item.transform.SetParent(null);
        worldModel.transform.SetParent(transform);
        viewmodel.transform.SetParent(transform);
        viewmodel.SetActive(false);
        worldModel.SetActive(false);

        foreach (MeshRenderer m in item.GetComponents<MeshRenderer>())
        {
            m.enabled = true;
        }

        foreach (Collider c in item.GetComponents<Collider>())
        {
            c.enabled = true;
        }

        item.GetComponent<Rigidbody>().isKinematic = false;

        slot = null;

        index = -1;

        hand = false;

        isitem = true;

        

        if (isServer)
            netIdentity.RemoveClientAuthority();

        if (otherHand != null) otherHand.setDualStatus();


        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = player.getVelocity() + player.transform.forward * 1;
        }

        player = null;
    }


    [Server]
    public void serverDrop()
    {

    }

    public PlayerScript getPlayer()
    {
        return player;
    }

    public bool getHand()
    {
        return hand;
    }

    public void setHand(bool h)
    {
        hand = h;
    }

    public void setDualStatus()
    {
        isInDual = false;
        if(hand) isInDual = true;
        if (slot.getOtherHand() != null) isInDual = true;
        if (player.isLocalPlayer) { unsetControls(hand); setControls(hand); }
    }

    protected virtual void setControls(bool h)
    {
        if(isInDual)
        {
            if (h)
            {
                player.getInputMaster().Player.Fire_1.performed += Fire1Down;
                player.getInputMaster().Player.Fire_1.canceled += Fire1Up;
                player.getInputMaster().Player.reload2.performed += ReloadDown;
                player.getInputMaster().Player.reload2.canceled += ReloadUp;
            }
            else
            {
                player.getInputMaster().Player.Fire_2Zoom1.performed += Fire1Down;
                player.getInputMaster().Player.Fire_2Zoom1.canceled += Fire1Up;
                player.getInputMaster().Player.reload.performed += ReloadDown;
                player.getInputMaster().Player.reload.canceled += ReloadUp;
            }
        }
        else
        {
            player.getInputMaster().Player.Fire_1.performed += Fire1Down;
            player.getInputMaster().Player.Fire_1.canceled += Fire1Up;
            player.getInputMaster().Player.Fire_2Zoom1.performed += Fire2Down;
            player.getInputMaster().Player.Fire_2Zoom1.canceled += Fire2Up;
            player.getInputMaster().Player.reload.performed += ReloadDown;
            player.getInputMaster().Player.reload.canceled += ReloadUp;
        }
    }

    protected virtual void unsetControls(bool hand)
    {
        if (!hand)
        {
            player.getInputMaster().Player.Fire_1.performed -= Fire1Down;
            player.getInputMaster().Player.Fire_1.canceled -= Fire1Up;
            player.getInputMaster().Player.Fire_2Zoom1.performed -= Fire2Down;
            player.getInputMaster().Player.Fire_2Zoom1.canceled -= Fire2Up;
            player.getInputMaster().Player.reload.performed -= ReloadDown;
            player.getInputMaster().Player.reload.canceled -= ReloadUp;
        }
        else
        {
            player.getInputMaster().Player.Fire_2Zoom1.performed -= Fire1Down;
            player.getInputMaster().Player.Fire_2Zoom1.canceled -= Fire1Up;
            player.getInputMaster().Player.reload2.performed -= ReloadDown;
            player.getInputMaster().Player.reload2.canceled -= ReloadUp;
        }
    }

    public virtual void onEquip()
    {
        isEquiped = true;
        if(player.getLivePlayerModel().getArms() != null && armsMesh != null)
        {
            SkinnedMeshRenderer arms = player.getLivePlayerModel().getArms();
            armsMesh.sharedMesh = arms.sharedMesh;
            armsMesh.materials = arms.materials;
        }

        worldModel.SetActive(true);
        player.getLivePlayerModel().equipWeapon(this, hand);
        if (player.isLocalPlayer)
        {

            if (isInDual)
            {
                if (!hand)
                {
                    if (rightHandAnims != null)
                    {
                        ViewAnim.runtimeAnimatorController = rightHandAnims;
                        unMirror();
                    }
                    else
                    {
                        if (leftHandAnims != null)
                        {
                            ViewAnim.runtimeAnimatorController = leftHandAnims;
                            mirror();
                        }
                        else
                        {
                            unMirror();
                        }
                    }

                }
                else
                {
                    if (leftHandAnims != null)
                    {
                        ViewAnim.runtimeAnimatorController = leftHandAnims;
                        unMirror();
                    }
                    else
                    {
                        if(rightHandAnims != null)
                        {
                            ViewAnim.runtimeAnimatorController = rightHandAnims;
                            mirror();
                        } 
                    }
                }
            }
            else
            {
                if (defaultAnims != null)
                {
                    ViewAnim.runtimeAnimatorController = defaultAnims;
                }
                unMirror();
            }
            viewmodel.SetActive(true);
            if (drawAnim != null) ViewAnim.Play(drawAnim);
        }
    }

    private void mirror()
    {
        viewmodel.transform.localScale = new Vector3(-Mathf.Abs(viewmodel.transform.localScale.x), viewmodel.transform.localScale.y, viewmodel.transform.localScale.z);
    }

    private void unMirror()
    {
        viewmodel.transform.localScale = new Vector3(Mathf.Abs(viewmodel.transform.localScale.x), viewmodel.transform.localScale.y, viewmodel.transform.localScale.z);
    }

    public virtual void onDequip()
    {

        isEquiped = false;

        worldModel.SetActive(false);

        if (player.isLocalPlayer)
        {
            viewmodel.SetActive(false);
        }
    }

    public virtual void onPlayerDeath(PlayerScript p)
    {
        if(p == player)
        {
            worldModel.SetActive(false);
            worldModel.transform.SetParent(transform);
        }
    }

    protected virtual void Fire1Down(InputAction.CallbackContext context)
    {
        fire1Down = true;
    }

    protected virtual void Fire1Up(InputAction.CallbackContext context)
    {
        fire1Down = false;
    }

    protected virtual void Fire2Down(InputAction.CallbackContext context)
    {
        fire2Down = true;
    }

    protected virtual void Fire2Up(InputAction.CallbackContext context)
    {
        fire2Down = false;
    }

    protected virtual void ReloadDown(InputAction.CallbackContext context)
    {
        reloadDown = true;
    }

    protected virtual void ReloadUp(InputAction.CallbackContext context)
    {
        reloadDown = false;
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
            NetworkIdentity netId = hit.collider.GetComponent<NetworkIdentity>();
            hitbox hb = hit.collider.GetComponent<hitbox>();
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            
            if(hb != null && hb.enabled)
            {
                if(hb.getObject() != null)
                {
                    cmdHitDamageable(hb.getObject(), baseDamage * hb.getMultiplier(), multiplier);
                    netId = hb.getObject().GetComponent<NetworkIdentity>();
                    rb = hb.getObject().GetComponent<Rigidbody>();
                }
            }
            else if (iD != null)
            {
                cmdHitDamageable(hit.collider.gameObject, baseDamage, multiplier);
            }

            if (rb != null)
            {
                if(netId != null)
                {
                    cmdPushObject(netId.gameObject, direction, hit.point);
                }
                else
                {
                    rb.AddForceAtPosition(direction, hit.point, ForceMode.Impulse);
                }
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


    protected virtual void raycastMShoot(float baseDamage, float multiplier, Vector3 position, float radius, Vector3 direction, LayerMask Shootable)
    {
        RaycastHit hit;

        if (Physics.SphereCast(position, radius, direction, out hit, Mathf.Infinity, Shootable))
        {

            IDamage iD = hit.collider.GetComponent<IDamage>();
            NetworkIdentity netId = hit.collider.GetComponent<NetworkIdentity>();
            hitbox hb = hit.collider.GetComponent<hitbox>();
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (hb != null && hb.enabled)
            {
                if (hb.getObject() != null)
                {
                    cmdHitDamageable(hb.getObject(), baseDamage * hb.getMultiplier(), multiplier);
                    netId = hb.getObject().GetComponent<NetworkIdentity>();
                    rb = hb.getObject().GetComponent<Rigidbody>();
                }
            }
            else if (iD != null)
            {
                cmdHitDamageable(hit.collider.gameObject, baseDamage, multiplier);
            }

            if (rb != null)
            {
                if (netId != null)
                {
                    cmdPushObject(netId.gameObject, direction, hit.point);
                }
                else
                {
                    rb.AddForceAtPosition(direction, hit.point, ForceMode.Impulse);
                }
            }

            if (hitEffect != null)
            {
                Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

            if (iD == null && rb == null)
            {
                if (debugMode)
                {
                    if (debugMarker != null)
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

        if(id != null)
        {
            id.takeDamagefromHit(damage, fleshMultiplier);
        }
    }

    [Command]
    protected virtual void cmdPushObject(GameObject thing, Vector3 Direction, Vector3 Position)
    {
        Rigidbody rb = thing.GetComponent<Rigidbody>();

        rpcPushObject(thing, Direction, Position);
    }

    [ClientRpc]
    protected virtual void rpcPushObject(GameObject thing, Vector3 Direction, Vector3 Position)
    {
        Rigidbody rb = thing.GetComponent<Rigidbody>();

        rb.AddForceAtPosition(Direction, Position, ForceMode.Impulse);
    }
}
