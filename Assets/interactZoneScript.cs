using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class interactZoneScript : MonoBehaviour
{

    
    public List<Ipickup> pickups = new List<Ipickup>();
    private PlayerScript player;

    private void Awake()
    {
        player = GetComponentInParent<PlayerScript>();
    }


    private void OnTriggerEnter(Collider other)
    {
        Ipickup pickup = other.gameObject.GetComponent<Ipickup>();

        if (pickup != null)
        {
            pickups.Add(pickup);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Ipickup pickup = other.gameObject.GetComponent<Ipickup>();

        if (pickup != null)
        {
            pickups.Remove(pickup);
        }
    }

    public void remove(Ipickup i)
    {
        pickups.Remove(i);
    }

    public Ipickup[] getList()
    {
        for(int i = pickups.Count-1; i >= 0; i--)
        {
            if(pickups.ElementAt(i).getObject().transform.parent == player.getBackpack())
            {
                pickups.RemoveAt(i);
            }
        }

        return pickups.ToArray();
    }
}
