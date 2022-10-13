using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iPlayable
{

    public void addPlayer(Player newPlayer);

    public Player removePlayer();

    public void attatchPlayer();

    public void detatchPlayer();

    public Player ActivatePlayer();

    public Player DeactivatePlayer();


}
