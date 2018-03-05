using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour 
{

	[SerializeField]
	private string levelToLoad, levelToUnload;

	void OnTriggerEnter2D( Collider2D other )
	{
		if( other.tag == "Anchor" )
		{
			//Load Scene Additively.
			
			if( levelToLoad.Length != 0 )
				SceneManager.LoadScene( levelToLoad , LoadSceneMode.Additive);
		}
	}

}
