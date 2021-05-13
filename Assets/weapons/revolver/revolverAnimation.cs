using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolverAnimation : MonoBehaviour
{


    public revolver revolver;
    public AudioSource source;




    // Start is called before the first frame update
    void insert(AudioClip a)
    {

        revolver.hasLoaded = true;
        playSound(a);

    }

    void click(AudioClip a)
	{
        playSound(a);
	}
    
    void fire(AudioClip a)
	{
        playSound(a);
        revolver.hasFired = true;
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
