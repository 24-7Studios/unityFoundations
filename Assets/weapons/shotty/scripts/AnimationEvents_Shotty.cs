using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents_Shotty : MonoBehaviour
{

	public AudioSource source;
	public Shooty gun;



	private void Start()
	{


		source = GetComponentInParent<AudioSource>();
		

	}



	public void hasFired(AudioClip sound)
	{


		gun.hasFired = true;
		playSound(sound);

	}

	public void pumpBack(AudioClip sound)
	{


		gun.pumpBack = true;
		playSound(sound);

	}

	public void pumpForward(AudioClip sound)
	{


		gun.pumpForward = true;
		playSound(sound);

	}

	public void playSound(AudioClip sound)
	{


		source.clip = sound;
		source.Play();


	}










}
