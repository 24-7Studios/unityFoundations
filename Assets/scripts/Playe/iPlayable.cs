using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iPlayable
{
    private Player myPlayer { get; set; }

    public void Possess(Player NewPlayer)
    {
        
    }

    public Player DePlayer()
    {
        return myPlayer;
        myPlayer = null;
    }
}
