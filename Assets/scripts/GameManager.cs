using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : MonoBehaviour 
{


	private bool isPaused = false;
	public static GameManager instance = null;

	void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );

		DontDestroyOnLoad( gameObject );

		

	}

	void OnEnable()
	{
		   SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		  SceneManager.sceneLoaded -= OnSceneLoaded;
	}


	void Start()
	{
		Debug.Log ( SceneManager.GetActiveScene().name );
	}

	void Update()
	{
		
		if ( Input.GetKeyDown( KeyCode.P ) )
		{
			Pause();
		}
	}


	//Pause Game
	private void Pause()
	{
		//If not in game don't pause
		if( SceneManager.GetActiveScene().name == "Splash" )
		{
			return;
		}
		
		if( !isPaused  )
		{
			EventManager.TriggerEvent( "PauseGame" , 1 );
			Time.timeScale = 0.0f;
			isPaused = true;
		}
		else
		{
			EventManager.TriggerEvent( "PauseGame" , 0 );
			Time.timeScale = 1.0f;
			isPaused = false;
		}
	}

	
	void OnSceneLoaded( Scene scene, LoadSceneMode mode )
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

		switch( scene.name )
		{
			case "level_1":

			//Trigger level 1 music
			EventManager.TriggerEvent( "PlaySound" , 3 );

			break;
		}
    }

	
	
}
