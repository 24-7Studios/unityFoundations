using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    public Player player;
    public Animator anim;
    public float fallVelocity;
    public float velocity;
    public float damp = 5;

    bool falling = false;


	private void Start()
	{

        player = GetComponent<Player>();


    }




	void Update()
    {

        velocity = player.playerBody.transform.InverseTransformDirection(player.playerBody.velocity).z;


        anim.SetFloat("speed", player.mover.WalkInfo().y, damp, Time.fixedDeltaTime);

        if(!player.mover.isGrounded() && player.playerBody.velocity.y < -fallVelocity)
		{
            falling = true;
		}


        if(falling && player.mover.isGrounded())
		{
            falling = false;
            anim.SetTrigger("landed");

		}




    }






}
