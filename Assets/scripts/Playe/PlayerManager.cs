using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : ScriptableObject
{


    private PlayerInputManager playerinputmmanager;
    private List<Player> playerList;


    private void addPlayer(Player p)
    {
        playerList.Add(p);
    }

}
