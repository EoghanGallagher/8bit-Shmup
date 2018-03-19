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

	[SerializeField]
	private string spawnTrigger;

	[SerializeField]
	private int spawnerId;

	// Use this for initialization

	
	private void OnTriggerEnter2D( Collider2D other )
	{
		
		if( other.tag == spawnTrigger )
		{
			StartCoroutine( "Spawn" );
		}
	}

	private IEnumerator Spawn(  )
	{
		for( int i =0; i < spawnCount; i++ )
		{
			yield return new WaitForSeconds( spawnDelay );

			GameObject spawn = PoolManager.instance.SpawnFromPool( objectToSpawn , transform.position, transform.rotation );
			
			
			//Assign all enemies an id to indicate which spawner spawned them
			BaseCharacter bChar = spawn.GetComponent<BaseCharacter>();

			if( bChar != null )
			{
				bChar.SpawnerId = spawnerId;
			}
			 
		}

		gameObject.SetActive( false );
		
		yield break;
	}
	
	
	public void Destroy()
	{
		StopCoroutine( "Spawn" );
		gameObject.SetActive( false );
	}
}
