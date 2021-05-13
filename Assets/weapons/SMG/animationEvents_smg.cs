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

	public void draw(AudioClip a)
	{
        playSound(a);
	}


	public void magDrop(AudioClip a)
	{
        playSound(a);
	}

    public void magInsert(AudioClip a)
    {
        playSound(a);
    }

    public void shoot(AudioClip a)
    {

        
        playSound(a);
   

    }


    public void playSound(AudioClip sound)
    {

        //source.clip = sound;
        //source.Play();
        source.PlayOneShot(sound);


    }
    

}
