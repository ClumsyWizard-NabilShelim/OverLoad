using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{
	public string Name;

	public AudioClip Clip;

	[Range(0, 1)]
	public float volume;
	[Range(0, 3)]
	public float pitch;

	public bool loop;
	[HideInInspector]
	public AudioSource source;

}
