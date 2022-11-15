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
    [SerializeField] private GameObject blankPlayable;

    private LayerMask viewModel;
    private LayerMask playermodel;
    private LayerMask Shootable;


    private List<iPlayable> myplayables = new List<iPlayable>();

    private int activePlayableIndex;

    private Transform myTransfrom;

    private PlayerInput myPlayerInput;
    private Inputmaster myInputMaster;


    private void Awake()
    {
        myPlayerInput = GetComponent<PlayerInput>();
        myInputMaster = new Inputmaster();
        playerIndex = myPlayerInput.playerIndex;
    }

    private void Start()
    {
        myplayables.Add(Instantiate(blankPlayable).GetComponent<iPlayable>());
        myplayables[0].addPlayer(this);
        myplayables[0].ActivatePlayer();
    }

    public void setLayers()
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

    public PlayerInput getPlayerInput()
    {
        return myPlayerInput;
    }

    public Inputmaster getInputMaster()
    {
        return myInputMaster;
    }

    private string retrieveUsername()
    {
        return null;
    }

}
