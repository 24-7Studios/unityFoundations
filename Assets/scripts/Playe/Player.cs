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

    private List<iPlayable> myplayables = new List<iPlayable>();

    private int activePlayableIndex;

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

    public bool activate(iPlayable play)
    {
        if(myplayables.Contains(play))
        {
            activePlayableIndex = myplayables.IndexOf(play);
            myplayables[activePlayableIndex].ActivatePlayer();
            return true;
        }
        return false;
    }

    public void addPlayable(iPlayable play)
    {
        if(!myplayables.Contains(play))
            myplayables.Add(play);
    }

    public void removePlayable(iPlayable play)
    {
        if (myplayables.Contains(play))
            myplayables.Remove(play);
    }

    public iPlayable getActive()
    {
        if(activePlayableIndex != -1)
        {
            return myplayables[activePlayableIndex];
        }

        return null;
    }

    private string retrieveUsername()
    {
        return null;
    }

}
