using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookHandler_fly : MonoBehaviour
{
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




        float MouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;


        ymov -= MouseY;
        ymov = Mathf.Clamp(ymov, -90f, 90f);

        xmov -= MouseX;

        

        
               


    }


	private void FixedUpdate()
	{
        //tis.transform.rotation = Quaternion.Euler(Vector3.up * -xmov + Vector3.right * ymov);


        cam.transform.localRotation = Quaternion.Euler(Vector3.right * ymov);
        body.transform.rotation = Quaternion.Euler(Vector3.up * -xmov);




    }


}
