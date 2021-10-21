using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MenuCamera : MonoBehaviour
{
    [SerializeField]
    private Camera menuCam;
    private AudioListener audioL;

    // Start is called before the first frame update
    void Start()
    {
        audioL = menuCam.GetComponent<AudioListener>();
    }

    // Update is called once per frame
    void Update()
    {
     
        if(NetworkClient.active)
        {
            menuCam.enabled = false;
            audioL.enabled = false;
        }
        else
        {
            menuCam.enabled = true;
            audioL.enabled = true;
        }
    }
}
