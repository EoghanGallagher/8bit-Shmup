using UnityEngine.Audio;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour {

	// Use this for initialization

	public Sound[] sounds;

	public static SoundManager instance = null;
	
	
	void OnEnable()
	{
		EventManager.StartListening( "PlaySound", Play);
	}
	
	void OnDisable()
	{
		EventManager.StopListening( "PlaySound", Play);
	}
	void Awake () 
	{
		
		//Singleton
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );

		
		
		DontDestroyOnLoad( gameObject );
		
		//Add audio source for each sound in the array
		foreach( Sound s in sounds )
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
			s.source.playOnAwake = s.loop;
			s.source.mute = s.mute;
		}

	}
	

	public void Play( int id )
	{
		Sound s = Array.Find( sounds , sound => sound.id == id );

		if( s == null )
			Debug.Log( "Sound not found..." );

		s.source.Play();
	}
}
