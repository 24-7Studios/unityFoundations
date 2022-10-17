using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Users;
using Cinemachine;
using Mirror;


public class Player : NetworkBehaviour
{

    private string UserName;

    private List<iPlayable> playables = new List<iPlayable>();

    private Transform myTransfrom;

    private PlayerInput myInput;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private string retrieveUsername()
    {
        return null;
    }

}
