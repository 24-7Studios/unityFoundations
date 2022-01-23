using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoTenAnimationEvents : MonoBehaviour
{

    [SerializeField]
    private autoTenScript weapon;

    public void magIn()
    {
        weapon.magIn();
    }

    public void magOut()
    {
        weapon.magOut();
    }
}
