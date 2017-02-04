using System;
using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private static SoundManager instance;
	public static SoundManager Instance{
		get{ 
			if (instance == null) {
				instance = FindObjectOfType<SoundManager> ();
			}
			return instance;
		}
	}

    public AudioClip[] SoundClips;
	public AudioClip[] BackgroundMusic;
	public GameObject Player;
	public AudioSource BackgroundSource;
	[Range(0,1)]
	public float BackgroundVolume;
	[Range(0,1)]
	public float SoundVolume;

	public void changeBackgroundMusic(int index){
		BackgroundSource.clip = BackgroundMusic [index];
		BackgroundSource.loop = true;
		BackgroundSource.Play ();
	}

	public void playSoundAt(int index, Transform transform)
	{
		if (index >= 0 && index < SoundClips.Length) {
			AudioSource source = new GameObject().AddComponent<AudioSource> ();
			source.transform.position = transform.position;
			source.volume = SoundVolume;
			source.PlayOneShot (SoundClips[index]);

			int duration = (int)Math.Ceiling(SoundClips[index].length);
			StartCoroutine(DestroyGameObject(source.gameObject, duration));
		}
	}

	public void playSound(int index)
    {
		if (index >= 0 && index < SoundClips.Length) {
			AudioSource source = Player.AddComponent<AudioSource> ();
			source.volume = SoundVolume;
			source.PlayOneShot (SoundClips[index]);

			int duration = (int)Math.Ceiling(SoundClips[index].length);
			StartCoroutine(DestroyAudioSource(source, duration));
		}
    }

	IEnumerator DestroyAudioSource(AudioSource source, int t)
	{
		//Waits for the specified time and continues afterwards
		yield return new WaitForSeconds(t);
		if (source != null) {
			Destroy (source);
		}
	}

	IEnumerator DestroyGameObject(GameObject tempObject, int t)
	{
		//Waits for the specified time and continues afterwards
		yield return new WaitForSeconds(t);
		if (tempObject != null) {
			Destroy (tempObject);
		}
	}

	void Start(){
		if(BackgroundSource!=null){
			BackgroundSource.volume = BackgroundVolume;
		}
	}

	void Update(){
		if(BackgroundSource!=null){
			BackgroundSource.volume = BackgroundVolume;
		}
	}
}
