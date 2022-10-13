using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Player : NetworkBehaviour
{

    private string UserName;

    private List<iPlayable> playables = new List<iPlayable>();


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private string retrieveUsername()
    {
        return null;
    }

}
