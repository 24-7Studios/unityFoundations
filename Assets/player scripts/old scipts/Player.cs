using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
   
    //player setup
    /// <summary>
    /// components that make the player work. also likely to be referenced by other scripts especially in the guns
    /// </summary>
    
    public Rigidbody playerPhysBody;
    public Transform camTransformer;
    public GameObject CameraSetup;
    public Camera worldCam;
    public Camera gunCam;
    public GameObject weaponHolder;

    [SerializeField] 
    private GameObject PlayerModelPrefab;

    public PlayerModelClass PlayerModel;

    [SerializeField] 
    private localPlayerOptions settings;





    //mouse
    /// <summary>
    /// pee pee poo poo what do u think it is
    /// </summary>

    private float sens;

    
    private float yMouseInput = 0;
    
    private float xMouseInput = 0;





    //movement
    /// <summary>
    /// stuff with movement. mostly just modifiers and some stuff with network syncing
    /// </summary>


    //[SerializeField]
    private float moveSpeed = 1.35f;

    //[SerializeField]
    private float jumpForce = 20;

    //[SerializeField]
    private float playerGravity = -0.75f;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private Transform foot;

    private Vector3 footPostition;

    [SerializeField]
    private LayerMask Jumpable;

    //[SerializeField]
    private float groundDistance = 0.3f;

    //[SerializeField]
    private float groundingForce = 0.1f;

    //[SerializeField]
    private float maxAngle = 20;

    [SerializeField]
    private float PositionCompensationDamping = 0.25f;

    [SerializeField]
    private float PostionSnapThreshold = 10;

    [SerializeField]
    private float SyncInterval = 999f;

    float SyncTimer = 0;

    [SerializeField]
    private bool fly = false;

    [SerializeField]
    private bool usePhysicsGravity = false;

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

            sens = settings.MouseSens;


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



            playerPhysBody.AddForce(InputMovement, ForceMode.Impulse);


            if (!isServer)
            {
                CmdMovePlayer(InputMovement);
            }
            else
            {
                RpcSyncPlayerPosistion(playerPhysBody.position);
            }



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

        playerPhysBody.AddForce(IM, ForceMode.Impulse);
        RpcSyncPlayerPosistion(playerPhysBody.position);

    }


    [ClientRpc]
    void RpcSyncPlayerPosistion(Vector3 P)
    {

        if (!isLocalPlayer)
        {
            playerPhysBody.position = P;
        }
        else if(isLocalPlayer && !isServer && Vector3.Distance(playerPhysBody.position, P) > PostionSnapThreshold)
        {

            playerPhysBody.position = P;

        }
        else if(isLocalPlayer && !isServer && Vector3.Distance(playerPhysBody.position, P) < PostionSnapThreshold)
        {

            CmdCompensateOnServer(playerPhysBody.position);

        }

    }

    [Command]
    void CmdCompensateOnServer(Vector3 P)
    {

        

        SyncTimer -= Time.deltaTime;

        if (SyncTimer < SyncInterval)
        {
            playerPhysBody.position = Vector3.Lerp(playerPhysBody.position, P, Time.fixedDeltaTime / PositionCompensationDamping);
            
            SyncTimer = syncInterval;
        }

        

    }


    [Command]
    void CmdSyncPlayerRotation(float y, float x)
    {
        
        yMouseInput = y;
        xMouseInput = x;

        /*
        camTransformer.transform.localRotation = Quaternion.Euler(Vector3.right * yMouseInput);
        playerPhysBody.transform.rotation = Quaternion.Euler(Vector3.up * -xMouseInput);
        */

        

        RpcSyncPlayerRotation(y, x);

    }

    [ClientRpc]
    void RpcSyncPlayerRotation(float y, float x)
    {

        if (!isLocalPlayer)

        {

            yMouseInput = y;
            xMouseInput = x;

            camTransformer.transform.localRotation = Quaternion.Euler(Vector3.right * y);
            playerPhysBody.transform.rotation = Quaternion.Euler(Vector3.up * -x);


        }

    }

    

    public void viewPunch(float r)
    {
        yMouseInput -= r;
    }



}
