using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Experimental;

public class movement_fly : NetworkRigidbody
{

    public Player player;
    public float moveSpeed = 2;
    public float jumpForce = 5;
    public float sprintMultiplyer = 2;
    public float playerGravity = 9.86f;




    public Transform groundCheck;
    public Transform foot;
    public Vector3 footPostition;
    public LayerMask Jumpable;
    public float groundDistance = 0.1f;
    public float groundingForce = 0.05f;
    public float maxAngle = 50;
    public bool fly = false;
    public bool usePhysicsGravity = false;



    Vector3 InputMovement;
    bool grounded;
    bool canJump;
    bool jump = false;
    float x = 0;
    float z = 0;
    float y = 0;
    Vector3 groundNormal;


    // Start is called before the first frame update
    [Server]
    void Start()
    {

        player = GetComponent<Player>();

        if (usePhysicsGravity)
		{
            playerGravity = Physics.gravity.y;
		}

        footPostition = foot.localPosition;


        
    }

    // Update is called once per frame
    [Client ]
    void Update()
    {
        

        if(!isLocalPlayer)
        {
            return;
        }

        grounded = isGrounded();
        canJump = CanJump();
        
         x = Input.GetAxisRaw("Horizontal");
         z = Input.GetAxisRaw("Vertical");

        RaycastHit FloorSnap;

        if (Physics.Raycast(groundCheck.position, -groundCheck.up, out FloorSnap) && ((FloorSnap.normal.x! < maxAngle) || (FloorSnap.normal.y! < maxAngle)) && grounded)
        {

            Quaternion toRotation = Quaternion.FromToRotation(transform.up, FloorSnap.normal) * transform.rotation;
            groundCheck.rotation = toRotation;


        }
        else
        {
            groundCheck.rotation = player.playerBody.rotation;

        }

        foot.rotation = groundCheck.rotation;
        foot.localPosition = footPostition;


        if (fly)
        {
            if (Input.GetKey("space"))
            {
                y = 1;
            }
            else if (Input.GetKey("c"))
            {
                y = -1;
            }
            else
            {
                y = 0;
            }
        }
        else
		{

            if(Input.GetKeyDown("space") && canJump)
			{
                jump = true;
			}
            if(grounded)
			{
                y = -groundingForce;
			}
            



            


        }

        

        

        


        if (fly)
        {
            InputMovement = (((player.camTransformer.transform.right * x + player.camTransformer.transform.forward * z) * moveSpeed) + player.camTransformer.transform.up * y);
        }
        else
		{
            InputMovement = ((((groundCheck.transform.right) * x + (groundCheck.transform.forward) * z) * moveSpeed) + groundCheck.transform.up * y);
        }

        CmdMove();

    }


    /*
    [Server]
	private void FixedUpdate()
	{

        if(!isLocalPlayer)
        { return;
        }

        if (!grounded)
        {

            y -= -playerGravity * Time.fixedDeltaTime;

        }


        player.playerBody.AddForce(InputMovement, ForceMode.Impulse);

        if (jump)
        {
            player.playerBody.velocity += transform.up * jumpForce;
            jump = false;
        }

    }
    */



    [Command]
    void CmdMove()
    {
        if (!grounded)
        {

            y -= -playerGravity * Time.fixedDeltaTime;

        }


        player.playerBody.AddForce(InputMovement, ForceMode.Impulse);

        if (jump)
        {
            player.playerBody.velocity += transform.up * jumpForce;
            jump = false;
        }
    }

   
    public bool isWalking()
	{

        if (x != 0 || z != 0)
            return true;

        return false;

	}

    public Vector2 WalkInfo()
	{
        Vector2 vec = new Vector2();

        vec.Set(x, z);

        return vec;

	}

    public bool CanJump()
	{
        return Physics.CheckSphere(groundCheck.position, groundDistance, Jumpable);
    }

    public bool isGrounded()
	{
        return Physics.CheckSphere(groundCheck.position, groundDistance,Jumpable);
    }



}
