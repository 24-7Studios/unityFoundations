using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_fly : MonoBehaviour
{


    public float moveSpeed = 2;
    public float jumpForce = 5;
    public float sprintMultiplyer = 2;
    public float playerGravity = 9.86f;


    public Rigidbody body;
    public Transform cam;
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
    bool  grounded;
    bool canJump;
    bool jump = false;
    float x = 0;
    float z = 0;
    float y = 0;
    Vector3 groundNormal;



    // Start is called before the first frame update
    void Start()
    {

        if(usePhysicsGravity)
		{
            playerGravity = Physics.gravity.y;
		}

        footPostition = foot.localPosition;



    }

    // Update is called once per frame
    void Update()
    {

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
            groundCheck.rotation = body.rotation;

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
            InputMovement = (((cam.transform.right * x + cam.transform.forward * z) * moveSpeed) + cam.transform.up * y);
        }
        else
		{
            InputMovement = ((((groundCheck.transform.right) * x + (groundCheck.transform.forward) * z) * moveSpeed) + groundCheck.transform.up * y);
        }
        


    }

	private void FixedUpdate()
	{
        
        if(!grounded)
		{

            y -= -playerGravity * Time.fixedDeltaTime;

        }


        body.AddForce(InputMovement, ForceMode.Impulse);

		if(jump)
		{
            body.velocity += transform.up * jumpForce;
            jump = false;
        }



    }


   
    public bool isWalking()
	{

        if (x != 0 || z != 0)
            return true;

        return false;

	}

    public static bool isSprinting()
	{

        if(Input.GetKey(KeyCode.LeftShift))
                return true;

        return false;
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
