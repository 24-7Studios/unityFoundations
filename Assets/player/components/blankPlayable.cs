using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blankPlayable : MonoBehaviour, iPlayable
{

    private Player myPlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addPlayer(Player newPlayer)
    {

    }

    public Player removePlayer()
    {
       
        Player p = myPlayer;
        myPlayer = null;
        return p;
    }

    public void attatchPlayer()
    {

    }

    public void detatchPlayer()
    {

    }

    public Player ActivatePlayer()
    {
        return myPlayer;
    }

    public Player DeactivatePlayer()
    {
        return myPlayer;
    }
}
