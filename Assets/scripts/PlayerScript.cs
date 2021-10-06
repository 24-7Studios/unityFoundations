using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;

public class PlayerScript : NetworkBehaviour
{

    //player setup
    /// <summary>
    /// components that make the player work. also likely to be referenced by other scripts especially in the guns
    /// </summary>

    public NetworkManager server;
    public Rigidbody playerPhysBody;
    public Transform camTransformer;
    public GameObject CameraSetup;
    public Camera worldCam;
    public Camera gunCam;
    

    [SerializeField]
    private Collider interactZone;

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


    [SerializeField]
    private float moveSpeed = 1.35f;

    [SerializeField]
    private float jumpForce = 20;

    [SerializeField]
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


    //backpack
    [SerializeField]
    private Transform backpack;
    [SerializeField]
    private Transform viewmodelHolder;

    [SerializeField]
    private int numOfSlots = 2;

    [SyncVar]
    List<Slot> WeaponSlots = new List<Slot>();

    [SyncVar, SerializeField]
    Slot primary;

    [SyncVar, SerializeField]
    Slot secondary;

    [SyncVar, SerializeField]
    private Slot equipedSlot;
    


    //controls
    private Inputmaster controls;
    


    private void Awake()
    {

        controls = new Inputmaster();
        controls.Player.jump.performed += _ => activateJump();
        controls.Player.Change.performed += __ => changeSlot();
        
        
    }


    private void Start()
    {


        for(int i = 0; i < numOfSlots; i++)
        {
            Slot s = new Slot(i);
            WeaponSlots.Add(s);
        }

        primary = WeaponSlots[0];
        secondary = WeaponSlots[1];

        equipedSlot = primary;

        setPlayermodel(PlayerModel);

  


        if (isLocalPlayer)
        {

            controls.Player.Enable();

            sens = settings.MouseSens;



            Instantiate(CameraSetup, camTransformer);



            Cursor.lockState = CursorLockMode.Locked;

        }

        
        
        
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



            float MouseX = controls.Player.looking.ReadValue<Vector2>().x * sens * Time.deltaTime;
            float MouseY = controls.Player.looking.ReadValue<Vector2>().y * sens * Time.deltaTime;


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

            x = controls.Player.Movement.ReadValue<Vector2>().x;
            z = controls.Player.Movement.ReadValue<Vector2>().y;

            
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
        //backpack 
        
        if(equipedSlot.getWeapon() != null)
        {
            if(isLocalPlayer)
            {
                equipedSlot.getWeapon().getViewmodelOb().SetActive(true);
            }

            equipedSlot.getWeapon().getWorldModelOb().SetActive(true);

            if (equipedSlot.getOtherHand() != null)
            {
                if(isLocalPlayer)
                {
                    equipedSlot.getOtherHand().getViewmodelOb().SetActive(true);
                }

                equipedSlot.getOtherHand().getWorldModelOb().SetActive(true);

            }
        }
        
        if(primary != equipedSlot)
        {
            if (primary.getWeapon() != null)
            {
                if (isLocalPlayer)
                {
                    primary.getWeapon().getViewmodelOb().SetActive(false);
                }

                primary.getWeapon().getWorldModelOb().SetActive(false);

                if (primary.getOtherHand() != null)
                {
                    if (isLocalPlayer)
                    {
                        primary.getOtherHand().getViewmodelOb().SetActive(false);
                    }

                    primary.getOtherHand().getWorldModelOb().SetActive(false);

                }
            }
        }

        if (secondary != equipedSlot)
        {
            if (secondary.getWeapon() != null)
            {
                if (isLocalPlayer)
                {
                    secondary.getWeapon().getViewmodelOb().SetActive(false);
                }

                secondary.getWeapon().getWorldModelOb().SetActive(false);

                if (secondary.getOtherHand() != null)
                {
                    if (isLocalPlayer)
                    {
                        secondary.getOtherHand().getViewmodelOb().SetActive(false);
                    }

                    secondary.getOtherHand().getWorldModelOb().SetActive(false);

                }
            }
        }

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
                CmdMovePlayer(InputMovement, playerPhysBody.position);
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
    
    void activateJump()
    {           
        if(!fly && (canJump == true) && jump == false )
        {
            jump = true;
        }
            
    }

    [Command]
    void CmdJump()
    {

            playerPhysBody.velocity += transform.up * jumpForce;

    }

    [Command]
    void CmdMovePlayer(Vector3 IM, Vector3 clientPos)
    {

        playerPhysBody.AddForce(IM, ForceMode.Impulse);
        

        if (!isLocalPlayer && Vector3.Distance(playerPhysBody.position, clientPos) > PostionSnapThreshold)
        {

            RpcCorrectPlayerPos(playerPhysBody.position);

        }
        else if (!isLocalPlayer && Vector3.Distance(playerPhysBody.position, clientPos) < PostionSnapThreshold)
        {


            SyncTimer -= Time.deltaTime;

            if (SyncTimer < SyncInterval)
            {

                SyncTimer = SyncInterval;
                playerPhysBody.position = Vector3.Lerp(playerPhysBody.position, clientPos, Time.fixedDeltaTime / PositionCompensationDamping);


            }



        }

        RpcSyncPlayerPosistion(playerPhysBody.position);

    }


    [ClientRpc]
    void RpcSyncPlayerPosistion(Vector3 serverPos)
    {

        if (!isLocalPlayer)
        {
            playerPhysBody.position = serverPos;
        }
      

    }
    [ClientRpc]
    void RpcCorrectPlayerPos(Vector3 serverPos)
    {

        playerPhysBody.position = serverPos;

    }



    [Command]
    void CmdSyncPlayerRotation(float y, float x)
    {
        
        yMouseInput = y;
        xMouseInput = x;

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

    public void setPlayermodel(PlayerModelClass p)
    {

        PlayerModel = Instantiate(p, playerPhysBody.transform);

        PlayerModel.setPlayer(this);
        

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
        }
    }

    public Transform getBackpack()
    {
        return backpack;
    }

    public Transform getViewmodelHolder()
    {
        return viewmodelHolder;
    }

    private void changeSlot()
    {
        if(equipedSlot == primary)
        {
            equipedSlot = secondary;
        }
        else
        {
            equipedSlot = primary;
        }

        if(!isServer)
        {
            cmdChangeSlot(equipedSlot.getIndex());
        }
        else
        {
            rpcChangeSlot(equipedSlot.getIndex());
        }

    }

    [Command]
    private void cmdChangeSlot(int i)
    {
        equipedSlot = WeaponSlots[i];
    }

    [ClientRpc]
    private void rpcChangeSlot(int i)
    {
        equipedSlot = WeaponSlots[i];
    }


    public void pickup(GameObject thing)
    {

        Ipickup i = thing.GetComponent<Ipickup>();

        i.pickup(this);
        
    }

    public Slot getEquipedSlot()
    {
        return equipedSlot;
    }

    public Slot getSlotAtIndex(int s)
    {
        return WeaponSlots[s];
    }

    public void drop(GameObject thing)
    {   
        Ipickup i = thing.GetComponent<Ipickup>();

        i.drop();

        thing.transform.position = transform.position + transform.forward * 2;    
    }


    public void viewPunch(float r)
    {
        yMouseInput -= r;
    }

    

}

[System.Serializable]
public class Slot
{
    [SerializeField]
    private WeaponClass myWeapon;
    [SerializeField]
    private WeaponClass otherHand;
    [SerializeField]
    private int myIndex;

    public Slot()
    {
        myIndex = -1;
    }

    public Slot(int s)
    {
        myIndex = s;
    }

    public void setIndex(int s)
    {
        myIndex = s;
    }

    public int getIndex()
    {
        return myIndex;
    }

    public WeaponClass getWeapon()
    {
        return myWeapon;
    }

    public WeaponClass getOtherHand()
    {
        return otherHand;
    }

    public void setWeapon(WeaponClass w)
    {
        if(myWeapon != null)
        {
            myWeapon.drop();
        }

        myWeapon = w;
    }

    public void setWeapon(WeaponClass w, bool hand)
    {

        w.setHand(hand);

        if(!hand)
        {
            if(myWeapon != null)
            {
                myWeapon.drop();
            }

            myWeapon = w; 
        }
        else
        {
            if(otherHand != null)
            {
                otherHand.drop();
            }

            otherHand = w;
        }

    }


}