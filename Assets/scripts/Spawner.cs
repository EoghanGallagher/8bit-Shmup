using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	[SerializeField]
	private float delayBeforSpawn;
	
	[SerializeField]
	private string objectToSpawn;

	[SerializeField]
	private float  spawnCount;

	[SerializeField]
	private float spawnDelay;

	// Use this for initialization
	

	private void OnTriggerEnter2D( Collider2D other )
	{
			Debug.Log( other.name );
		if( other.tag == "SpawnTrigger" )
		{
			Debug.Log( "Hit Spawn Collider" );
			StartCoroutine( "Spawn" );
		}
	}

	private IEnumerator Spawn(  )
	{
		for( int i =0; i < spawnCount; i++ )
		{
			yield return new WaitForSeconds( spawnDelay );

			PoolManager.instance.SpawnFromPool( objectToSpawn , transform.position, transform.rotation );
		}

		yield break;
	}
	
	
}
