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
        if(playerinputmanager != null)
        {
            Debug.LogError("There is already a PlayerInputManager!");
        }
        else
        {
            playerinputmanager = gameObject.GetComponent<PlayerInputManager>();
        }

        if(playermanager != null)
        {
            Debug.LogError("There is already a PlayerManager!");
        }
        else
        {
            playermanager = this;
        }
    }

    public void OnPlayerJoined(PlayerInput p)
    {
        //playerList.Add(p);
        //playerinputmanager.playerj
        Debug.Log("wtf");
        Debug.Log(p);
    }

}
