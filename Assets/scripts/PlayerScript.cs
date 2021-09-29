using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;

public class PlayerScript : NetworkBehaviour
{

    public GameObject testWeapon;


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
    public Transform backpack;
    [SerializeField]
    private Transform viewmodelHolder;

    [SyncVar]
    private List<WeaponClass> weapons;
    private WeaponClass primary;
    private WeaponClass secondary;
    //private int equipedSlot;
    //private WeaponClass equipedSlotWeapon;
    private WeaponClass equipedWeapon;
    private int inventoryIndex;


    //controls
    private Inputmaster controls;
    


    private void Awake()
    {

        controls = new Inputmaster();
        controls.Player.jump.performed += _ => activateJump();
        controls.Player.Change.performed += __ => ChangeSlot();
        
        
    }


    private void Start()
    {

        


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



        weapons = GetComponentsInChildren<WeaponClass>().ToList();

        if(weapons.Capacity > 0)
        {
            equipedWeapon = weapons[inventoryIndex];

            foreach (WeaponClass w in weapons)
            {
                if(w == equipedWeapon || w == equipedWeapon.getOtherHand())
                {
                    w.getViewmodelOb().SetActive(true);
                    w.getWorldModelOb().SetActive(true);
                }
                else
                {
                    w.getWorldModelOb().SetActive(false);
                    w.getViewmodelOb().SetActive(false);
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

    private int equipedSlot()
    {
        int value = -1;

        if(equipedWeapon == primary || equipedWeapon.getOtherHand() == primary)
        {
            value = 0;
        }
        else if(equipedWeapon == secondary || equipedWeapon.getOtherHand() == secondary)
        {
            value = 1;
        }

        return value;
    }

    private WeaponClass equipedSlotWeapon(int s)
    {

        if(s == 0)
        {
            return primary;
        }
        else if(s == 1)
        {
            return secondary;
        }
        else
        {
            return null;
        }

    }

    private void setWeapon(WeaponClass wep, int s)
    {
         
        if(s == 0)
        {
            
            if(primary != null)
            {
                primary.drop();
            }

            primary = wep;
        }
        if(s == 1)
        {

            if (secondary != null)
            {
                secondary.drop();
            }

            secondary = wep;
        }

    }

    private void ChangeSlot()
    {

        if(equipedSlot() == 0)
        {
            inventoryIndex = weapons.IndexOf(secondary);
        }
        if(equipedSlot() == 1)
        {
            inventoryIndex = weapons.IndexOf(primary);
        }
        
    }

    public void pickupWeapon(GameObject thing, bool hand)
    {


        thing.transform.SetParent(backpack, false);
        thing.transform.localPosition = Vector3.zero;

        Ipickup i = thing.GetComponent<Ipickup>();

        i.pickup(this);
        
            
        WeaponClass wep = thing.GetComponent<WeaponClass>();

            if (wep != null)
            {

           
                wep.hand = hand;

                if (hand)
                {
                    thing.transform.SetSiblingIndex(wep.getOtherHand().transform.GetSiblingIndex() + 1);
                }

                if(hand)
                {
                    if(equipedSlotWeapon(equipedSlot()).getOtherHand() != null)
                    {
                        equipedSlotWeapon(equipedSlot()).getOtherHand().drop();
                        equipedSlotWeapon(equipedSlot()).setOtherHand(wep);
                    }
                    else
                    {
                        equipedSlotWeapon(equipedSlot()).setOtherHand(wep);  
                    }
                }
                else
                {
                    
                    if(primary == null)
                    {
                        primary = wep;
                    }
                    else if(secondary == null)
                    {
                        secondary = wep;
                    }
                    else
                    {
                        setWeapon(wep, equipedSlot());
                    }

                }
                thing.transform.localPosition = wep.basePosOffset;
                thing.transform.localRotation = Quaternion.Euler(wep.baseRotOffset);
                thing.transform.localScale = wep.baseScaOffset;

                GameObject vModel = wep.getViewmodelOb();
                GameObject wModel = wep.getWorldModelOb();

                vModel.transform.SetParent(viewmodelHolder);
                vModel.transform.localPosition = Vector3.zero;


                PlayerModel.equipWeapon(wep, wep.hand);

                if (isLocalPlayer)
                {
                    vModel.SetActive(true);
                    vModel.layer = 11;
                    wModel.SetActive(true);
                    wModel.layer = 6;
                }
                else
                {
                    vModel.SetActive(false);
                    wModel.SetActive(true);
                    wModel.layer = 0;
                }

            }   
        
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
