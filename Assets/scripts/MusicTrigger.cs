
using UnityEngine;

public class MusicTrigger : MonoBehaviour 
{

	private void OnTriggerEnter2D( Collider2D other )
	{
		if( other.tag == "SpawnTrigger" )
		{
			
			Debug.Log( "Starting Music ....................................." );
			EventManager.TriggerEvent( "PlaySound" , 6 );
		}
	}
	
}
