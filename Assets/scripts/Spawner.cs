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
	IEnumerator Start () 
	{
		
		yield return new WaitForSeconds( delayBeforSpawn );

		for( int i =0; i <= spawnCount; i++ )
		{
			yield return new WaitForSeconds( spawnDelay );

			PoolManager.instance.SpawnFromPool( objectToSpawn , transform.position, transform.rotation );
		}



	}
	
	
}
