using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iPlayable
{

    public void addPlayer(Player newPlayer);

    public Player removePlayer();

    public void attatch();

    public void detatch();

    public Player ActivatePlayer();

    public Player DeactivatePlayer();


}
