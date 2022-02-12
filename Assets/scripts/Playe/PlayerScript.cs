using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Users;
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
    private interactZoneScript interactZone;

    [SerializeField]
    private PlayerModelClass PlayerModel;

    [SerializeField]
    private localPlayerOptions settings;





    //mouse and camera effects
    /// <summary>
    /// pee pee poo poo what do u think it is
    /// </summary>

    private float sens;


    private float yMouseInput = 0;

    private float xMouseInput = 0;

    //[SerializeField]
    private float viewpunchAttack = 1;

    private float viewPunchRecovery = 4.25f;

    private float CurrentPunch = 0;

    private float TotalPunch = 0;

    private float AppliedPunch;

    private float recoilAttack;

    private float recoilSmoothing;

    private float Totalrecoil;

    private float currentRecoil;

    private float appliedRecoil;

    [SerializeField]
    private bool bob = true;

    [SerializeField]
    private float bobAmp = 0.015f;

    [SerializeField]
    private float bobFreq = 10.0f;

    [SerializeField]
    private float bobSmoothing = 4;

    [SerializeField]
    private bool cameraTilt;

    [SerializeField]
    private float tiltAmount = 2;

    [SerializeField]
    private float tiltSmoothing = 1;

    private Vector3 defaultCameraPos;
    private Vector3 defaultCameraRot;

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

    [SerializeField]
    private float groundDistance = 0.25f;

    [SerializeField]
    private float groundingForce = 0.05f;

    [SerializeField]
    private float maxAngle = 20;

    [SerializeField]
    private float PositionCompensationDamping = 0.25f;

    [SerializeField]
    private float PostionSnapThreshold = 10;

    [SerializeField]
    private float SyncInterval = 100f;

    float SyncTimer = 0;

    [SerializeField]
    private bool fly = false;

    [SerializeField]
    private bool usePhysicsGravity = false;




    bool grounded;
    bool canJump;
    bool jump = false;
    float x = 0;
    float z = 0;
    float y = 0;
    Vector3 BasicInputMovement;
    Vector3 InputMovement;
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

    [SyncVar]
    private bool isDead;

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
    private bool viewmodelSway = true;
    [SerializeField]
    private float viewmodelSwayFactor = 0;
    [SerializeField]
    private float viewmodelSwaySmoothing = 0;
    [SerializeField]
    private bool viewmodelShift = true;
    [SerializeField]
    private float viewmodelShiftFactor = 0;
    [SerializeField]
    private float viewmodelShiftSmoothing = 0;

    [SerializeField]
    private int numOfSlots = 2;

    [SyncVar]
    List<Slot> WeaponSlots = new List<Slot>();

    [SerializeField]
    Slot meleeSlot;

    [SerializeField]
    private Slot primarySlot;

    [SerializeField]
    private Slot secondarySlot;

    [SerializeField]
    WeaponClass defaultMelee;

    private bool requestedMelee = false;

    [SerializeField]
    private Slot equipedSlot;

    [SerializeField]
    private Slot previousSlot;


    //controls
    private Inputmaster controls;

    [SerializeField]
    private PlayerInput input;



    private void Awake()
    {
        controls = new Inputmaster();
        input = GetComponent<PlayerInput>();
        controls.Player.jump.performed += ctx => activateJump();
        controls.Player.Change.performed += ctx => changeSlot();
        controls.Player.interact.started += ctx => interact();
        controls.Player.interact.performed += ctx => manualPickup();
        controls.Player.drop.performed += ctx => manualDrop();
        controls.Player.melee.performed += ctx => equipToMelee();
        died += onDeath;
    }


    private void Start()
    {

        health = DefaultHealth;


        for (int i = 0; i <= numOfSlots; i++)
        {
            Slot s = new Slot(i);
            WeaponSlots.Add(s);
        }

        meleeSlot = WeaponSlots[0];
        primarySlot = WeaponSlots[1];
        secondarySlot = WeaponSlots[2];

        equipedSlot = meleeSlot;


        setPlayermodel(PlayerModel);


        if (isLocalPlayer)
        {
            gameObject.layer = 6;
        }
        else
        {
            gameObject.layer = 7;
        }

        if (isLocalPlayer)
        {

            controls.Player.Enable();
            input.enabled = true;

            LocalAud = Instantiate(CameraSetup, camTransformer).GetComponent<AudioSource>();

            Cursor.lockState = CursorLockMode.Locked;

        }




        if (usePhysicsGravity)
        {
            playerGravity = Physics.gravity.y;
        }

        footPostition = foot.localPosition;




    }


    private void OnConnectedToServer()
    {
        cmdSyncSlots(equipedSlot.getIndex(), previousSlot.getIndex());
    }

    private void Update()
    {


        //mouse looking


        if (isLocalPlayer)
        {

            float MouseX = 0;
            float MouseY = 0;


            if (input.currentControlScheme != null)
            {
                if (input.currentControlScheme.Equals("gamepad"))
                {
                    MouseX = controls.Player.looking.ReadValue<Vector2>().x * settings.controllerSens * Time.deltaTime;
                    MouseY = controls.Player.looking.ReadValue<Vector2>().y * settings.controllerSens * Time.deltaTime;
                }
                else
                {
                    MouseX = controls.Player.looking.ReadValue<Vector2>().x * settings.MouseSens * Time.deltaTime;
                    MouseY = controls.Player.looking.ReadValue<Vector2>().y * settings.MouseSens * Time.deltaTime;
                }
            }




            yMouseInput -= MouseY;
            yMouseInput = Mathf.Clamp(yMouseInput, -90f, 90f);
            xMouseInput -= MouseX;

            //TotalPunch = Mathf.Lerp(TotalPunch, 0, viewPunchRecovery * Time.deltaTime);
            //AppliedPunch = Mathf.Lerp(AppliedPunch, TotalPunch, viewpunchAttack * Time.deltaTime);

            currentRecoil = Mathf.Lerp(currentRecoil, currentRecoil + Totalrecoil, recoilAttack * Time.deltaTime);
            Totalrecoil = Mathf.Lerp(Totalrecoil, 0, recoilSmoothing * Time.deltaTime);
            yMouseInput += Totalrecoil * Time.deltaTime * recoilAttack;

            CurrentPunch = Mathf.Lerp(CurrentPunch, TotalPunch, Time.deltaTime * viewpunchAttack);
            TotalPunch = Mathf.Lerp(TotalPunch, 0, Time.deltaTime * viewPunchRecovery);
            AppliedPunch = (TotalPunch + CurrentPunch) / 2;


            float yMouseTotal = yMouseInput + AppliedPunch;
            float xMouseTotal = xMouseInput;
            //camTransformer.transform.localRotation = Quaternion.Euler(Vector3.right * yMouseInput);


            camTransformer.transform.localRotation = Quaternion.Euler((yMouseTotal) * Vector3.right);

            playerPhysBody.transform.rotation = Quaternion.Euler(Vector3.up * -xMouseTotal);



            CmdSyncPlayerRotation(yMouseTotal, xMouseTotal);

            //Camera Tilt

            //camTransformer.localRotation = Quaternion.Euler((getBasicInputMovement().normalized * tiltAmount) + camTransformer.localRotation.eulerAngles);

            //viewmodel sway and roation

            Quaternion swayX = Quaternion.AngleAxis(-controls.Player.looking.ReadValue<Vector2>().y * settings.MouseSens * viewmodelSwayFactor, Vector3.right);
            Quaternion swayY = Quaternion.AngleAxis(controls.Player.looking.ReadValue<Vector2>().x * settings.MouseSens * viewmodelSwayFactor, Vector3.up);

            Quaternion targetSway = swayX * swayY;

            viewmodelHolder.localRotation = Quaternion.Slerp(viewmodelHolder.localRotation, targetSway, viewmodelSwaySmoothing * Time.deltaTime);

            Vector3 targetShift = BasicInputMovement * viewmodelShiftFactor;

            viewmodelHolder.localPosition = Vector3.Lerp(viewmodelHolder.localPosition, targetShift, viewmodelShiftSmoothing * Time.deltaTime);    

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




            BasicInputMovement = (Vector3.right * x) + (Vector3.up * y) + (Vector3.forward * z);

            if (fly)
            {
                InputMovement = (((camTransformer.transform.right * x + camTransformer.transform.forward * z) * moveSpeed) + camTransformer.transform.up * y);
            }
            else
            {
                InputMovement = ((((groundCheck.transform.right) * x + (groundCheck.transform.forward) * z) * moveSpeed) + groundCheck.transform.up * y);
            }

            if (isServer)
            {
                RpcSyncPlayerInput(BasicInputMovement, InputMovement);
            }
            else
            {
                CmdSyncPlayerInput(BasicInputMovement, InputMovement);
            }

        }

        ////////////////////////////////////////////////////////
        //backpack 

        if (isLocalPlayer)
        {
            if (meleeSlot.getWeapon() == null && requestedMelee == false)
            {
                giveMelee();
            }

            if(equipedSlot.getWeapon() == null)
            {
                changeSlot();
            }
        }

        /*
        if(equipedSlot.getWeapon() != null)
        {
            equipedSlot.getWeapon().getViewmodelOb().SetActive(true);
            equipedSlot.getWeapon().getWorldModelOb().SetActive(true);
        }
        if(equipedSlot.getOtherHand() != null)
        {
            equipedSlot.getOtherHand().getViewmodelOb().SetActive(true);
            equipedSlot.getOtherHand().getWorldModelOb().SetActive(true);
        }

        foreach (Slot s in WeaponSlots)
        {
            if(s != equipedSlot)
            {
                if(s.getWeapon() != null)
                {
                    s.getWeapon().getViewmodelOb().SetActive(false);
                    s.getWeapon().getWorldModelOb().SetActive(false);
                }
                if (s.getOtherHand() != null)
                {
                    s.getOtherHand().getViewmodelOb().SetActive(false);
                    s.getOtherHand().getWorldModelOb().SetActive(false);
                }
            }
        }
        */


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



            playerPhysBody.AddForce(InputMovement, ForceMode.VelocityChange);


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
        if (!fly && (canJump == true) && jump == false)
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
            playerPhysBody.AddForce(IM, ForceMode.VelocityChange);
            playerPhysBody.position = Vector3.Lerp(playerPhysBody.position, serverPos, Time.fixedDeltaTime / PositionCompensationDamping);
        }
        else if (!isLocalPlayer)
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

        RpcSyncPlayerRotation(y, x);

    }

    [ClientRpc]
    void RpcSyncPlayerRotation(float y, float x)
    {

        if (!isLocalPlayer)
        {

            camTransformer.transform.localRotation = Quaternion.Euler(Vector3.right * y);
            playerPhysBody.transform.rotation = Quaternion.Euler(Vector3.up * -x);


        }

    }

    [Command]
    void CmdSyncPlayerInput(Vector3 BIM, Vector3 IM)
    {
        BasicInputMovement = BIM;
        InputMovement = IM;

        RpcSyncPlayerInput(BasicInputMovement, InputMovement);
    }

    [ClientRpc]
    void RpcSyncPlayerInput(Vector3 BIM, Vector3 IM)
    {
        if (!isLocalPlayer)
        {
            BasicInputMovement = BIM;
            InputMovement = IM;
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

        camTransformer.position = PlayerModel.CameraOffset.position;
        defaultCameraPos = camTransformer.localPosition;
        defaultCameraRot = camTransformer.localRotation.eulerAngles;

        //PlayerModel.netIdentity.AssignClientAuthority(connectionToClient);

        if (PlayerModel.hitBoxes.Capacity > 0)
        {
            gameObject.layer = 2;

            if (!isLocalPlayer)
            {
                foreach (hitbox part in PlayerModel.hitBoxes)
                {
                    part.gameObject.layer = 0;
                }
            }
            else
            {
                foreach (hitbox part in PlayerModel.hitBoxes)
                {
                    part.gameObject.layer = 6;
                }
            }
        }
        else
        {
            if (!isLocalPlayer)
            {
                gameObject.layer = 0;
            }
            else
            {
                gameObject.layer = 6;
            }
        }

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

    public Vector3 getInputMovement()
    {
        return InputMovement;
    }

    public Vector3 getBasicInputMovement()
    {
        return BasicInputMovement;
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

    //////////////////////////////////
    private bool canChange()
    {

        if (primarySlot.getWeapon() != null && secondarySlot.getWeapon() != null)
        {
            return true;
        }
        if (equipedSlot == meleeSlot && (primarySlot.getWeapon() != null || secondarySlot.getWeapon() != null))
        {
            return true;
        }

        return false;
    }

    private void equipToMelee()
    {

        if (equipedSlot != meleeSlot)
        {
            previousSlot = equipedSlot;
        }
        equipedSlot = meleeSlot;

        if (previousSlot.getWeapon() != null)
        {
            previousSlot.getWeapon().onDequip();
            if (previousSlot.getOtherHand() != null)
            {
                previousSlot.getOtherHand().onDequip();
            }
        }

        if(equipedSlot.getWeapon() != null)
        {
            equipedSlot.getWeapon().onEquip();
            if (equipedSlot.getOtherHand() != null)
            {
                equipedSlot.getOtherHand().onEquip();
            }
        }

        syncSlots();
    }

    public void changeSlot()
    {
        if (!(primarySlot.getWeapon() == null && secondarySlot.getWeapon() == null))
        {
            if (equipedSlot == meleeSlot)
            {
                if (previousSlot.getWeapon() != null)
                {
                    equipedSlot = previousSlot;
                    previousSlot = meleeSlot;
                }
                else
                {
                    previousSlot = meleeSlot;
                    if (primarySlot.getWeapon() != null)
                    {
                        equipedSlot = primarySlot;
                    }
                    else
                    {
                        equipedSlot = secondarySlot;
                    }
                }
            }
            else if (equipedSlot == primarySlot && secondarySlot.getWeapon() != null)
            {
                previousSlot = primarySlot;
                equipedSlot = secondarySlot;
            }
            else if (equipedSlot == secondarySlot && primarySlot.getWeapon() != null)
            {
                previousSlot = secondarySlot;
                equipedSlot = primarySlot;
            }

            if (previousSlot.getWeapon() != null)
            {
                previousSlot.getWeapon().onDequip();
                if (previousSlot.getOtherHand() != null)
                {
                    previousSlot.getOtherHand().onDequip();
                }
            }

            //meleeSlot.getWeapon().onDequip();


            equipedSlot.getWeapon().onEquip();

            if (equipedSlot.getOtherHand() != null)
            {
                equipedSlot.getOtherHand().onEquip();
            }

            syncSlots();

        }
        else
        {
            /*
            if(primarySlot.getWeapon() == null && secondarySlot.getWeapon() == null)
            {
                equipToMelee();
            }
            */
            equipToMelee();
        }
    }

    public void syncSlots()
    {
        if (isLocalPlayer)
        {
            if(equipedSlot.getWeapon() != null)
            {
                equipedSlot.getWeapon().onEquip();

                if (equipedSlot.getOtherHand() != null)
                {
                    equipedSlot.getOtherHand().onEquip();
                }
            }

            if (!isServer)
            {
                cmdSyncSlots(equipedSlot.getIndex(), previousSlot.getIndex());
            }
            else
            {
                rpcSyncSlots(equipedSlot.getIndex(), previousSlot.getIndex());
            }
        }
    }

    [Command]
    private void cmdSyncSlots(int e, int p)
    {
        equipedSlot = WeaponSlots[e];
        if (p >= 0)
        {
            previousSlot = WeaponSlots[p];
        }

        rpcSyncSlots(e, p);
    }

    [ClientRpc]
    private void rpcSyncSlots(int e, int p)
    {
        if (!isLocalPlayer)
        {
            equipedSlot = WeaponSlots[e];

            if (p >= 0)
            {
                previousSlot = WeaponSlots[p];
            }

            if (previousSlot.getWeapon() != null)
            {
                previousSlot.getWeapon().onDequip();
                if (previousSlot.getOtherHand() != null)
                {
                    previousSlot.getOtherHand().onDequip();
                }
            }

            if (equipedSlot.getWeapon() != null)
            {
                equipedSlot.getWeapon().onEquip();

                if (equipedSlot.getOtherHand() != null)
                {
                    equipedSlot.getOtherHand().onEquip();
                }
            }


            Debug.Log(equipedSlot.getWeapon());

        }
    }


    private void giveMelee()
    {
        requestedMelee = true;
        if (isServer)
        {
            GameObject melee = Instantiate(defaultMelee.gameObject);
            NetworkServer.Spawn(melee);
            rpcGiveMelee(melee);
        }
        else
        {
            cmdGiveMelee();
        }
    }

    [Command]
    private void cmdGiveMelee()
    {
        GameObject melee = Instantiate(defaultMelee.gameObject);
        NetworkServer.Spawn(melee);
        rpcGiveMelee(melee);
    }

    [ClientRpc]
    private void rpcGiveMelee(GameObject melee)
    {
        requestedMelee = false;
        melee.GetComponent<WeaponClass>().forcedPickup(this, meleeSlot.getIndex(), false);
    }

    private void interact()
    {

    }

    private void manualPickup()
    {


        Ipickup[] list = interactZone.getList();
        Ipickup target = null;
        if (list.Length > 0)
        {

            float distance = Vector3.Distance(list[0].getObject().transform.position, camTransformer.position);
            target = list[0];

            foreach (Ipickup i in list)
            {
                Debug.Log(i);
                if (Vector3.Distance(i.getObject().transform.position, camTransformer.position) <= distance)
                {
                    distance = Vector3.Distance(i.getObject().transform.position, camTransformer.position);
                    target = i;
                }
            }

            if (target != null)
            {
                pickup(target.getObject());
            }
        }
    }

    public void pickup(GameObject thing)
    {
        if(isServer)
        {
            Ipickup i = thing.GetComponent<Ipickup>();

            i.serverPickup(this);

            rpcPickup(thing);
        }
        else
        {
            cmdPickup(thing);
        }
    }

    [Command]
    private void cmdPickup(GameObject thing)
    {
        Ipickup i = thing.GetComponent<Ipickup>();

        i.serverPickup(this);

        rpcPickup(thing);
    }


    [ClientRpc]
    private void rpcPickup(GameObject thing)
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

    public Slot getPickupSlot()
    {
        foreach (Slot s in WeaponSlots)
        {
            if (s.getWeapon() == null)
            {
                return s;
            }
        }

        if (equipedSlot == meleeSlot)
        {
            return previousSlot;
        }
        else
        {
            return equipedSlot;
        }
    }

    
    private void manualDrop()
    {
        if(equipedSlot != meleeSlot)
        {
            tryDrop(equipedSlot.getWeapon().netIdentity);
        }
    }

    private void tryDrop(NetworkIdentity thing)
    {
        if(isServer)
        {
            rpcDrop(thing);
        }
        else
        {
            cmdDrop(thing);
        }
    }

    [Command]
    private void cmdDrop(NetworkIdentity thing)
    {
        rpcDrop(thing);
    }

    [Command]
    private void cmdDoDrop(NetworkIdentity thing)
    {
        drop(thing);
    }

    [ClientRpc]
    private void rpcDrop(NetworkIdentity thing)
    {
        drop(thing);
        //cmdDoDrop(thing);
    }

    public void drop(GameObject thing)
    {   
        Ipickup i = thing.GetComponent<Ipickup>();
        interactZone.remove(i);
        i.drop();

        //thing.transform.position = transform.position + transform.forward * 2;    
    }

    public void drop(NetworkIdentity net)
    {
        Ipickup i = net.GetComponent<Ipickup>();
        interactZone.remove(i);
        i.drop();

        //net.transform.position = transform.position + transform.forward * 2;
    }

    public void viewPunch(float p, float a, float r)
    {
        TotalPunch -= p;
        viewpunchAttack = a;
        viewPunchRecovery = r;
    }

    public void recoil(float p, float a, float r)
    {
        Totalrecoil -= p;
        recoilAttack = a;
        recoilSmoothing = r;
    }

    public float getHealth()
    {
        return health;
    }

    public float getShields()
    {
        return shields;
    }

    public void die()
    {
        if (isServer)
        {
            rpcDie();
        }
        else
        {
            cmdDie();
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
    }
    
    private void onDeath(PlayerScript p)
    {
        if (isServer && p == this)
        {
            foreach (NetworkIdentity i in backpack.GetComponentsInChildren<NetworkIdentity>())
            {
                if (i.GetComponent<meleescript>() == null)
                {
                    tryDrop(i);
                }
            }
            List<NetworkStartPosition> positions = FindObjectsOfType<NetworkStartPosition>().ToList<NetworkStartPosition>();
            spawn(positions[(int)(Random.value * positions.Count)].transform.position);  
        }
        health = DefaultHealth;
        aud.PlayOneShot(DieSound);
    }

    [ClientRpc]
    private void spawn(Vector3 spawnpos)
    {
        if(isServer)
        {
            RpcCorrectPlayerPos(spawnpos);
        }
        transform.position = spawnpos;
    }

    public void takeDamagefromHit(float d, float m)
    {
        if(!isServer)
        {
            cmdTakeDamageFromHit(d, m);
        }
        else
        {
            health -= d;
            rpcSyncDamageFromHit(health, shields);

            if (health < 0 && isDead == false)
            {
                die();
            }

        }
    }

    [Command]
    private void cmdTakeDamageFromHit(float d, float m)
    {
        health -= d;
        rpcSyncDamageFromHit(health, shields);

        if (health < 0)
        {
            die();
        }
    }

    [ClientRpc]
    private void rpcSyncDamageFromHit(float h, float s)
    {
        health = h;
        shields = s;
        if (isLocalPlayer)
        {
            LocalAud.PlayOneShot(localDamageSound);
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


        if (!hand)
        {
            myWeapon = null;
        }
        else
        {
            otherHand = null;
        }
    }


}