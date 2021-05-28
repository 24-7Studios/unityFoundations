using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookHandler_fly : MonoBehaviour
{
    public Player player;
    public Transform cam;
    public Rigidbody body;

    public float sens = 100f;


    public float ymov = 0;
    public float xmov = 0;



    // Start is called before the first frame update
    void Start()
    {


        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {




        float MouseX = Input.GetAxisRaw("Mouse X") * sens;
        float MouseY = Input.GetAxisRaw("Mouse Y") * sens;


        ymov -= MouseY;
        ymov = Mathf.Clamp(ymov, -90f, 90f);

        xmov -= MouseX;

        cam.transform.localRotation = Quaternion.Euler(Vector3.right * ymov);
        body.transform.rotation = Quaternion.Euler(Vector3.up * -xmov);

        




    }

    public void viewPunch(float r)
	{
        ymov -= r;
	}


}
