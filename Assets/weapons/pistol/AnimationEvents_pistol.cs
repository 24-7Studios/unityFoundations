using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents_pistol : MonoBehaviour
{

    public PistolScript pistol;
	public AudioSource source;
	public bool idle;
	public bool his;


	private void Start()
	{


		source = GetComponentInParent<AudioSource>();


	}

	private void Update()
	{

		if(!pistol.anim.GetBool("isCooling") && his)
		{

			source.Pause();
			source.clip = null;
			his = false;
			

		}
		
		


	}


	public void shoot(AudioClip a)
	{


		playSound(a);
		idle = false;

	}



	public void slideClose(AudioClip a)
	{

		playSound(a);
		idle = false;

	}


	public void slideOpen(AudioClip a)
	{
		playSound(a);
		idle = false;

	}

	public void hiss(AudioClip a)
	{
		idle = false;
		his = true;
		source.clip = a;
	
		
		source.Play();
		
		
		

	}


	public void Idle()
	{
		idle = true;
	}

	public void playSound(AudioClip sound)
	{


		source.PlayOneShot(sound);


	}
}
