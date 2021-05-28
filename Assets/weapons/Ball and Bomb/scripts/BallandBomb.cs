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

        clone = Instantiate(Throw, player.camTransformer.transform.position + player.camTransformer.transform.forward * 1, player.camTransformer.rotation);

        clone.velocity = (player.camTransformer.forward * power + player.playerBody.velocity);





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
