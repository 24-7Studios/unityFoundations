using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponClass : NetworkBehaviour, Ipickup
{

    [SyncVar]
    protected PlayerScript player;

    [SyncVar]
    protected bool slot;

    [SerializeField]
    private bool canDual;

    [SyncVar]
    private WeaponClass otherHand;

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




    public Vector3 basePosOffset = new Vector3();

    public Vector3 baseRotOffset = new Vector3();

    public Vector3 baseScaOffset = new Vector3();

    public Vector3 WModelPosOffset = new Vector3();

    public Vector3 WModelRotOffset = new Vector3();

    public Vector3 WModelScaOffset = new Vector3();




    // Start is called before the first frame update
    void Start()
    {
        
        if(player != null)
        {
            pickup(player);
        }
        else
        {

        }


    }

    // Update is called once per frame
    void Update()
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

    public void pickup(PlayerScript p)
    {
        Debug.Log("player has picked up: " + this);
        player = p;
        
        if(!hand)
        {
            if (player.equipedSlotWeapon(false) == null)
            {
                slot = false;
            }
            else if (player.equipedSlotWeapon(true) == null)
            {
                slot = true;
            }
            else
            {
                slot = player.getEquipedSlot();
            }
            player.setSlotWeapon(this, slot);
        }


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

    public void drop()
    {
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
    public bool isDual()
    {
        return otherHand != null;
    }

    public  WeaponClass getOtherHand()
    {
        return otherHand;
    }

    public void setOtherHand(WeaponClass other)
    {
        otherHand = other;
    }

    protected virtual void raycastShoot()
    {

    }


}
