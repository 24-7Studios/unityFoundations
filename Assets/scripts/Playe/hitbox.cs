using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class hitbox : NetworkBehaviour, IDamage
{
    [SerializeField]
    private IDamage mainObject;

    public float hitMultiplier;

    public void die()
    {
        mainObject.die();
    }

    public void takeDamagefromHit(float baseDamage, float fleshMulitplier)
    {
        mainObject.takeDamagefromHit(baseDamage * hitMultiplier, fleshMulitplier);
    }

    public void hit()
    {
        mainObject.hit();
    }

    public void setObject(IDamage i)
    {
        mainObject = i;
    }
}
