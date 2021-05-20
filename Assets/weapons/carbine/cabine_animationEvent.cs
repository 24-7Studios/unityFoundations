using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cabine_animationEvent : MonoBehaviour
{

    public carbine carbine;
    public AudioSource source;




    // Start is called before the first frame update
    void insert(AudioClip a)
    {

        carbine.hasLoaded = true;
        playSound(a);

    }

    void back(AudioClip a)
    {
        playSound(a);
    }

    void forward(AudioClip a)
	{
        playSound(a);
	}


    void fire(AudioClip a)
    {
        playSound(a);
    }

    void extract(AudioClip a)
    {
        playSound(a);
    }

    public void playSound(AudioClip sound)
    {


        source.PlayOneShot(sound);


    }
}
