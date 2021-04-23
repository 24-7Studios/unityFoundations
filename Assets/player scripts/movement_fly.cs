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
    public LayerMask ground;
    public float groundDistance = 0.1f;
    public float groundingForce = 0.05f;
    public bool fly = false;
    public bool usePhysicsGravity = false;
    
    

    Vector3 InputMovement;
    bool  grounded;

    float x = 0;
    float z = 0;
    float y = 0;




    // Start is called before the first frame update
    void Start()
    {

        if(usePhysicsGravity)
		{
            playerGravity = Physics.gravity.y;
		}





    }

    // Update is called once per frame
    void Update()
    {

        grounded = isGrounded();
        
        
         x = Input.GetAxisRaw("Horizontal");
         z = Input.GetAxisRaw("Vertical");


        

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

            if(Input.GetKeyDown("space") && grounded)
			{
                Jump();
			}
            else if(grounded)
			{
                y = -groundingForce;
			}
            else
			{
                y -= playerGravity * playerGravity * Time.deltaTime * Time.deltaTime;
			}
            


        }

        



        


        if (fly)
        {
            InputMovement = (((cam.transform.right * x + cam.transform.forward * z) * moveSpeed) + cam.transform.up * y);
        }
        else
		{
            InputMovement = (((body.transform.right * x + body.transform.forward * z) * moveSpeed) + body.transform.up * y);
        }
        


    }

	private void FixedUpdate()
	{
        

        
           
            body.AddForce(InputMovement, ForceMode.VelocityChange);

		



    }


    public void Jump()
	{

        body.velocity += transform.up * jumpForce;

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

    public bool isGrounded()
	{
        return Physics.CheckSphere(groundCheck.position, groundDistance, ground);
    }

}
