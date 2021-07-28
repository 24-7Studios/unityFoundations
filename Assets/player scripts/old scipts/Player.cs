using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
   
    //player setup
    
    public Rigidbody playerPhysBody;
    public Transform camTransformer;
    public GameObject CameraSetup;
    public Camera worldCam;
    public Camera gunCam;
    public GameObject weaponHolder;
    public GameObject PlayerModelPrefab;
    public PlayerModelClass PlayerModel;


    //mouse
    public float sens = 100f;

    
    float yMouseInput = 0;
    
    float xMouseInput = 0;

    //movement
    
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
    

    private void Start()
    {

        

        if (!isLocalPlayer)
        {
            
            foreach (GameObject part in PlayerModel.models)
            {
                part.layer = 0;
            }
            
        }


        if (isLocalPlayer)
        {
            
            foreach (GameObject part in PlayerModel.models)
            {
                part.layer = 6;
            }
            


            Instantiate(CameraSetup, camTransformer);



            Cursor.lockState = CursorLockMode.Locked;

        }

        Instantiate(PlayerModel, playerPhysBody.transform);
        
        
        if (usePhysicsGravity)
        {
            playerGravity = Physics.gravity.y;
        }

        footPostition = foot.localPosition;
        


    }

    
    private void Update()
    {




        //mouse looking


        if (isLocalPlayer)
        {

            float MouseX = Input.GetAxisRaw("Mouse X") * sens;
            float MouseY = Input.GetAxisRaw("Mouse Y") * sens;


            yMouseInput -= MouseY;
            yMouseInput = Mathf.Clamp(yMouseInput, -90f, 90f);

            xMouseInput -= MouseX;

            camTransformer.transform.localRotation = Quaternion.Euler(Vector3.right * yMouseInput);
            playerPhysBody.transform.rotation = Quaternion.Euler(Vector3.up * -xMouseInput);
            
            CmdSyncPlayerRotation(yMouseInput, xMouseInput);

        }

        


            
            

        
        

        ///////////////////////////////////////////////////////////////////////////////////////


        //movement
        
        //if (isServer || isLocalPlayer)
        //{

            grounded = isGrounded();
            canJump = CanJump();



            RaycastHit FloorSnap;

            if (Physics.Raycast(groundCheck.position, -groundCheck.up, out FloorSnap) && ((FloorSnap.normal.x! < maxAngle) || (FloorSnap.normal.y! < maxAngle)) && grounded)
            {

                Quaternion toRotation = Quaternion.FromToRotation(transform.up, FloorSnap.normal) * transform.rotation;
                groundCheck.rotation = toRotation;


            }
            else
            {
                groundCheck.rotation = playerPhysBody.rotation;

            }

            foot.rotation = groundCheck.rotation;
            foot.localPosition = footPostition;

        //}

        if (isLocalPlayer)
        {

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

                if (Input.GetKeyDown("space") && canJump)
                {
                    jump = true;
                }
                
            }




            if (fly)
            {
                InputMovement = (((camTransformer.transform.right * x + camTransformer.transform.forward * z) * moveSpeed) + camTransformer.transform.up * y);
            }
            else
            {
                InputMovement = ((((groundCheck.transform.right) * x + (groundCheck.transform.forward) * z) * moveSpeed) + groundCheck.transform.up * y);
            }

        }
        
        ////////////////////////////////////////////////////////



    }

    
    public void FixedUpdate()
    {

        if (isLocalPlayer)
        {

            

            if (jump)
            {   

                playerPhysBody.velocity += transform.up * jumpForce;
                jump = false;

                if (!isServer)
                {
                    CmdJump();
                }


            }

            if (!grounded)
            {
                y -= -playerGravity * Time.fixedDeltaTime;
            }
            if (grounded)
            {
                y = -groundingForce;
            }


            if (!isServer)
            {
                CmdMovePlayer(InputMovement);
            }


            playerPhysBody.AddForce(InputMovement, ForceMode.Impulse);


        }

        
    }

    
    public bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, Jumpable);
    }

    public bool CanJump()
    {
        return isGrounded();
    }
    

    [Command]
    void CmdJump()
    {

            playerPhysBody.velocity += transform.up * jumpForce;

    }

    [Command]
    void CmdMovePlayer(Vector3 IM)
    {
        InputMovement = IM;
        playerPhysBody.AddForce(InputMovement, ForceMode.Impulse);

    }

    [Command]
    void CmdSyncPlayerRotation(float y, float x)
    {
        /*
        yMouseInput = y;
        xMouseInput = x;

        camTransformer.transform.localRotation = Quaternion.Euler(Vector3.right * yMouseInput);
        playerPhysBody.transform.rotation = Quaternion.Euler(Vector3.up * -xMouseInput);
        */

        RpcSyncPlayerRotation(y, x);

        //RpcSyncPlayerRotation(y, x);

    }

    [ClientRpc]
    void RpcSyncPlayerRotation(float y, float x)
    {

        if (!isLocalPlayer)

        {

            yMouseInput = y;
            xMouseInput = x;

            camTransformer.transform.localRotation = Quaternion.Euler(Vector3.right * yMouseInput);
            playerPhysBody.transform.rotation = Quaternion.Euler(Vector3.up * -xMouseInput);


        }

    }

    

    public void viewPunch(float r)
    {
        yMouseInput -= r;
    }



}
