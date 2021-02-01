using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public Sounds[] sounds;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(transform.parent.gameObject);
			return;
		}
		DontDestroyOnLoad(transform.parent.gameObject);

		foreach (Sounds s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();

			s.source.clip = s.Clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

    void Start()
    {
        
    }

	public void Play(string name)
	{
		Sounds s = Array.Find(sounds, sound => sound.Name == name);
		s.source.Play();
	}

	public void Stop(string name)
	{
		Sounds s = Array.Find(sounds, sound => sound.Name == name);
		s.source.Stop();
	}
}
