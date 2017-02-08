using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// This class handles the background music and the sound effects
/// </summary>
public class SoundManager : MonoBehaviour
{

	[Tooltip("Add all needed Soundeffects here.")]
	public AudioClip[] SoundClips;

	[Tooltip("Add all needed Soundeffects here.")]
	public AudioClip[] BackgroundMusic;

	[Tooltip("The audio source of the background music.")]
	public AudioSource BackgroundSource;

	[Range(0,1)]
	public float BackgroundVolume;

	[Range(0,1)]
	public float SoundVolume;

	/// <summary>
	/// Singleton implementation.
	/// </summary>
	private static SoundManager instance;
	public static SoundManager Instance{
		get{ 
			if (instance == null) {
				instance = FindObjectOfType<SoundManager> ();
			}
			return instance;
		}
	}

	/// <summary>
	/// Changes the background music track.
	/// </summary>
	/// <param name="index">The index of the new track.</param>
	public void changeBackgroundMusic(int index){
		BackgroundSource.clip = BackgroundMusic [index];
		BackgroundSource.loop = true;
		BackgroundSource.Play ();
	}

	/// <summary>
	/// Plays the Sound effect at the given position.
	/// </summary>
	/// <param name="index">The index of the sound effect track.</param>
	/// <param name="transform">The position where it should be played.</param>
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

	/// <summary>
	/// Destroys a game object after a given time has elapsed.
	/// </summary>
	/// <param name="tempObject">The game object that should be destroyed.</param>
	/// <param name="t">The time it should wait before destroying the object.</param>
	IEnumerator DestroyGameObject(GameObject tempObject, int t)
	{
		//Waits for the specified time and continues afterwards
		yield return new WaitForSeconds(t);
		if (tempObject != null) {
			Destroy (tempObject);
		}
	}

	/// <summary>
	/// Adjusts the volume of the background music.
	/// </summary>
	void Start(){
		if(BackgroundSource!=null){
			BackgroundSource.volume = BackgroundVolume;
		}
	}

	/// <summary>
	/// Updates the volume of the background music.
	/// </summary>
	void Update(){
		if(BackgroundSource!=null){
			BackgroundSource.volume = BackgroundVolume;
		}
	}
}
