using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMarker : MonoBehaviour 
{

	[SerializeField]
	private bool isOkToUnloadScene;

	[SerializeField]
	private int levelToLoad, levelToUnload , nextDestination;

	void OnTriggerEnter2D( Collider2D other )
	{
		if( other.tag == "Anchor" )
		{
			
			if( isOkToUnloadScene )
				EventManager.TriggerEvent( "UnloadScene" , levelToUnload );

			//Load Scene Additively.
			EventManager.TriggerEvent( "LoadScene" , levelToLoad );

			EventManager.TriggerEvent( "UpdateDestination", nextDestination );

			StartCoroutine( CountDown( 10 ) );
		
		}
	}

	private IEnumerator CountDown( int seconds )
	{
		while( seconds  >  0 )
		{
			yield return new WaitForSeconds( 1.0f );
			seconds --;
		}

		EventManager.TriggerEvent( "StartMoving" , 1 );
	}

}
