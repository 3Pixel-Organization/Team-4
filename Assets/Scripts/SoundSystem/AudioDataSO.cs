using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AudioManager/Audio data")]
public class AudioDataSO : ScriptableObject
{
	public AudioClip audioClip;

	private GameObject gameObject;
	private AudioSource audioSource;

	public void Play()
	{
		if(gameObject != null && audioSource != null)
		{
			audioSource.Stop();
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
