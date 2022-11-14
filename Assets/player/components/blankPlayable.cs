using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Users;
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
        removePlayer();
        myPlayer = newPlayer;
    }

    public Player removePlayer()
    {
        DeactivatePlayer();
        detatchPlayer();
        return myPlayer = null;
    }

    public void attatchPlayer()
    {
        myPlayer.transform.SetParent(this.transform);
    }

    public void detatchPlayer()
    {
        myPlayer.transform.SetParent(null);
    }

    public Player ActivatePlayer()
    {
        myPlayer.setCamRect();
        myPlayer.setLayers();
        this.gameObject.SetActive(true);
        return myPlayer;
    }

    public Player DeactivatePlayer()
    {
        this.gameObject.SetActive(false);
        return myPlayer;
    }


}
