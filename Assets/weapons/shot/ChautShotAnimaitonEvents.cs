using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChautShotAnimaitonEvents : MonoBehaviour
{

    [SerializeField]
    public ChautShot myWeapon;

    public void open()
    {
        myWeapon.open();
    }

    public void close()
    {
        myWeapon.close();
    }

    public void magIn()
    {
        myWeapon.magIn();
    }

    public void magOut()
    {
        myWeapon.magOut();
    }
}
