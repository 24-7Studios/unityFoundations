using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    private PlayerInputManager playerinputmanager;
    private List<Player> playerList;


    private void Awake()
    {
        //playerinputmanager.playerJoinedEvent += addPlayer;
    }

    public void OnPlayerJoined(Player p)
    {
        //playerList.Add(p);
        //playerinputmanager.playerj
        Debug.Log("wtf");
    }

    public static PlayerManager GetPlayerManager()
    {
        return this;
    }
}
