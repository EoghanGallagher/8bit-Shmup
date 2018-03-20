using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour 
{

	void OnTriggerEnter2D( Collider2D other )
	{
		if( other.tag == "Anchor")
		{
			StartCoroutine( CountDown() );
		}
	}

	IEnumerator CountDown()
	{
		

		for( int x= 3; x > 0; x-- )
		{
			yield return new WaitForSeconds(1.0f);
		}

		EventManager.TriggerEvent( "StartEruption" , 0 );
		
	}

	
}
