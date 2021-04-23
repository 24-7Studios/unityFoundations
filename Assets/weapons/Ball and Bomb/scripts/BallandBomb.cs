using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallandBomb : weaponClass
{

    public string leftAnim;
    public string rightAnim;
    public string type = "primary";
    public Rigidbody Ball;
    public Rigidbody Bomb;
    public float power = 5;

    




    public override void PrimaryAction()
    {

        fire(Ball, leftAnim);



    }


    public override void SecondaryAction()
    {


        fire(Bomb, rightAnim);

    }





    void fire(Rigidbody Throw, string A)
    {





        Rigidbody clone;

        anim.Play(A);

        clone = Instantiate(Throw, cam.transform.position + cam.transform.forward * 1, cam.rotation);

        clone.velocity = (cam.forward * power + body.velocity);





    }

    public override void Update()
    {
        base.Update();

        if (InputFire1Down)
        {
            PrimaryAction();
        }
        if (InputFire2Down)
        {
            SecondaryAction();
        }


    }



    }
