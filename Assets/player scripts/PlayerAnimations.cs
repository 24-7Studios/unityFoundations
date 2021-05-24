using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    public Animator anim;
    public movement_fly moves;
    public float velocity;
    public float damp = 5;

   
    // Update is called once per frame
    void Update()
    {

        velocity = moves.body.transform.InverseTransformDirection(moves.body.velocity).z;


        anim.SetFloat("speed", velocity / (moves.moveSpeed * 17), damp, Time.fixedDeltaTime);




    }






}
