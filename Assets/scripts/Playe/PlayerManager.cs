using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    private static PlayerInputManager playerinputmanager;
    private static PlayerManager playermanager;
    private List<Player> playerList;


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
        playerList.Add(p.GetComponent<Player>());
    }

    public void OnPlayerDropped(PlayerInput p)
    {
        Player player = p.transform.GetComponent<Player>();
        playerList.Remove(player);
    }
}
