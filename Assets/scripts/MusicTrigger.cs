
using UnityEngine;

public class MusicTrigger : MonoBehaviour 
{

	public int soundToStop;
	public int soundToStart;

	private void OnTriggerEnter2D( Collider2D other )
	{
		if( other.tag == "SpawnTrigger" )
		{
			EventManager.TriggerEvent( "StopSound" , soundToStop );
			EventManager.TriggerEvent( "PlaySound" , soundToStart );
		}
	}
	
}
