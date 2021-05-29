using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AudioManager/Audio data")]
public class AudioDataSO : ScriptableObject
{
	public AudioClip audioClip;

	private GameObject gameObject;
	private AudioSource audioSource;
	private const float VolumeMaxVal= 1f;
	//set default max value to 1f
	[Range(0f, VolumeMaxVal)]
	public float volSlide;

	public void Play()
	{
		if(gameObject != null && audioSource != null)
		{
			audioSource.Stop();
			audioSource.volume = volSlide;
			audioSource.Play();
			return;
		}

		gameObject = Instantiate(new GameObject());
		audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
		audioSource.clip = audioClip;

		audioSource.Play();
	}

	public void Update()
	{

	}
}
