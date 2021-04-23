using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleescript : weaponClass
{

    public string leftAnim;
    public string rightAnim;
    public int distance = 2;
    public float damage = 50;
    public float force = 20;


    // Start is called before the first frame update
    public override void PrimaryAction()
    {


        Debug.Log("left arm");
        punch(leftAnim);



    }


    public override void SecondaryAction()
    {


        Debug.Log("right arm");
        punch(rightAnim);


    }




    void punch (string an)
	{




        anim.Play(an);




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
