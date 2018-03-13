using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Scene
{
	public int id;
	public string name;

}


public class LevelManager : MonoBehaviour 
{

	public static LevelManager instance = null;

	private void Awake()
	{
		
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );

		DontDestroyOnLoad( gameObject );

	}

	private void OnEnable()
	{
		EventManager.StartListening( "LoadScene" , LoadScene );
		EventManager.StartListening( "UnloadScene" , UnloadScene );
	}

	private void OnDisable()
	{
		EventManager.StopListening( "LoadScene" , LoadScene );
		EventManager.StopListening( "UnloadScene" , UnloadScene );
	}

	public List<Scene> scenes;



	private void LoadScene( int sceneIndex )
	{
		StartCoroutine( IELoadScene( sceneIndex ) );
	}

	IEnumerator IELoadScene( int sceneIndex )
	{
		string scene = scenes[ sceneIndex ].name ;
		
		//Load the next scene additively
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync( scene, LoadSceneMode.Additive );	
		 
		//Wait until the last operation fully loads to return anything
        while ( !asyncLoad.isDone )
        {
            yield return null;
        }

		Debug.Log( "Scene " + scene + " loaded Successfully" );

		yield break;
	}

	private void UnloadScene( int sceneIndex )
	{
		string scene = scenes[ sceneIndex ].name;
		//Unload previous Scene Asynchronously
		SceneManager.UnloadSceneAsync( scenes[ sceneIndex ].name  );

		Debug.Log( "Scene " + scene + " unloaded Successfully" );
	}

	
	
	
}
