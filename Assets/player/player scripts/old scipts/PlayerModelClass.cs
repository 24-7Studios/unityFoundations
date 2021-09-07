using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerModelClass : NetworkBehaviour
{

    public string ModelName;
    public GameObject baseCont;
    public GameObject ModelPrefab;
    public List<GameObject> models;
    protected PlayerScript player;

    public virtual void setPlayer(PlayerScript p)
    {
        player = p;
        Debug.Log("player Set!" + player);
    }

}
