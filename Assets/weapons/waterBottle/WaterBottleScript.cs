using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBottleScript : weaponClass
{
    public string useWater;
    public string waterIdle;




    public override void Update()
    {
        base.Update();
        if(InputFire1Down)
        {
            PrimaryAction();
        }
    }
    public override void PrimaryAction()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(waterIdle))
        {
            anim.Play(useWater);
        }
    }





}
