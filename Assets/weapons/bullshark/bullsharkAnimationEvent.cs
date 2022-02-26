using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullsharkAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private bullshark weapon;

    public void magIn()
    {
        weapon.magIn();
    }

    public void magOut()
    {
        weapon.magOut();
    }
}
