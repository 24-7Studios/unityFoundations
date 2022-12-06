using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Users;
using Cinemachine;
using Mirror;

public class PlayerScript : NetworkBehaviour, IDamage, iPlayable
{

    //player setup
    /// <summary>
    /// components that make the player work. also likely to be referenced by other scripts especially in the guns
    /// </summary>

    private Player myPlayer;

    [SerializeField]
    private PlayerStuffScriptableObject parameters;

    //[SerializeField]
    private Rigidbody playerPhysBody;

    private Collider playerCollider;

    [SerializeField]
    private Transform camTransformer;

    [SerializeField]
    private GameObject CameraSetup;

    //[SerializeField]
    private AudioSource aud;

    //[SerializeField]
    private AudioSource LocalAud;

    [SerializeField]
    private Transform virtualCam;

    [SerializeField]
    private Transform corpseVirtualCam;

    [SerializeField]
    private AudioListener audioListener;

    [SerializeField]
    private interactZoneScript interactZone;

    [SerializeField]
    private PlayerModelClass SelectedPlayerModel;

    [SerializeField]
    private PlayerModelClass LivePlayerModel;

    [SerializeField]
    private Light flashL;



    //mouse and camera effects
    /// <summary>
    /// pee pee poo poo what do u think it is
    /// </summary>

    private float sens;

    private float yMouseInput = 0;

    private float xMouseInput = 0;

    [SerializeField]
    private Transform camEffector;

    private float viewpunchAttack = 1;

    private float viewPunchRecovery = 4.25f;

    private float CurrentPunch = 0;

    private float TotalPunch = 0;

    private float AppliedPunch;

    [SerializeField]
    private float MaxPush = 0.05f;

    private float viewPushAttack;

    private float viewPushRecovery;

    private float currentPush;

    private float totalPush;

    private float appliedPush;

    private float recoilAttack;

    private float recoilSmoothing;

    private float Totalrecoil;

    private float currentRecoil;

    private Vector3 defaultCameraPos;
    private Vector3 defaultCameraRot;




    //movement
    /// <summary>
    /// stuff with movement. mostly just modifiers and some stuff with network syncing
    /// </summary>


    [SerializeField]
    private float currentMoveForce;

    [SerializeField]
    private float currenGravity;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private Transform foot;

    private Vector3 footPostition;

    float SyncTimer = 0;

    [SerializeField]
    private bool usePhysicsGravity = false;

    int currentJump = 0;
    bool doJump = false;
    bool grounded;
    //bool 
    float x = 0;
    float z = 0;
    float y = 0;
    Vector3 BasicInputMovement;
    Vector3 RelativeInputMovement;
    Vector3 groundNormal;



    //health

    [SyncVar]
    private float health;

    [SyncVar]
    private float shields;

    [SerializeField]
    private bool instantDeath;

    [SerializeField, SyncVar]
    private bool invinsible;

    [SyncVar]
    private bool isDead;

    public delegate void Spawned(PlayerScript player);
    public static Spawned spawned;

    public delegate void StandBy(PlayerScript player);
    public static StandBy standby;

    public delegate void Enable(PlayerScript player);
    public static Enable Renable;

    public delegate void Died(PlayerScript player);
    public static Died died;

    [SerializeField]
    private AudioClip localDamageSound;

    [SerializeField]
    private AudioClip DieSound;

    [SerializeField]
    private float respawnTime = 5;

    private float respawnTimer = 0;

    private bool awaitingSpawn = true;

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
    private PlayerInput input;
    private InputAction looking;
    private InputAction moving;

    public Vector3 floornormal;


    public void addPlayer(Player newPlayer)
    {
        if (myPlayer != null)
        {
            removePlayer();
        }
        myPlayer = newPlayer;
    }

    public Player removePlayer()
    {
        DeactivatePlayer();
        detatchPlayer();
        return myPlayer = null;
    }

    public void attatchPlayer()
    {
        myPlayer.transform.SetParent(camEffector);
    }

    public void detatchPlayer()
    {

    }

    public Player ActivatePlayer()
    {
        attatchPlayer();
        //myPlayer.setLayers();
        input = myPlayer.getPlayerInput();
        bindControls();
        this.gameObject.SetActive(true);
        return myPlayer;
    }

    public Player DeactivatePlayer()
    {
        this.gameObject.SetActive(false);
        return myPlayer;
    }

    private void bindControls()
    {
        input = myPlayer.getPlayerInput();
        looking = input.actions.FindAction("looking");
        moving = input.actions.FindAction("Movement");
        input.actions.FindAction("jump").performed += ctx => activateJump();
        input.actions.FindAction("Change").performed += ctx => changeSlot();
        input.actions.FindAction("interact").started += ctx => interact();
        input.actions.FindAction("interact").performed += ctx => manualPickup();
        input.actions.FindAction("toggleLight").performed += ctx => toggleFlashlight();
        input.actions.FindAction("dual").performed += ctx => manualPickup(true);
        input.actions.FindAction("drop").performed += ctx => manualDrop();
        input.actions.FindAction("melee").performed += ctx => equipToMelee();
        input.actions.FindAction("kill").performed += ctx => die();



        input.ActivateInput();
        input.enabled = true;
        foreach(InputAction inp in input.actions)
        {
            inp.Enable();
        }
    }


    private void Awake()
    {
        //gets components
        playerPhysBody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        aud = GetComponent<AudioSource>();

       
        died += onDeath;
    }

    private void Start()
    {
        health = parameters.playerHealth.defaultHealth;


        for (int i = 0; i <= numOfSlots; i++)
        {
            Slot s = new Slot(i);
            WeaponSlots.Add(s);
        }

        meleeSlot = WeaponSlots[0];
        primarySlot = WeaponSlots[1];
        secondarySlot = WeaponSlots[2];

        equipedSlot = meleeSlot;

        if (IsLocalPlayer())
        {

            //controls.Player.Enable();

            //input.enabled = true;

            //LocalAud = Instantiate(CameraSetup, camEffector).GetComponent<AudioSource>();

            Cursor.lockState = CursorLockMode.Locked;

        }

        footPostition = foot.localPosition;

    }


    private void OnConnectedToServer()
    {
        cmdSyncSlots(equipedSlot.getIndex(), previousSlot.getIndex());
    }

    private void Update()
    {

        if(isServer)
        {
            if (awaitingSpawn)
            {
                respawnTimer -= Time.deltaTime;
                if (respawnTimer <= 0)
                {
                    List<NetworkStartPosition> positions = FindObjectsOfType<NetworkStartPosition>().ToList<NetworkStartPosition>();
                    spawn(positions[(int)(Random.value * positions.Count)].transform.position);
                }
            }
        }




        playerPhysBody.drag = parameters.playerMovement.playerDrag;
        playerPhysBody.angularDrag = parameters.playerMovement.playerAngularDrag;
        playerPhysBody.mass = parameters.playerMovement.playerMass;



        //if(IsLocalPlayer())
        {
            Debug.Log(this.connectionToServer);
            Debug.Log(this.hasAuthority);
            handleMouseInput();
            handleCameraTilt();
            handleViewShift();
            handleViewSway();
            handleViewPush();
        }


        ///////////////////////////////////////////////////////////////////////////////////////


        grounded = checkSlope();


        if (IsLocalPlayer())
        {
            x = moving.ReadValue<Vector2>().x;
            z = moving.ReadValue<Vector2>().y;





            BasicInputMovement = calculateBasicInputMovement();

            RelativeInputMovement = calculateRelativeInputMovement();
            

            if (isServer)
            {
                RpcSyncPlayerInput(BasicInputMovement, RelativeInputMovement);
            }
            else
            {
                CmdSyncPlayerInput(BasicInputMovement, RelativeInputMovement);
            }
        }

        ////////////////////////////////////////////////////////
        //backpack 

        if (IsLocalPlayer() && !awaitingSpawn)
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
    }


    public void FixedUpdate()
    {
        handleGravity();

        doMovement();
        

        if (IsLocalPlayer())
        {
            checkJump();

            if (!isServer)
            {
                CmdMovePlayer(RelativeInputMovement, playerPhysBody.position);
            }
            else
            {
                RpcSyncPlayerPosistion(RelativeInputMovement, playerPhysBody.position);
            }
        }


    }

    private void handleMouseInput()
    {
        float MouseX = 0;
        float MouseY = 0;

        if (input.currentControlScheme != null)
        {
            if (input.currentControlScheme.Equals("gamepad"))
            {
                MouseX = looking.ReadValue<Vector2>().x * parameters.playerOptions.controllerSens * Time.deltaTime;
                MouseY = looking.ReadValue<Vector2>().y * parameters.playerOptions.controllerSens * Time.deltaTime;
            }
            else
            {
                MouseX = looking.ReadValue<Vector2>().x * parameters.playerOptions.MouseSens * Time.deltaTime;
                MouseY = looking.ReadValue<Vector2>().y * parameters.playerOptions.MouseSens * Time.deltaTime;
            }
        }


        yMouseInput -= MouseY;
        yMouseInput = Mathf.Clamp(yMouseInput, -90f, 90f);
        xMouseInput -= MouseX;


        currentRecoil = Mathf.Lerp(currentRecoil, currentRecoil + Totalrecoil, recoilAttack * Time.deltaTime);
        Totalrecoil = Mathf.Lerp(Totalrecoil, 0, recoilSmoothing * Time.deltaTime);
        yMouseInput += Totalrecoil * Time.deltaTime * recoilAttack;

        CurrentPunch = Mathf.Lerp(CurrentPunch, TotalPunch, Time.deltaTime * viewpunchAttack);
        TotalPunch = Mathf.Lerp(TotalPunch, 0, Time.deltaTime * viewPunchRecovery);
        AppliedPunch = (TotalPunch + CurrentPunch) / 2;

        float yMouseTotal = yMouseInput + AppliedPunch;
        float xMouseTotal = xMouseInput;

        camTransformer.transform.localRotation = Quaternion.Euler((yMouseTotal) * Vector3.right);

        playerPhysBody.transform.rotation = Quaternion.Euler(Vector3.up * -xMouseTotal);

        CmdSyncPlayerRotation(yMouseTotal, xMouseTotal);
    }

    private void handleViewPush()
    {
        totalPush = Mathf.Clamp(totalPush, -totalPush, totalPush);
        currentPush = Mathf.Lerp(currentPush, totalPush, viewPushAttack * Time.deltaTime);
        totalPush = Mathf.Lerp(totalPush, 0, viewPushRecovery * Time.deltaTime);

        camTransformer.localPosition = -Vector3.forward * currentPush + defaultCameraPos;
        
    }

    public void viewPush(float p, float a, float r)
    {
        totalPush += p;
        viewPushAttack = a;
        viewPushRecovery = r;
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

    private void handleCameraTilt()
    {
        Quaternion targetTilt = Quaternion.AngleAxis(-BasicInputMovement.x * parameters.playerOptions.tiltAmount, Vector3.forward);
        camEffector.localRotation = Quaternion.Slerp(camEffector.localRotation, targetTilt, Time.deltaTime * parameters.playerOptions.tiltSmoothing);
    }

    private void handleViewSway()
    {
        Quaternion swayX = Quaternion.AngleAxis(looking.ReadValue<Vector2>().y * parameters.playerOptions.MouseSens * parameters.playerOptions.viewmodelSwayFactor, Vector3.right);
        Quaternion swayY = Quaternion.AngleAxis(looking.ReadValue<Vector2>().x * parameters.playerOptions.MouseSens * parameters.playerOptions.viewmodelSwayFactor, Vector3.up);
        Quaternion targetSway = swayX * swayY;
        viewmodelHolder.localRotation = Quaternion.Slerp(viewmodelHolder.localRotation, targetSway, parameters.playerOptions.viewmodelSwaySmoothing * Time.deltaTime);
    }

    private void handleViewShift()
    {
        Vector3 targetShift = BasicInputMovement * parameters.playerOptions.viewmodelShiftFactor;
        viewmodelHolder.localPosition = Vector3.Lerp(viewmodelHolder.localPosition, targetShift, parameters.playerOptions.viewmodelShiftSmoothing * Time.deltaTime);
    }

    private void handleGravity()
    {
        if(grounded)
        {
            currentJump = 0;
            y = -parameters.playerMovement.groundingForce;
        }
        else
        {
            playerPhysBody.velocity -= (-parameters.playerMovement.playerGravity) * (playerPhysBody.transform.up);
            if (currentJump == 0)
            {
                currentJump++;
            }
        }
    }

    private bool checkSlope()
    {
        bool on;

        RaycastHit FloorSnap;


        if (Physics.Raycast(groundCheck.position, -groundCheck.up, out FloorSnap) && !((Mathf.Abs(FloorSnap.normal.x) > parameters.playerMovement.maxAngle/90) || (Mathf.Abs(FloorSnap.normal.z) > parameters.playerMovement.maxAngle/90)) && checkFloor())
        {
            Quaternion toRotation = Quaternion.FromToRotation(transform.up, FloorSnap.normal) * transform.rotation;
            groundCheck.rotation = toRotation;
            on = true;
        }
        else
        {
            groundCheck.rotation = playerPhysBody.rotation;
            on = false;
        }

        floornormal = FloorSnap.normal;
        foot.rotation = groundCheck.rotation;
        foot.localPosition = footPostition;
        return on;

    }

    public bool checkFloor()
    {
        return Physics.CheckSphere(groundCheck.position, parameters.playerMovement.groundDistance, parameters.playerMovement.Jumpable);
    }

    public bool CanJump()
    {
        return (grounded || currentJump < parameters.playerMovement.jumps);
    }

    private void checkJump()
    {
        if (doJump)
        {
            if (playerPhysBody.velocity.y < 0)
            {
                playerPhysBody.velocity = (transform.up * parameters.playerMovement.jumpForce) + playerPhysBody.velocity.x * Vector3.right + playerPhysBody.velocity.z * Vector3.forward;
            }
            else
            {
                playerPhysBody.velocity += transform.up * parameters.playerMovement.jumpForce;
            }
            currentJump++;
            doJump = false;

            if (!isServer)
            {
                CmdJump();
            }
        }
    }

    void activateJump()
    {
        if(CanJump() && !doJump)
        {
            doJump = true;
        }
    }

    [Command]
    void CmdJump()
    {
        if (playerPhysBody.velocity.y < 0)
        {
            playerPhysBody.velocity = (transform.up * parameters.playerMovement.jumpForce) + playerPhysBody.velocity.x * Vector3.right + playerPhysBody.velocity.z * Vector3.forward;
        }
        else
        {
            playerPhysBody.velocity += transform.up * parameters.playerMovement.jumpForce;
        }
        RpcJump();
    }

    [ClientRpc]
    void RpcJump()
    {
        if (IsLocalPlayer()) return;

        if (playerPhysBody.velocity.y < 0)
        {
            playerPhysBody.velocity = (transform.up * parameters.playerMovement.jumpForce) + playerPhysBody.velocity.x * Vector3.right + playerPhysBody.velocity.z * Vector3.forward;
        }
        else
        {
            playerPhysBody.velocity += transform.up * parameters.playerMovement.jumpForce;
        }
    }

    private Vector3 calculateRelativeInputMovement()
    {
        if (grounded)
        {
            return ((((groundCheck.transform.right) * x + (groundCheck.transform.forward) * z) * parameters.playerMovement.moveForce) + groundCheck.transform.up * y);
        }
        else
        {
            return ((((groundCheck.transform.right) * x + (groundCheck.transform.forward) * z) * parameters.playerMovement.moveForce) * parameters.playerMovement.airStrafeModifier + groundCheck.transform.up * y);
        }
    }

    private Vector3 calculateBasicInputMovement()
    {
        return (Vector3.right * x) + (Vector3.up * y) + (Vector3.forward * z);
    }

    public Vector3 getBasicInputMovement()
    {
        return BasicInputMovement;
    }

    private Vector3 calculatePlayerFriction()
    {
        return -playerPhysBody.velocity * parameters.playerMovement.playerGroundFriction;
    }

    private void doMovement()
    {
        playerPhysBody.AddForce(RelativeInputMovement, ForceMode.VelocityChange);

        if (grounded)
        {
            playerPhysBody.AddForce(calculatePlayerFriction(), ForceMode.Acceleration);
        }
    }

    [Command]
    void CmdMovePlayer(Vector3 IM, Vector3 clientPos)
    {


        if (!IsLocalPlayer() && Vector3.Distance(playerPhysBody.position, clientPos) > parameters.playerMovement.PostionSnapThreshold)
        {

            RpcCorrectPlayerPos(playerPhysBody.position);

        }
        else if (!IsLocalPlayer() && Vector3.Distance(playerPhysBody.position, clientPos) < parameters.playerMovement.PostionSnapThreshold)
        {

            SyncTimer -= Time.deltaTime;

            if (SyncTimer < parameters.playerMovement.SyncInterval)
            {
                SyncTimer = parameters.playerMovement.SyncInterval;
                playerPhysBody.position = Vector3.Lerp(playerPhysBody.position, clientPos, Time.fixedDeltaTime / parameters.playerMovement.PositionCompensationDamping);
            }
        }

        RpcSyncPlayerPosistion(IM, playerPhysBody.position);

    }


    [ClientRpc]
    void RpcSyncPlayerPosistion(Vector3 IM, Vector3 serverPos)
    {

        if (!IsLocalPlayer() && Vector3.Distance(playerPhysBody.position, serverPos) < parameters.playerMovement.PostionSnapThreshold)
        {
            playerPhysBody.AddForce(IM, ForceMode.VelocityChange);
            if (grounded)
            {
                playerPhysBody.AddForce(calculatePlayerFriction(), ForceMode.Acceleration);
            }
            playerPhysBody.position = Vector3.Lerp(playerPhysBody.position, serverPos, Time.fixedDeltaTime / parameters.playerMovement.PositionCompensationDamping);
        }
        else if (!IsLocalPlayer())
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

    //[Command]
    //void CmdSyncPlayerRotation(Vector3 rot)


    [ClientRpc]
    void RpcSyncPlayerRotation(float y, float x)
    {

        if (!IsLocalPlayer())
        {

            camTransformer.transform.localRotation = Quaternion.Euler(Vector3.right * y);
            playerPhysBody.transform.rotation = Quaternion.Euler(Vector3.up * -x);


        }

    }

    //[ClientRpc]
    //void RpcSyncPlayerRotation(Vector3 rot)


    [Command]
    void CmdSyncPlayerInput(Vector3 BIM, Vector3 IM)
    {
        BasicInputMovement = BIM;
        RelativeInputMovement = IM;

        RpcSyncPlayerInput(BasicInputMovement, RelativeInputMovement);
    }

    [ClientRpc]
    void RpcSyncPlayerInput(Vector3 BIM, Vector3 IM)
    {
        if (!IsLocalPlayer())
        {
            BasicInputMovement = BIM;
            RelativeInputMovement = IM;
        }
    }

    public PlayerModelClass getSelectedPlayerModel()
    {
        return SelectedPlayerModel;
    }

    public PlayerModelClass getLivePlayerModel()
    {
        return LivePlayerModel;
    }

    public void applyPlayermodel()
    {
        if(!isServer)
        {
            cmdApplyPlayermodel();
        }
        else
        {
            if (LivePlayerModel != null)
            {
                NetworkServer.UnSpawn(LivePlayerModel.gameObject);
            }

            PlayerModelClass p = Instantiate(SelectedPlayerModel, playerPhysBody.transform);
            NetworkServer.Spawn(p.gameObject);
            LivePlayerModel = p;

            LivePlayerModel.setPlayer(this);
            rpcApplyPlayermodel(LivePlayerModel.gameObject);
        }
    }

    [Command]
    public void cmdApplyPlayermodel()
    {
        if(LivePlayerModel != null)
        {
            NetworkServer.UnSpawn(LivePlayerModel.gameObject);
        }

        //Debug.Log("spawned in playermodel");
        PlayerModelClass p = Instantiate(SelectedPlayerModel, playerPhysBody.transform);
        NetworkServer.Spawn(p.gameObject);
        LivePlayerModel = p;

        LivePlayerModel.setPlayer(this);
        rpcApplyPlayermodel(LivePlayerModel.gameObject);
    }

    [ClientRpc]
    public void rpcApplyPlayermodel(GameObject model)
    {
        clientApplyPlayermodel(model);
    }

    public  void clientApplyPlayermodel(GameObject model)
    {
        LivePlayerModel = model.GetComponent<PlayerModelClass>();
        Debug.Log(LivePlayerModel);
        LivePlayerModel.setPlayer(this);
        LivePlayerModel.transform.SetParent(playerPhysBody.transform, false);


        camTransformer.position = LivePlayerModel.CameraOffset.position;
        defaultCameraPos = camTransformer.localPosition;
        defaultCameraRot = camTransformer.localRotation.eulerAngles;
    }

    public void clearLivePlayermodel()
    {
        if (LivePlayerModel != null)
        {
            LivePlayerModel = null;
        }
    }

    public void setPlayermodel(PlayerModelClass p)
    {
        SelectedPlayerModel = p;
    }

    public Transform getCamTransformer()
    {
        return camTransformer;
    }

    public Vector3 getVelocity()
    {
        return playerPhysBody.velocity;
    }

    public Rigidbody getPhysBody()
    {
        return playerPhysBody;
    }

    public Transform getBackpack()
    {
        return backpack;
    }

    public Transform getViewmodelHolder()
    {
        return viewmodelHolder;
    }
    

    /// this needs to be fixed. Is broken rn and just to compile for  testing
    public PlayerInput getInput()
    {
        return input;
    }


    public bool IsLocalPlayer()
    {
        return networkPlayerObject.getInstance().isLocalPlayer;
    }


    //////////////////////////////////
    
    private void toggleFlashlight()
    {
        flashL.enabled = !flashL.enabled;
        cmdToggFlashlight(flashL.enabled);
    }

    [Command]
    private void cmdToggFlashlight(bool on)
    {
        flashL.enabled = on;
        rpcToggFlashlight(on);
    }

    [ClientRpc]
    private void rpcToggFlashlight(bool on)
    {
        flashL.enabled = on;
    }

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
        if (equipedSlot.getWeapon() != null)
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

    public void reSyncSlots()
    {
        cmdReSyncSlots();
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

    [Command(requiresAuthority = false)]
    private void cmdReSyncSlots()
    {
        rpcSyncSlots(equipedSlot.getIndex(), previousSlot.getIndex());
    }

    [ClientRpc]
    private void rpcSyncSlots(int e, int p)
    {
        Debug.Log(this + "  " + e + ":" + p);
        if (!IsLocalPlayer())
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
        equipToMelee();
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

    private void manualPickup(bool hand)
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
                //pickup(target.getObject(), hand);
                WeaponClass weapon = target.getObject().GetComponent<WeaponClass>();
                if(weapon != null)
                {
                    if(weapon.CanDualWield() && equipedSlot.getWeapon().CanDualWield())
                    {
                        pickup(weapon.getObject(), hand);
                    }
                    else
                    {
                        pickup(weapon.getObject());
                    }
                }
                else
                {
                    pickup(target.getObject());
                }
            }
        }
    }

    public void pickup(GameObject thing)
    {
        if(isServer)
        {
            Ipickup i = thing.GetComponent<Ipickup>();

            if (i.getPlayer() != null) return;

            i.serverPickup(this);

            rpcPickup(thing);
        }
        else
        {
            cmdPickup(thing);
        }
    }

    public void pickup(GameObject gun, bool hand)
    {
        if (isServer)
        {
            WeaponClass i = gun.GetComponent<WeaponClass>();

            if (i.getPlayer() != null) return;

            i.serverPickup(this, hand);

            //rpcDualPickup(gun, hand);
            rpcPickup(gun);
        }
        else
        {
            cmdDualPickup(gun, hand);
        }
    }

    [Command]
    private void cmdPickup(GameObject thing)
    {
        Ipickup i = thing.GetComponent<Ipickup>();

        if (i.getPlayer() != null) return;

        i.serverPickup(this);

        rpcPickup(thing);
    }


    [Command]
    public void cmdDualPickup(GameObject gun, bool hand)
    {
        WeaponClass i = gun.GetComponent<WeaponClass>();

        if (i.getPlayer() != null) return;

        i.serverPickup(this, hand);

        //rpcDualPickup(gun, hand);
        rpcPickup(gun);
    }

    [ClientRpc]
    private void rpcPickup(GameObject thing)
    {
        Ipickup i = thing.GetComponent<Ipickup>();

        i.pickup(this);
    }

    /*
    [ClientRpc]
    private void rpcDualPickup(GameObject gun, bool hand)
    {
        WeaponClass i = gun.GetComponent<WeaponClass>();
        i.pickup(this, hand);
    }
    */

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
        if (equipedSlot.getWeapon() == null) return;
        if (equipedSlot == meleeSlot) return;

        if(equipedSlot.getOtherHand() != null)
        {
            tryDrop(equipedSlot.getOtherHand().netIdentity);
            return;
        }
        tryDrop(equipedSlot.getWeapon().netIdentity);
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
    }

    public void drop(NetworkIdentity net)
    {
        Ipickup i = net.GetComponent<Ipickup>();
        interactZone.remove(i);
        i.drop();
    }

    public float getHealth()
    {
        return health;
    }

    public float getShields()
    {
        return shields;
    }

    [Server]
    public void setInv(bool i)
    {
        invinsible = i;
    }


    public void die()
    {
        isDead = true;
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
        isDead = true;
        rpcDie();
    }

    [ClientRpc]
    private void rpcDie()
    {
        isDead = true;
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
        }
        if(p == this)
        {
            isDead = true;
            aud.PlayOneShot(DieSound);
            standbyForSpawn();
        }
    }


    public void standbyForSpawn()
    {
        standby.Invoke(this);
        awaitingSpawn = true;
        respawnTimer = respawnTime;
        playerPhysBody.isKinematic = true;
        playerCollider.enabled = false;
        viewmodelHolder.gameObject.SetActive(false);
        if(IsLocalPlayer())
        {
            //audioListener.enabled = false;
            //controls.Player.Disable();
            //controls.PlayerStandby.Enable();
        }
    }

    public void enable()
    {
        Renable.Invoke(this);
        playerPhysBody.isKinematic = false;
        playerCollider.enabled = true;
        viewmodelHolder.gameObject.SetActive(true);
        if(IsLocalPlayer())
        {
            //audioListener.enabled = true;
            //controls.Player.Enable();
            //controls.PlayerStandby.Disable();
        }
    }

    [ClientRpc]
    private void spawn(Vector3 spawnpos)
    {
        awaitingSpawn = false;
        isDead = false;
        if(isServer)
        {
            RpcCorrectPlayerPos(spawnpos);
        }
        transform.position = spawnpos;
        if (isServer)
        {
            applyPlayermodel();
            health = parameters.playerHealth.defaultHealth;
        }
        enable();
    }

    public void takeDamagefromHit(float d, float m)
    {
        if (invinsible) return;

        if(!isServer)
        {
            cmdTakeDamageFromHit(d, m);
        }
        else
        {
            health -= d;
            rpcSyncDamageFromHit(health, shields);
        }
    }

    [Command]
    private void cmdTakeDamageFromHit(float d, float m)
    {
        if (invinsible) return;

        health -= d;
        rpcSyncDamageFromHit(health, shields);
    }

    [ClientRpc]
    private void rpcSyncDamageFromHit(float h, float s)
    {
        health = h;
        shields = s;
        if(isServer)
        {
            if(health < 0 && !isDead)
            {
                die();
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

    public bool isDual()
    {
        return (myWeapon != null) && (otherHand != null);
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

            if(!myWeapon.CanDualWield() && otherHand != null)
            {
                otherHand.drop();
            }
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