using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class interactZoneScript : MonoBehaviour
{

    private List<Ipickup> pickups = new List<Ipickup>();

    private void OnCollisionEnter(Collision collision)
    {
        Ipickup pickup = collision.gameObject.GetComponent<Ipickup>();

        if(pickup != null)
        {
            pickups.Add(pickup);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Ipickup pickup = collision.gameObject.GetComponent<Ipickup>();

        if (pickup != null)
        {
            pickups.Remove(pickup);
        }
    }

    public List<Ipickup> getList()
    {
        return pickups;
    }
}
