using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class hitbox : MonoBehaviour
{
    [SerializeField]
    private GameObject mainObject;

    [SerializeField]
    private float defaultMultiplier;

    [SerializeField]
    private string type;

    
    public void setObject(GameObject i)
    {
        mainObject = i;
    }

    public GameObject getObject()
    {
        return mainObject;
    }

    public float getMultiplier()
    {
        return defaultMultiplier;
    }
    
    public string getType()
    {
        return type;
    }
}

