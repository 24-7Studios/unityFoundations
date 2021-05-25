using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    public Animator anim;
    public movement_fly moves;
    public float fallVelocity;
    public float velocity;
    public float damp = 5;

    bool falling = false;
   
    // Update is called once per frame
    void Update()
    {

        velocity = moves.body.transform.InverseTransformDirection(moves.body.velocity).z;


        anim.SetFloat("speed", moves.WalkInfo().y, damp, Time.fixedDeltaTime);

        if(!moves.isGrounded() && moves.body.velocity.y < -fallVelocity)
		{
            falling = true;
		}


        if(falling && moves.isGrounded())
		{
            falling = false;
            anim.SetTrigger("landed");

		}




    }






}
