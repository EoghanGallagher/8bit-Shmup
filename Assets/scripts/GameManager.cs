
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

	public static GameManager instance = null;

	void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );

		DontDestroyOnLoad( gameObject );

	}

	void Start()
	{
		Invoke("InitGame", 2);
	}


	void InitGame()
	{
		Debug.Log( "Starting Game ...." );
		SceneManager.LoadScene( "level_1" );
	}
	
}
