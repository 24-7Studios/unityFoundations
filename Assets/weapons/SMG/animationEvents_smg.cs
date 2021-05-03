using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationEvents_smg : MonoBehaviour
{


    public AudioSource source;



    private void Start()
	{


		source = GetComponentInParent<AudioSource>();


	}




    public void shoot(AudioClip a)
    {


        playSound(a);
   

    }


    public void playSound(AudioClip sound)
    {


        source.PlayOneShot(sound);


    }


}
