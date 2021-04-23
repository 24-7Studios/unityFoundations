using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClass : MonoBehaviour
{


    public string itemName;
    public Rigidbody item;

    private Vector3 temp;




    // Start is called before the first frame update
    void Start()
    {
        


        



    }

    // Update is called once per frame
    public virtual void Update()
    {


        temp = item.transform.position;
        item.transform.localPosition = Vector3.zero;
        item.transform.parent.transform.position = temp;


    }
}
