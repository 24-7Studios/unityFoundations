using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponClass : NetworkBehaviour, Ipickup
{


    [SerializeField]
    protected GameObject item;

    [SerializeField]
    protected GameObject worldModel;

    [SerializeField]
    protected GameObject viewmodel;


    [SerializeField]
    protected bool isitem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void raycastShoot()
    {

    }

    /*
    public GameObject getItemOb()
    {
        return item;
    }
    */
    

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

    public void pickup(PlayerScript p)
    {
        item.SetActive(false);
        isitem = false;
    }

    public void drop()
    {
        transform.SetParent(item.transform);
        item.transform.SetParent(null);
        worldModel.transform.SetParent(transform);
        viewmodel.transform.SetParent(transform);
        viewmodel.SetActive(false);
        worldModel.SetActive(false);
        transform.transform.localPosition = Vector3.zero;
        transform.transform.localRotation = Quaternion.Euler(Vector3.zero);
        item.SetActive(true);
        isitem = true;
    }

}
