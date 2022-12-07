using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private GameObject defaultIPlayable;
    [SerializeField] private LayersPlayer PlayerLayers;

    private static PlayerInputManager playerinputmanager;
    private static PlayerManager playermanager;
    private List<Player> playerList;


    public static PlayerInputManager getInputManager()
    {
        return playerinputmanager;
    }

    public static PlayerManager getInstance()
    {
        return playermanager;
    }

    public GameObject getPlayable()
    {
        return defaultIPlayable;
    }

    private void Awake()
    {

        playerList = new List<Player>();

        if(playerinputmanager != null)
        {
            Debug.LogError("There is already a PlayerInputManager!");
        }
        else
        {
            playerinputmanager = gameObject.GetComponent<PlayerInputManager>();
            DontDestroyOnLoad(playerinputmanager.gameObject);
        }

        if(playermanager != null)
        {
            Debug.LogError("There is already a PlayerManager!");
        }
        else
        {
            playermanager = this;
            DontDestroyOnLoad(playermanager.gameObject);
        }


    }

    public void OnPlayerJoined(PlayerInput p)
    {
        //Debug.Log(p.GetComponent<Player>());
        Player pl = p.GetComponent<Player>();
        playerList.Add(pl);
        setLayers(pl, p.playerIndex);
        setPlayerRects();
    }

    public void OnPlayerDropped(PlayerInput p)
    {
        Player player = p.transform.GetComponent<Player>();
        playerList.Remove(player);
        setPlayerRects();
    }

    private void setLayers(Player p, int index)
    {
        if(index < 1)
        {
            p.setPayermodelLayer(PlayerLayers.p1pm);
            p.setViewmodelLayer(PlayerLayers.p1vm);
            p.setShootableLayer(PlayerLayers.p1sh);
        }
        else if(index < 2)
        {
            p.setPayermodelLayer(PlayerLayers.p2pm);
            p.setViewmodelLayer(PlayerLayers.p2vm);
            p.setShootableLayer(PlayerLayers.p2sh);
        }
        else if(index < 3)
        {
            p.setPayermodelLayer(PlayerLayers.p3pm);
            p.setViewmodelLayer(PlayerLayers.p3vm);
            p.setShootableLayer(PlayerLayers.p3sh);
        }
        else if(index < 4)
        {
            p.setPayermodelLayer(PlayerLayers.p4pm);
            p.setViewmodelLayer(PlayerLayers.p4vm);
            p.setShootableLayer(PlayerLayers.p4sh);
        }
    }

    public void setPlayerRects()
    {
        for(int i = 0; i < playerList.Count; i++)
        {
            playerList[i].setCamRect(rectGen(i, playerList.Count));
        }
    }

    private Rect rectGen(int ind, int num)
    {
        Rect theRect = new Rect();

        theRect.x = 0;
        theRect.y = 0;
        theRect.width = 1;
        theRect.height = 1;

        if(num > 1)
        {
            theRect.height = 0.5f;
            theRect.y = (ind == 0) ? 0.5f : 0;
            if(num > 2)
            {
                theRect.width = 0.5f;
                theRect.x = (ind == 0) ? 0.25f : (ind == 1) ? 0 : 0.5f;

                if(num > 3)
                {
                    theRect.y = (ind < 2) ? 0.5f : 0;
                    theRect.x = (ind % 2 == 0) ? 0 : 0.5f;
                }
            }
        }

        return theRect;
    }
}
