using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Users;
using Cinemachine;
using Mirror;



public class Player : MonoBehaviour
{

    private string UserName;

    private int playerIndex;

    [SerializeField] private Camera myMainCamera;
    private Vector2 cameraXY;
    private Vector2 cameraWH;


    private LayerMask viewModel;
    private LayerMask playermodel;
    private LayerMask Shootable;


    private List<iPlayable> myplayables = new List<iPlayable>();

    private int activePlayableIndex;

    private Transform myTransfrom;

    private PlayerInput myInput;


    private void Awake()
    {
        myInput = GetComponent<PlayerInput>();
        playerIndex = myInput.playerIndex;
    }

    private void setLayers(int playerindex)
    {

    }

    public void setCamRect()
    {
        Rect newRect = new Rect();

        if(PlayerInputManager.instance.playerCount > 0)
        {
            newRect.y = 0.5f;

            if(PlayerInputManager.instance.playerCount > 2)
            {
                newRect.x = 0.5f;
            }
        }

        if (playerIndex < 2 && PlayerInputManager.instance.playerCount > 0) ;
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

    public Camera getCamera()
    {
        return myMainCamera;
    }

    private string retrieveUsername()
    {
        return null;
    }

}
