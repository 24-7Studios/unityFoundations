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

    private int playerIndex;

    private LayerMask viewModel;
    private LayerMask playermodel;
    private LayerMask Shootable;


    private List<iPlayable> myplayables = new List<iPlayable>();

    private int activePlayableIndex;

    private Transform myTransfrom;

    private PlayerInput myInput;


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

    public bool activate(int index)
    {
        if (index < myplayables.Count)
        {
            activePlayableIndex = index;
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
