using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : ItemClass
{
    public Transform myWeapon;
    public weaponClass myWeaponClass;
    



    // Start is called before the first frame update
    void Start()
    {


        if (myWeaponClass.isItem)
        {
            hasDropped();
        }

    }

    


    public void hasDropped()
	{

        myWeapon.transform.parent = null;
        itemName = myWeaponClass.weaponName;
        myWeaponClass.enabled = false;

    }
       


}
