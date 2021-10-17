using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerScript : NetworkBehaviour, IDamage
{

    //player setup
    /// <summary>
    /// components that make the player work. also likely to be referenced by other scripts especially in the guns
    /// </summary>

    [SerializeField]
    private Rigidbody playerPhysBody;

    [SerializeField]
    private Transform camTransformer;

    [SerializeField]
    private GameObject CameraSetup;

    [SerializeField]
    private AudioSource aud;

    [SerializeField]
    private AudioSource LocalAud;

    [SerializeField]
    private Camera worldCam;

    [SerializeField]
    private Camera gunCam;
    

    [SerializeField]
    private Collider interactZone;

    [SerializeField]
    private PlayerModelClass PlayerModel;

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

    //health
    [SerializeField]
    private float DefaultHealth = 100;

    [SyncVar]
    private float health;

    [SerializeField]
    private float DefaultShields = 100;

    [SyncVar]
    private float shields;

    [SerializeField]
    private float instantDeath;

    public delegate void Spawned(PlayerScript player);
    public static Spawned spawned;

    public delegate void Died(PlayerScript player);
    public static Died died;

    [SerializeField]
    private AudioClip localDamageSound;

    [SerializeField]
    private AudioClip DieSound;

    //backpack
    [SerializeField]
    private Transform backpack;
    [SerializeField]
    private Transform viewmodelHolder;

    [SerializeField]
    private int numOfSlots = 2;

    [SyncVar]
    List<Slot> WeaponSlots = new List<Slot>();

    [SerializeField]
    Slot primary;

    [SerializeField]
    Slot secondary;

    [SerializeField]
    private Slot equipedSlot;
    


    //controls
    private Inputmaster controls;
    


    private void Awake()
    {

        controls = new Inputmaster();
        controls.Player.jump.performed += ctx => activateJump();
        controls.Player.Change.performed += ctx => changeSlot();


        
    }


    private void Start()
    {

        health = DefaultHealth;


        for(int i = 0; i < numOfSlots; i++)
        {
            Slot s = new Slot(i);
            WeaponSlots.Add(s);
        }

        primary = WeaponSlots[0];
        secondary = WeaponSlots[1];

        equipedSlot = primary;

        setPlayermodel(PlayerModel);

  
        if(!isLocalPlayer)
        {
            gameObject.layer = 0;
        }

        if (isLocalPlayer)
        {

            controls.Player.Enable();

            sens = settings.MouseSens;



            LocalAud = Instantiate(CameraSetup, camTransformer).GetComponent<AudioSource>();



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
                RpcSyncPlayerPosistion(InputMovement, playerPhysBody.position);
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

        //playerPhysBody.AddForce(IM, ForceMode.Impulse);
        

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

        RpcSyncPlayerPosistion(IM, playerPhysBody.position);

    }


    [ClientRpc]
    void RpcSyncPlayerPosistion(Vector3 IM, Vector3 serverPos)
    {

        if (!isLocalPlayer && Vector3.Distance(playerPhysBody.position, serverPos) < PostionSnapThreshold)
        {
            playerPhysBody.AddForce(IM, ForceMode.Impulse);
            playerPhysBody.position = Vector3.Lerp(playerPhysBody.position, serverPos, Time.fixedDeltaTime / PositionCompensationDamping);
        }
        else if(!isLocalPlayer)
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

    public PlayerModelClass getPlayermodel()
    {
        return PlayerModel;
    }

    public void setPlayermodel(PlayerModelClass p)
    {

        PlayerModel = Instantiate(p, playerPhysBody.transform);

        PlayerModel.setPlayer(this);
        PlayerModel.netIdentity.AssignClientAuthority(connectionToClient);

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

    public Transform getCamTransformer()
    {
        return camTransformer;
    }

    public Transform getBackpack()
    {
        return backpack;
    }

    public Transform getViewmodelHolder()
    {
        return viewmodelHolder;
    }

    public Inputmaster getInputMaster()
    {
        return controls;
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
        rpcChangeSlot(i);
    }

    [ClientRpc]
    private void rpcChangeSlot(int i)
    {
        if(!isLocalPlayer)
        {
            equipedSlot = WeaponSlots[i];
        }
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

    public void drop(NetworkIdentity net)
    {
        Ipickup i = net.GetComponent<Ipickup>();

        i.drop();

        net.transform.position = transform.position + transform.forward * 2;
    }

    public void viewPunch(float r)
    {
        yMouseInput -= r;
    }

    public void die()
    {
        if(!isServer)
        {
            if(isLocalPlayer)
            {
                cmdDie();
            }
        }
        else
        {
            rpcDie();
        }
    }

    [Command]
    private void cmdDie()
    {
        rpcDie();
    }

    [ClientRpc]
    private void rpcDie()
    {
        died?.Invoke(this);
        foreach (NetworkIdentity i in backpack.GetComponentsInChildren<NetworkIdentity>())
        {
            drop(i);
        }
        aud.PlayOneShot(DieSound);
        transform.position = Vector3.zero;
        health = DefaultHealth;
    }
      
    public void takeDamagefromHit(float d, float m)
    {
        if(!isServer)
        {
            cmdTakeDamageFromHit(d, m);
        }
        else
        {
            rpcTakeDamageFromHit(d, m);
        }
    }

    [Command]
    private void cmdTakeDamageFromHit(float d, float m)
    {
        rpcTakeDamageFromHit(d, m);
    }

    [ClientRpc]
    private void rpcTakeDamageFromHit(float d, float s)
    {
        health -= d;

        if(health < 0)
        {
            die();
        }
        else
        {
            if(isLocalPlayer)
            {
                LocalAud.PlayOneShot(localDamageSound);
            }
        }
    }

    public void hit()
    {

    }

}

[System.Serializable]
public class Slot
{
    [SerializeField, SyncVar]
    private WeaponClass myWeapon;
    [SerializeField, SyncVar]
    private WeaponClass otherHand;
    [SerializeField, SyncVar]
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

        if (w != null)
        {
            w.setHand(hand);
        }

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

    public void dropWeapon(bool hand)
    {
        if(!hand)
        {
            if(myWeapon != null)
            {
                myWeapon.drop();
            }
        }
        else
        {
            if(otherHand != null)
            {
                otherHand.drop();
            }
        }
    }

    public void removeWeapon(bool hand)
    {
        if(!hand)
        {
            myWeapon = null;
        }
        else
        {
            otherHand = null;
        }
    }


}