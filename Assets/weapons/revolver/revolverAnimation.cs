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
    
     void revolverHolster(AudioClip a)
	{
        source.clip = a;
        source.Play();
	}
    
    void fire(AudioClip a)
	{
        playSound(a);
        revolver.hasFired = true;
	}

    void Update()
    {
        if (!gameObject.activeInHierarchy)
            {
                source.Stop();
            }
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
