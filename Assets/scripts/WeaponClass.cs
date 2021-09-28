using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponClass : NetworkBehaviour, Ipickup
{

    [SyncVar]
    public PlayerScript player;

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


    [SerializeField]
    private bool canDual;

    [SyncVar]
    private WeaponClass otherHand;

    [SyncVar]
    public bool hand;

    [SyncVar]
    protected bool isitem;

    // Start is called before the first frame update
    void Start()
    {
        
        if(player != null)
        {
            player.pickupWeapon(gameObject, hand);
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

        player = p;

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


    public  WeaponClass getOtherHand()
    {
        return otherHand;
    }

    public void setOtherHand(WeaponClass other)
    {
        otherHand = other;
    }

    public bool isDual()
    {
        return otherHand != null;
    }

    protected virtual void raycastShoot()
    {

    }


}
