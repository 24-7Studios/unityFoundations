using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class interactZoneScript : MonoBehaviour
{

    
    public List<Ipickup> pickups = new List<Ipickup>();

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

    public Ipickup[] getList()
    {
        return pickups.ToArray();
    }
}
