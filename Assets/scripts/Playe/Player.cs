using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Users;
using Cinemachine;
using Mirror;
using UnityEngine.Rendering.Universal;



public class Player : MonoBehaviour
{

    private string UserName;

    private int playerIndex;

    [SerializeField] private Camera myMainCamera;
    [SerializeField] private Camera myModelCamera;
    [SerializeField] private GameObject blankPlayable;

    private int viewModel;
    private int playermodel;
    private int myHitbox;
    private LayerMask Shootable;


    private List<iPlayable> myplayables = new List<iPlayable>();

    private int activePlayableIndex;

    private Transform myTransfrom;

    private PlayerInput myPlayerInput;



    private void Awake()
    {
        myPlayerInput = GetComponent<PlayerInput>();
        playerIndex = myPlayerInput.playerIndex;
        myMainCamera.GetUniversalAdditionalCameraData().cameraStack.Add(myModelCamera);
    }

    private void Start()
    {
        ((myNetworkManager)myNetworkManager.singleton).addPlayer(PlayerManager.getInstance().getPlayable());
        //Debug.Log("created player object");
        /*
        iPlayable p = ((myNetworkManager)myNetworkManager.singleton).addPlayer(PlayerManager.getInstance().getPlayable()).GetComponent<iPlayable>();
        myplayables.Add(p);
        myplayables[0].addPlayer(this);
        myplayables[0].ActivatePlayer();
        */
    }


    public void setViewmodelLayer(int l)
    {
        viewModel = l;
    }

    public void setPayermodelLayer(int l)
    {
        playermodel = l;
    }

    public void setHitboxLayer(int l)
    {
        myHitbox = l;
    }

    public void setShootableLayer(LayerMask l)
    {
        Shootable = l;
    }

    public void setViewmodelCamCulling(LayerMask l)
    {
        myModelCamera.cullingMask = l;
    }

    public void setWorldCamCulling(LayerMask l)
    {
        myMainCamera.cullingMask = l;
    }

    public int getViewmodelLayer()
    {
        return viewModel;
    }

    public int getPlayermodelLayer()
    {
        return playermodel;
    }

    public int getHitboxLayer()
    {
        return myHitbox;
    }

    public LayerMask getShootableLayer()
    {
        return Shootable;
    }

    public void setCamRect(Rect newRect)
    {
        myMainCamera.rect = newRect;
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

    private string retrieveUsername()
    {
        return null;
    }

}

public static class coolFunctions
{
    public static void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach(Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}

