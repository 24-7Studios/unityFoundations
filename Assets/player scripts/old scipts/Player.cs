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
    public GameObject weaponHolder;
    public PlayerModelClass PlayerModel;


    public float sens = 100f;


    public float yMouseInput = 0;
    public float xMouseInput = 0;


    private void Start()
    {



        if (!isLocalPlayer)
        {

            foreach (GameObject part in PlayerModel.models)
            {
                part.layer = 0;
            }

            

            return;

        }


        foreach (GameObject part in PlayerModel.models)
        {
            part.layer = 6;
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
        CmdSyncPlayerRotation();





    }

    [Command]
    void CmdSyncPlayerRotation()
    {

        camTransformer.transform.localRotation = Quaternion.Euler(Vector3.right * yMouseInput);

        playerPhysBody.transform.rotation = Quaternion.Euler(Vector3.up * -xMouseInput);

    }

    

    public void viewPunch(float r)
    {
        yMouseInput -= r;
    }



}
