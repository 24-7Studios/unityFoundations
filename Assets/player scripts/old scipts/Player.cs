using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{

    public Rigidbody playerPhysBody;
    public Transform camTransformer;
    public GameObject CameraSetup;
    public Camera worldCam;
    public Camera gunCam;
    //public lookHandler_fly looker;
    //public movement_fly mover;
    public GameObject weaponHolder;


    public float sens = 100f;


    public float yMouseInput = 0;
    public float xMouseInput = 0;


    private void Start()
    {

        

        if (!isLocalPlayer)
        {

            

            return;

        }

        Instantiate(CameraSetup, camTransformer);

        

        Cursor.lockState = CursorLockMode.Locked;


    }

    //input
    private void Update()
    {

       

        if (!isLocalPlayer) return;

        float MouseX = Input.GetAxisRaw("Mouse X") * sens;
        float MouseY = Input.GetAxisRaw("Mouse Y") * sens;


        yMouseInput -= MouseY;
        yMouseInput = Mathf.Clamp(yMouseInput, -90f, 90f);

        xMouseInput -= MouseX;

        camTransformer.transform.localRotation = Quaternion.Euler(Vector3.right * yMouseInput);
        playerPhysBody.transform.rotation = Quaternion.Euler(Vector3.up * -xMouseInput);

        




    }

    public void viewPunch(float r)
    {
        yMouseInput -= r;
    }



}
